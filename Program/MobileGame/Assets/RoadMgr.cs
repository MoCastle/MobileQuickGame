using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoadMgr {

    static string SceneFilePath
    {
        get
        {
            return RoadMgr.SceneFilePath + "/SceneData";
        }
    }

    static string StreamAssetPath
    {
        get
        {
            string path = "";
            switch(Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    path = Application.streamingAssetsPath;
                    break;
                default:
                    path = "";
                    break;
            }
            return path;
        }
    }
}
