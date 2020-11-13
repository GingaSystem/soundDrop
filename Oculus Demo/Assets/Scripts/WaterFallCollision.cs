using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFallCollision : MonoBehaviour
{
    private float collisionLevel = 1.0f;
    private float timeCollided = 0.0f;

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
        var waterFall = GetComponent<ParticleSystem>();
        var rawLevel = (waterFall.transform.position.y - obj.transform.position.y);
        collisionLevel = Mathf.Min(1.0f, Mathf.Max(0.3f, rawLevel / 3.0f));
        timeCollided = timeElapsed;

        var debugLog = name + ": rawLevel = " + rawLevel + ", collisionLevel = " + collisionLevel + ", timeCollided = " + timeCollided;
        Debug.Log(debugLog);
        OVRDebugConsole.Log(debugLog);
    }

    public float CollisionLevel
    {
        get {
            if ((timeElapsed - timeCollided) > 0.3f)
            {
                collisionLevel = 1.0f;
            }
            return collisionLevel;
        }
    }
}
