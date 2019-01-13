using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class PathManager {
    public static string SceneData
    {
        get
        {
            string sceneData = PathManager.StreamAssess + "/JSon/SceneData.txt";
            return sceneData;
        }
    }
    public static string StreamAssess
    {
        get
        {
            return Application.streamingAssetsPath;
        }
    }
}
