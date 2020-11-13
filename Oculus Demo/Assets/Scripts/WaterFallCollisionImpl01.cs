using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFallCollisionImpl01 : WaterFallCollision
{
    private float gain = 1.0f;
    private float timeCollided = 0.0f;
    private float collisionPotisionHand = 0; //手の衝突位置
    private float nGain = 1; //ゲインを調整する係数 

    private float timeElapsed;

    //public OVRCameraRig ovrCameraRig = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    void OnParticleCollision(GameObject obj)
    {
        // Inspectorでチェックが入っていない場合は処理を行わないようにする
        if (!enabled) { return; }

        var waterFall = GetComponent<ParticleSystem>();
        var relativeDistance = 0.0f;

        if (HandIsInside)
        {
            // hand is inside
            relativeDistance = (collisionPotisionHand - obj.transform.position.y);

            if (relativeDistance < 0)
            {

                gain = Mathf.Max(0, Mathf.Min(3, nGain * relativeDistance + 1));
                //手が衝突場所から上がっていくとき

            }
            else
            {
                gain = Mathf.Max(0, Mathf.Min(3, nGain * relativeDistance + 1));
                //手が衝突場所から下がっていくとき

            }
        }
        else
        {
            // hand just came in
            gain = 1.0f;
            collisionPotisionHand = obj.transform.position.y;


        }

        timeCollided = timeElapsed;//手の衝突した時間として現在時刻を記録



        var debugLog = name + ": relativeDistance = " + relativeDistance + ", gain = " + gain + ", timeCollided = " + timeCollided;
        Debug.Log(debugLog);
        OVRDebugConsole.Log(debugLog);
    }

    public bool HandIsInside
    {
        // Returns true if the hand is in the waterfall.
        get
        {
            return ((timeElapsed - timeCollided) < 0.3f);
        }
    }

    public override float Gain
    {
        get
        {
            if (!HandIsInside)
            {
                gain = 1.0f;
            }
            return gain;
        }
    }
}