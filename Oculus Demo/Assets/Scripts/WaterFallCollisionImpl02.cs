using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaterFallCollisionImpl02 : WaterFallCollision
{
    // 最後にpartcileとcollideしてから、これ以上の時間が経過したらオブジェクトはwaterfallの外に出たものとみなす
    private const float OBJECT_DETECT_THRESHOLD = 0.3f;

    private float gain = 1.0f;
    private float timeCollided = 0.0f;
    private float timeCollidedLeftHand = 0.0f;
    private float timeCollidedRightHand = 0.0f;

    private float collisionPotisionHand = 0; //手の衝突位置
    private float nGain = 2.0f; //ゲインを調整する係数 

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
        if (LeftHandInWaterFall) {
            var controller = OVRInput.Controller.LTouch;
            OVRInput.SetControllerVibration(1, 1, controller);
            StartCoroutine(StopVibrationAfter(0.3f, controller));
        }
        if (RightHandInWaterFall)
        {
            var controller = OVRInput.Controller.RTouch;
            OVRInput.SetControllerVibration(1, 1, controller);
            StartCoroutine(StopVibrationAfter(0.3f, controller));
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


        // hand is inside
        relativeDistance = (waterFall.transform.position.y - obj.transform.position.y);

        gain = Mathf.Max(0, Mathf.Min(3, nGain * relativeDistance + 0.3f));
        //gain = nx + 0.3


        timeCollided = timeElapsed;//オブジェクトの衝突した時間として現在時刻を記録
        if (obj.name == "CustomHandLeft")
        {
            timeCollidedLeftHand = timeElapsed;
        } else if (obj.name == "CustomHandRight")
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
