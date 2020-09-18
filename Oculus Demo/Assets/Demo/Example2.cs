    using UnityEngine;

    public class Example2 : MonoBehaviour
    {
        public AudioSpectrum spectrum;
        public GameObject[] cubes;
        public float scale;
        public bool micON = false;

        private void Start()
        {
            if (micON)
            {
                var aud = GetComponent<AudioSource>();
                aud.clip = Microphone.Start(null, true, 10, 44100);
                aud.loop = true;
                while (!(Microphone.GetPosition(null) > 0)) { }
                aud.Play();
            }
    
        }

        private void Update()
        {
            for (int i = 0; i < cubes.Length; i++)
            {
                var cube = cubes[i];
                cube.GetComponent<Renderer>().material.color = new Color(spectrum.Levels[i]*100, spectrum.Levels[i+1]*100, spectrum.Levels[i+2]*100);
                    var localScale = cube.transform.localScale;
                var localPosition = cube.transform.localPosition;



                localScale.y = spectrum.Levels[i] * scale;
                localPosition.x = spectrum.Levels[i] * scale;


                cube.transform.localScale = localScale;

            }
        }
    
    }