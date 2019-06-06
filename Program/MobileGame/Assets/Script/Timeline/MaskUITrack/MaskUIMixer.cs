using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class MaskUIMixer : PlayableBehaviour {
    Image m_Image;
    bool m_FirstFrameHappened;
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        m_Image = playerData as Image;

        if (m_Image == null)
            return;

        //if (!m_FirstFrameHappened)
        //{
        //    m_DefaultColor = m_TrackBinding.color;
        //    m_DefaultIntensity = m_TrackBinding.intensity;
        //    m_DefaultBounceIntensity = m_TrackBinding.bounceIntensity;
        //    m_DefaultRange = m_TrackBinding.range;
        //    m_FirstFrameHappened = true;
        //}

        int inputCount = playable.GetInputCount();
        Debug.Log(playable.GetInputWeight(0));
        Debug.Log(playable.GetInputWeight(1));

        //Color blendedColor = Color.clear;
        //float blendedIntensity = 0f;
        //float blendedBounceIntensity = 0f;
        //float blendedRange = 0f;
        //float totalWeight = 0f;
        //float greatestWeight = 0f;
        //int currentInputs = 0;

        //for (int i = 0; i < inputCount; i++)
        //{
        //    float inputWeight = playable.GetInputWeight(i);
        //    ScriptPlayable<LightControlBehaviour> inputPlayable = (ScriptPlayable<LightControlBehaviour>)playable.GetInput(i);
        //    LightControlBehaviour input = inputPlayable.GetBehaviour();

        //    blendedColor += input.color * inputWeight;
        //    blendedIntensity += input.intensity * inputWeight;
        //    blendedBounceIntensity += input.bounceIntensity * inputWeight;
        //    blendedRange += input.range * inputWeight;
        //    totalWeight += inputWeight;

        //    if (inputWeight > greatestWeight)
        //    {
        //        greatestWeight = inputWeight;
        //    }

        //    if (!Mathf.Approximately(inputWeight, 0f))
        //        currentInputs++;
        //}

        //m_TrackBinding.color = blendedColor + m_DefaultColor * (1f - totalWeight);
        //m_TrackBinding.intensity = blendedIntensity + m_DefaultIntensity * (1f - totalWeight);
        //m_TrackBinding.bounceIntensity = blendedBounceIntensity + m_DefaultBounceIntensity * (1f - totalWeight);
        //m_TrackBinding.range = blendedRange + m_DefaultRange * (1f - totalWeight);
    }
}
