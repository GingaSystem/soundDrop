using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMixerSetting : MonoBehaviour
{
    public GameObject testCube;
    public GameObject cube;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;
    // Start is called before the first frame update
    void Start()
    {








    }

    // Update is called once per frame
    void Update()
    {

        float gain1 = 0, gain2 = 0, gain3 = 0, gain4 = 0, gain5 = 0;
        var aud = GetComponent<AudioSource>();

        Debug.Log(gain1);
        gain1 = cube.transform.localScale.y;
        gain2 = cube2.transform.localScale.y;
        gain3 = cube3.transform.localScale.y;
        gain4 = cube4.transform.localScale.y;
        gain5 = cube5.transform.localScale.y;



        Debug.Log("cubeScale" + cube.transform.localScale);


        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain1", gain1);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain2", gain2);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain3", gain3);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain4", gain4);
        aud.outputAudioMixerGroup.audioMixer.SetFloat("gain5", gain5);

    }
}
