using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSpawn:MonoBehaviour {
    //生成角色
    public abstract GameObject GenActor();
}
