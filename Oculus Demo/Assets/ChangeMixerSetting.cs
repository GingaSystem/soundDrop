using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMixerSetting : MonoBehaviour
{
    public GameObject cube;
    // Start is called before the first frame update
    void Start()
    {
        float gain1 = 0,gain2 = 0,gain3 = 0,gain4 = 0,gain5 = 0;
        var aud = GetComponent<AudioSource>();
    
        Debug.Log(gain1);
        gain1 = cube.transform.localScale.y;
        gain2 = cube.transform.localScale.y;
        gain3 = cube.transform.localScale.y;
        gain4 = cube.transform.localScale.y;
        gain5 = cube.transform.localScale.y;

        

        Debug.Log("cubeScale"+cube.transform.localScale);


        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain1", gain1);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain2", gain2);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain3", gain3);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain4", gain4);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain5", gain5);






    }

    // Update is called once per frame
    void Update()
    {

    }
}
