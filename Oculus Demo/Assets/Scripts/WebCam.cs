    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    // Webカメラ
    public class WebCam : MonoBehaviour
    {
        private static int INPUT_SIZE = 256;
        private static int FPS = 30;

        // UI
        RawImage rawImage;
        WebCamTexture webCamTexture;

        // スタート時に呼ばれる
        void Start()
        {
            // Webカメラの開始
            this.rawImage = GetComponent<RawImage>();
    #if UNITY_EDITOR_OSX
            this.webCamTexture = new WebCamTexture("FaceTime HD Camera");
    #else
    this.webCamTexture = new WebCamTexture();
    #endif
            this.rawImage.texture = this.webCamTexture;
            this.webCamTexture.Play();
        }
    }