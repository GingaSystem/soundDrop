using UnityEngine;
using System.Collections;

public class WebcamCapture : MonoBehaviour
{

    //このクラスをウェブカメラの映像をテクスチャとして貼り付けるオブジェクトに適用する

    private WebCamTexture webcamtex;

    // Use this for initialization
    void Start()
    {

        webcamtex = new WebCamTexture();   //コンストラクタ

        Renderer _renderer = GetComponent<Renderer>();  //Planeオブジェクトのレンダラ
        _renderer.material.mainTexture = webcamtex;    //mainTextureにWebCamTextureを指定
        webcamtex.Play();  //ウェブカムを作動させる
    }

}