using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMixerSettingWaterFall : MonoBehaviour
{

    public WaterFallCollision[] collisions;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float gain1 = 0, gain2 = 0, gain3 = 0, gain4 = 0, gain5 = 0;
        var aud = GetComponent<AudioSource>();
        collisions = GetComponentsInChildren<WaterFallCollision>();

        gain1 = collisions[0].CollisionLevel;
        gain2 = collisions[1].CollisionLevel;
        gain3 = collisions[2].CollisionLevel;
        gain4 = collisions[3].CollisionLevel;
        gain5 = collisions[4].CollisionLevel;

        OVRDebugConsole.Log("gain1 = " + gain1);
        OVRDebugConsole.Log("gain2 = " + gain2);
        OVRDebugConsole.Log("gain3 = " + gain3);
        OVRDebugConsole.Log("gain4 = " + gain4);
        OVRDebugConsole.Log("gain5 = " + gain5);


        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain1", gain1);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain2", gain2);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain3", gain3);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain4", gain4);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain5", gain5);

    }
}
