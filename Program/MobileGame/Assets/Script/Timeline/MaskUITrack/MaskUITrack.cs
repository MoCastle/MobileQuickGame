using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
[TrackColor(0.9454092f, 0.9779412f, 0.3883002f)]
[TrackClipType(typeof(MaskUIClip))]//轨道承载的剪辑类型
[TrackBindingType(typeof(Image))]
public class MaskUITrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MaskUIMixer>.Create(graph, inputCount);
    }
}
