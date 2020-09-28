using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : MonoBehaviour
{

    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        /*
        float rotX, rotY, rotZ;
        rotX = Mathf.PerlinNoise(timeElapsed, timeElapsed) * 360;
        rotY = Mathf.PerlinNoise(timeElapsed, timeElapsed) * 360;
        rotZ = Mathf.PerlinNoise(timeElapsed, timeElapsed) * 360;
        */
        float rot = (timeElapsed * 10) % 360;
        transform.rotation = Quaternion.Euler(rot, rot, rot);

        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            float rotCube = (timeElapsed * 80 * (i+1)/5) % 360;
            child.rotation = Quaternion.Euler(rotCube, rotCube, rotCube);
        }
    }
}
