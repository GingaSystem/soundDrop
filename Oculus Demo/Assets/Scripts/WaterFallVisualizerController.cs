using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WaterFallVisualizerController : MonoBehaviour
{
    public float scale;
    public bool micON = false;

    private AudioSpectrum spectrum;
    private ParticleSystem[] waterFalls;

    private void Start()
    {
        // Get AudioSpecturm
        spectrum = GetComponent<AudioSpectrum>();

        // Duplicate WaterFall0
        GameObject tmpl = GameObject.Find("WaterFall0");
        for (int i = 1; i < spectrum.Levels.Length; ++i)
        {
            GameObject wf = GameObject.Instantiate(tmpl, gameObject.transform);
            wf.name = "WaterFall" + i;

            // --- position
            var pos = wf.transform.localPosition;

            // (1) Line
            /*
            pos.x = 1.5f * i;
            wf.transform.localPosition = pos;
            */

            // (2) Round
            wf.transform.localPosition = Quaternion.Euler(0, 180.0f * ((float)i / spectrum.Levels.Length), 0) * wf.transform.localPosition;

            // --- color
            var trailsModule = wf.GetComponent<ParticleSystem>().trails;
            var waterfallColor = trailsModule.colorOverTrail;
            waterfallColor.color = Color.HSVToRGB((float)(i) / spectrum.Levels.Length, 1, 1);
            trailsModule.colorOverTrail = waterfallColor;
        }

        waterFalls = GetComponentsInChildren<ParticleSystem>();

        var aud = GetComponent<AudioSource>();
        spectrum.audioSource = aud;
        if (micON)
        {
            aud.clip = Microphone.Start(null, true, 10, 44100);
            aud.loop = true;
            while (!(Microphone.GetPosition(null) > 0)) { }
            aud.Play();
        }

    }

    private void Update()
    {
        // Reflect spectrum levels to number of particles to emit.
        for (int i = 0; i < waterFalls.Length; i++)
        {
            var waterFall = waterFalls[i];
            var emission = waterFall.emission;
            //この関数で水の量を調整
            //emission.rateOverTime = 0.5f + spectrum.Levels[i] * scale;
            //emission.rateOverTime = 3 ^ (int)(spectrum.Levels[i] * scale);
            //emission.rateOverTime = (int)(spectrum.Levels[i] * 3000) ^ (int)(spectrum.Levels[i] * scale);
            //emission.rateOverTime = 30;

            if (spectrum.Levels[i] <= 0.0005)
            {
                emission.rateOverTime = 0;
            }
            else if (spectrum.Levels[i] < 0.03)
            {
                //spectrum.Levels[i]が0から0.0022まで y=a^spectrum.Levels[i]*10000 指数関数
                //emission.rateOverTime = Mathf.Pow(1.11f, spectrum.Levels[i]);
                emission.rateOverTime = Mathf.Pow(1.0008f, spectrum.Levels[i]);
            }
            /*
            else if (spectrum.Levels[i] < 0.03)
            {
                //spectrum.Levels[i]が0.0022から0.02まで y = ax + b：y = 188.4973315x + 21.23005337 
                emission.rateOverTime = 188.4973315f * spectrum.Levels[i] + 21.23005337f-10;
            }
            */
            else if (spectrum.Levels[i] < 0.072)
            {

                //spectrum.Levels[i]が0.02から0.072まで y = ax + b：y = 96.15384615x + 23.07692308
                emission.rateOverTime = 96.15384615f * spectrum.Levels[i] + 23.07692308f-10;

            }
            else { emission.rateOverTime = 20; }

            //Debug.Log(spectrum.Levels[i]);
            //File.AppendAllText("/tmp/out.txt", i + "," + spectrum.Levels[i] + "\n");

        }

        // Reflect current gain level (determined based on the collision status) to the mixer.
        updateMixerSettings();
    }

    private void updateMixerSettings()
    {
        var collisions = new List<WaterFallCollision>();

        foreach (var component in GetComponentsInChildren<WaterFallCollision>())
        {
            if (component.enabled)
            {
                collisions.Add(component);
            }
        }

        // assert that one WaterFallCollision for each WaterFall
        Debug.Assert(collisions.Count == waterFalls.Length);

        var aud = GetComponent<AudioSource>();
        for (int i = 0; i < spectrum.Levels.Length; ++i)
        {
            aud.outputAudioMixerGroup.audioMixer.SetFloat("gain" + i, collisions[i].Gain);
        }
    }
}