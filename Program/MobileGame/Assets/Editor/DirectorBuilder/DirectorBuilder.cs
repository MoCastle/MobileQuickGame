using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameScene;
using Cinemachine;
using Cinemachine.Editor;

public class DirectorBuilder {
    [MenuItem("编辑工具/设置场景/普通战斗场景")]
    public static void AddetScene()
    {
        if (GameObject.Find("BattleDir"))
            return;
        GameObject directorSample = Resources.Load("Prefab\\Scene\\BattleDir") as GameObject;
        GameObject director = GameObject.Instantiate<GameObject>(directorSample);
        BattleSceneMainDirectorEntity directorEntity = director.GetComponent<BattleSceneMainDirectorEntity>();
        director.name = "BattleDir";
        Camera mainCamera = Camera.main;
        CinemachineVirtualCamera vc = CinemachineMenu.InternalCreateVirtualCamera("CM vcam", true, typeof(CinemachineComposer), typeof(CinemachineTransposer));
        directorEntity.camera = vc;
        
        if(!GameObject.Find("DoorList"))
        {
            GameObject doorList = new GameObject("DoorList");
            directorEntity.SetDoorList(doorList);
            GameObject doorSample = Resources.Load<GameObject>("Prefab\\Scene\\Door");
            GameObject door = GameObject.Instantiate<GameObject>(doorSample);
            door.transform.SetParent(doorList.transform);
        }
        if (!GameObject.Find("NPCList"))
        {
            GameObject npcList = new GameObject("NPCList");
            directorEntity.SetNPCList(npcList);
        }
    }
}
