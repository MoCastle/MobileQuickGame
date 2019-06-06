using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MaskUIClip : PlayableAsset
{
    public MaskUIData maskUIData = new MaskUIData();
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MaskUIData>.Create(graph, maskUIData);
        return playable;
    }
}
