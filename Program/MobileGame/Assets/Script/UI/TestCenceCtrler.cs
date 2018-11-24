using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCenceCtrler : MonoBehaviour {
    public string[] ActorName;
	// Use this for initialization
	public void OnBtnClick(GameObject obj)
    {
        ReleaseSpawn.PubReleaseActor(ActorName[obj.transform.GetSiblingIndex()]);
    }
}
