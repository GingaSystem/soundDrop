using UnityEngine;

public class WaterFallVisualizerController : MonoBehaviour
{
    public AudioSpectrum spectrum;
    public float scale;
    public bool micON = false;
    public ParticleSystem[] waterFalls; 

    private void Start()
    {
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
        for (int i = 0; i < waterFalls.Length; i++)
        {
            var waterFall = waterFalls[i];
            var emission = waterFall.emission;
            emission.rateOverTime = 0.5f + spectrum.Levels[i] * scale;


           

        }
    }

}