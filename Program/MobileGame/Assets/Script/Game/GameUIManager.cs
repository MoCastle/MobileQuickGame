using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using BaseFunc;

namespace FrameWork
{
    public class GameUIManager:BaseFrameWorkManager
    {
        #region 对外接口
        //清理整个UI
        public void ClearAll()
        {
            ClearUIQue(_NormQue);
            ClearUIQue(_LowQue);
        }
        #endregion
        #region 内部
        void ClearUIQue(Transform uiQue)
        {
            for (int StartIdx = uiQue.childCount - 1; StartIdx >= 0; --StartIdx)
            {
                BaseUI ui = uiQue.GetChild(StartIdx).GetComponent<BaseUI>();
                ui.Close();
            }
        }
        #endregion
        public GameUIManager()
        {
            InitWindowCanvas();
        }
        #region 画布属性、层级属性
        #region 内部属性
        Transform _UICanvas;
        //底层UI栈
        Transform _LowQue;
        //普通层UI队列
        Transform _NormQue;
        //通用BG
        Transform _BG;
        EventTrigger _BGTrigger;
        Transform m_EffectCanvas;
        #endregion 
        //当前已打开的UI
        Dictionary<string, Transform> _CurShowUI;
        Dictionary<string, float> _GCUIDict;

        #endregion
        #region 初始化相关
        //初始化UI画布
        void InitWindowCanvas()
        {
            Transform _UICanvas = null;

            GameObject ilegalCanvas = GameObject.Find("UIFrame");
            if (ilegalCanvas != null)
            {
                GameObject.Destroy(ilegalCanvas);
            }
            
            //Object uiCanvas = Resources.Load("Prefab\\Base\\UI\\UIFrame");
            Object uiCanvas = Resources.Load("Prefab\\Base\\UIFrame");

            if (uiCanvas == null)
            {
                Debug.Log("Canvas Prefab Missed");
                return;
            }
            GameObject uiGameObj = GameObject.Instantiate(uiCanvas) as GameObject;
            GameObject.DontDestroyOnLoad(uiGameObj);
            _UICanvas = uiGameObj.transform;

            _LowQue = _UICanvas.transform.Find("LowUI");
            _NormQue = _UICanvas.transform.Find("NormUI");

            _BG = _UICanvas.Find("BG");
            _BG.gameObject.SetActive(false);
            if (_NormQue == null)
            {
                Debug.Log("UICanvas Lay NormUI Missed");
            }
            _CurShowUI = new Dictionary<string, Transform>();
            _GCUIDict = new Dictionary<string, float>();

            GameObject effectCanvas = GameObject.Find("m_EffectCanvas");
            if (effectCanvas != null)
            {
                GameObject.Destroy(effectCanvas);
            }
            Object uiEffectCanvas = Resources.Load("Prefab\\Base\\EffectCanvas");
            if (uiEffectCanvas == null)
            {
                Debug.Log("Canvas Prefab Missed");
                return;
            }
            GameObject uiEffectCanvasObject = GameObject.Instantiate(uiCanvas) as GameObject;
            GameObject.DontDestroyOnLoad(uiEffectCanvasObject);
            m_EffectCanvas = uiEffectCanvasObject.transform;
        }
        #endregion

        #region 窗口开关
        public BaseUI ShowUI(string UIName)
        {
            Transform outUIWnd = null;

            //如果已经打开了 则只需要显示在前面就行了
            if (_CurShowUI.TryGetValue(UIName, out outUIWnd))
            {
                outUIWnd.gameObject.SetActive(true);
                outUIWnd.SetAsLastSibling();
                return outUIWnd.GetComponent<BaseUI>();
            }
            BaseUI UIScr = null;

            //没有打开 重新加载
            GameObject uiGameObj = LoadUI(UIName);
            if (uiGameObj == null)
            {
                return null;
            }

            //检查脚本有没有加
            UIScr = uiGameObj.GetComponent<BaseUI>();
            if (UIScr == null)
            {
                Debug.Log("UI NotAddScript");
                return null;
            }
            HideCurMutex();

            switch (UIScr.Type)
            {
                case UIType.Low:
                    uiGameObj.transform.SetParent(_LowQue, false);
                    break;
                case UIType.Normal:
                    uiGameObj.transform.SetParent(_NormQue, false);
                    //_BG.gameObject.active = true;
                    break;
            }

            //ShowOPForUI(UIScr);
            _CurShowUI.Add(UIName, uiGameObj.transform);
            _GCUIDict.Remove(UIName);

            return UIScr;
        }

