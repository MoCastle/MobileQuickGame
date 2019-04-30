using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene;
[Serializable]
public struct CharacterData
{
    public Propty propty;
    public Vector3 position;
    public Vector3 scale;

    public void WriteActor(BaseActorObj actor)
    {
        Propty propty = actor.propty;
        propty.InitPropty();
        propty = actor.propty;
        position = actor.transform.position;
        scale = actor.transform.localScale;
    }
    public void ReadActor(BaseActorObj actor)
    {
        actor.transform.position = position;
        actor.transform.localScale = scale;
        actor.propty = propty;
    }
}

public class BaseCharacter
{

}
