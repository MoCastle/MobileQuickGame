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
}