        public GameObject ShowEffectUI(string UIName)
        {
            Transform effectUI = m_EffectCanvas.Find(UIName);
            if(effectUI==null)
            {
                GameObject newEffectUI = LoadUI(UIName);
                newEffectUI.transform.parent = m_EffectCanvas;
                effectUI = newEffectUI.transform;
            }
            effectUI.gameObject.active = true;
            return effectUI.gameObject;
        }

        public void CloseEffectUI(string UIName)
        {
            Transform effectUI = m_EffectCanvas.Find(UIName);
            if (effectUI != null)
            {
                effectUI.gameObject.active = false;
            }
        }

        //关闭窗口操作
        public void CloseWnd(BaseUI baseUI)
        {
            //_GCUIDict.Add(baseUI.UIName,Time.time);
            switch (baseUI.Type)
            {
                case UIType.Mutex:
                    ReShowUI();
                    break;
                case UIType.Low:
                    return;
                    break;
            }

            if (GetCurUI() != null)
            {
                _BG.gameObject.SetActive(false);
            }
        }

        //获取UI
        public GameObject LoadUI(string UIName)
        {
            Object uiCanvas = Resources.Load("Prefab/UI/" + UIName);
            if (uiCanvas == null)
            {
                Debug.Log("UI Missed");
                return null;
            }
            string Name = uiCanvas.name;
            GameObject loadedUIModel = GameObject.Instantiate(uiCanvas) as GameObject;
            loadedUIModel.name = uiCanvas.name;

            return loadedUIModel;
        }

        //互斥窗口处理 隐藏当前已经打开的窗口
        public void HideCurMutex()
        {
            //没有打开后需要隐藏的窗口
            if (_NormQue.childCount < 1)
            {
                return;
            }
            //获取之前最后一个窗口
            BaseUI ShowingWnd = GetCurUI();

            //对上一个已打开窗口处理 若互斥则隐藏
            if (ShowingWnd != null && ShowingWnd.Type == UIType.Mutex)
            {
                ShowingWnd.Hide();
            }

        }

        //获取当前打开窗口
        public BaseUI GetCurUI()
        {
            if (_NormQue.childCount < 1)
            {
                return null;
            }
            BaseUI CurUI = null;

            for (int index = _NormQue.childCount - 1; index >= 0; --index)
            {
                Transform WndObj = _NormQue.GetChild(index);
                CurUI = WndObj.GetComponent<BaseUI>();
                //跳过被关掉的窗口
                if (CurUI == null || CurUI.IsOpenning == false)
                {
                    continue;
                }
                else
                {
                    //找到上一个打开中的窗口了
                    break;
                }
            }
            return CurUI;
        }

        //关掉窗口后 处理上一个窗口 若隐藏则打开
        public void ReShowUI()
        {

            if (_NormQue.childCount < 1)
            {
                return;
            }
            BaseUI ShowingWnd = GetCurUI();

            if (ShowingWnd == null || ShowingWnd.Type != UIType.Mutex)
            {
                return;
            }
            else
            {
                ShowOPForUI(ShowingWnd);
            }
        }

        //针对显示UI的操作
        void ShowOPForUI(BaseUI OPUI)
        {
            OPUI.Show();
            bool NeedBG = OPUI.IsUseBG;
            if (NeedBG)
            {
                int BGIdx = _BG.transform.GetSiblingIndex();
                int ShowIdx = OPUI.transform.GetSiblingIndex();

                _BG.SetSiblingIndex(BGIdx > ShowIdx ? ShowIdx : ShowIdx - 1);
                _BG.gameObject.SetActive(true);
                if (OPUI.IsUseBGFunc)
                {

                    //PointerEventData Func = new PointerEventData(OPUI.BGFunc);
                    /*    OPUI.BGFunc;
                    _BGTrigger.OnPointerClick(OPUI.BGFunc);*/
                }
            }
            else
            {
                _BG.gameObject.SetActive(false);
            }
        }
        //针对单个UI的关闭操作
        void CloseOPForUI()
        {
            _BG.gameObject.SetActive(false);
        }

        public override void Update()
        {
        }

        public override void Start()
        {
        }
        #endregion
    }
}

