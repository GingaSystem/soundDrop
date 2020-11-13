using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMixerSettingWaterFall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var collisions = new List<WaterFallCollision>();

        foreach (var component in GetComponentsInChildren<WaterFallCollision>())
        {
            if (component.enabled)
            {
                collisions.Add(component);
            }
        }

        Debug.Assert(collisions.Count == 5);

        float gain1 = 0, gain2 = 0, gain3 = 0, gain4 = 0, gain5 = 0;
        gain1 = collisions[0].Gain;
        gain2 = collisions[1].Gain;
        gain3 = collisions[2].Gain;
        gain4 = collisions[3].Gain;
        gain5 = collisions[4].Gain;

        var aud = GetComponent<AudioSource>();
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain1", gain1);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain2", gain2);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain3", gain3);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain4", gain4);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain5", gain5);

    }
}
