using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterFallCollisionImpl02 : WaterFallCollision
{
    // 最後にpartcileとcollideしてから、これ以上の時間が経過したらオブジェクトはwaterfallの外に出たものとみなす
    private const float OBJECT_DETECT_THRESHOLD = 0.4f;

    private float gain = 1.0f;
    private float timeCollided = 0.0f;
    private float timeCollidedLeftHand = 0.0f;
    private float timeCollidedRightHand = 0.0f;

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

        // コントローラを0.3sec振動させる
        if (LeftHandInWaterFall)
        {
            StartCoroutine(VibrationController.VibrateLeftFor(OBJECT_DETECT_THRESHOLD));
        }
        if (RightHandInWaterFall)
        {
            StartCoroutine(VibrationController.VibrateRightFor(OBJECT_DETECT_THRESHOLD));
        }
    }

    private IEnumerator StopVibrationAfter(float seconds, OVRInput.Controller controller)
    {
        yield return new WaitForSeconds(seconds);
        OVRInput.SetControllerVibration(0, 0, controller);
    }

    void OnParticleCollision(GameObject obj)
    {
        // Inspectorでチェックが入っていない場合は処理を行わないようにする
        if (!enabled) { return; }

        var waterFall = GetComponent<ParticleSystem>();
        var relativeDistance = 0.0f;


        // approx. 0~3.5
        relativeDistance = (waterFall.transform.position.y - obj.transform.position.y);

        float rawGain;
        // (1) Linear conversion (gain = 2x + 0.3)
        //rawGain = 2.0f * relativeDistance + 0.3f;

        // (2) gain = 0.3 + x / (1 + |x|)
        //rawGain = 0.3f + (3 * relativeDistance / (1 + Mathf.Abs(relativeDistance - 1)));

        //(3)gain = 0.3/0.8 * relativeDistance (relativeDistance:0~0.8)

        if (relativeDistance < 0.8f)
        {
            rawGain = (0.3f / 0.8f) * relativeDistance;
        }
        else if (relativeDistance < 1.3f)
        {

            //gain = (1-0.3)/(1.3-0.8) * relativeDistance (relativeDistance:0.8~1.3)
            rawGain = (1.0f - 0.3f) / (1.3f - 0.8f) * (relativeDistance - 0.8f) + 0.3f;
        }
        else if (relativeDistance < 2.0f)
        {

            rawGain = (1.5f - 1.0f) / (2.0f - 1.3f) * (relativeDistance - 1.3f) + 1.0f;
        }
        else
        {

            rawGain = (3.0f - 1.5f) / (2.5f - 2.0f) * (relativeDistance - 2.0f) + 1.5f;
        }

        gain = Mathf.Max(0, Mathf.Min(3, rawGain));

        timeCollided = timeElapsed;//オブジェクトの衝突した時間として現在時刻を記録
        if (obj.name == "CustomHandLeft")
        {
            timeCollidedLeftHand = timeElapsed;
        }
        else if (obj.name == "CustomHandRight")
        {
            timeCollidedRightHand = timeElapsed;
        }

        var debugLog = name + "/" + obj.name + ": relativeDistance = " + relativeDistance + ", gain = " + gain + ", timeCollided = " + timeCollided;
        Debug.Log(debugLog);
        OVRDebugConsole.Log(debugLog);
    }

    public bool ObjectIsInWaterFall
    {
        // Returns true if some object (rock, hand, etc.) is in the waterfall.
        get
        {
            return ((timeElapsed - timeCollided) < OBJECT_DETECT_THRESHOLD);
        }
    }

    public bool RightHandInWaterFall
    {
        // Returns true if the right hand is in the waterfall.
        get
        {
            return ((timeElapsed - timeCollidedRightHand) < OBJECT_DETECT_THRESHOLD);
        }
    }


    public bool LeftHandInWaterFall
    {
        // Returns true if the left hand is in the waterfall.
        get
        {
            return ((timeElapsed - timeCollidedLeftHand) < OBJECT_DETECT_THRESHOLD);
        }
    }

    public override float Gain
    {
        get
        {
            if (!ObjectIsInWaterFall)
            {
                gain = 1.0f;
            }
            return gain;
        }
    }
}
