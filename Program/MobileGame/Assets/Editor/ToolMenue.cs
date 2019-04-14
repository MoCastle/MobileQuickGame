using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ToolMenue : MonoBehaviour {
    [MenuItem("编辑工具/读取场景数据")]
    public static void AddetScene()
    {
        EditAPP.SceneEditMgr.LoadSceneData();
    }
    
    [MenuItem("编辑工具/将场景数据保存为配置表")]
    public static void SaveSceneData()
    {
        EditAPP.SceneEditMgr.SaveSceneData();
    }
    [MenuItem("编辑道具/道具编辑窗")]
    static void ShowEditWindow()
    {
        ItemEditor myWindow = (ItemEditor)EditorWindow.GetWindow(typeof(ItemEditor), false, "", true);//创建窗口
        myWindow.Show();//展示
    }
}
