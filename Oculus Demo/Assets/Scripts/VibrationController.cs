using System;

using UnityEngine;

public class VibrationController : MonoBehaviour
{
    private static long leftVibrationEndsAt = -1;
    private static long rightVibrationEndsAt = -1;

    public static void VibrateLeftFor(float seconds)
    {
        leftVibrationEndsAt = DateTime.Now.Ticks + (long)(seconds * 10000000);
    }

    public static void VibrateRightFor(float seconds)
    {
        rightVibrationEndsAt = DateTime.Now.Ticks + (long)(seconds * 10000000);
    }

    void Update()
    {
        var now = DateTime.Now.Ticks;

        // Turn on vibration
        if (now <= leftVibrationEndsAt)
        {
            Debug.Log("turn on vibration left");
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
        }
        if (now <= rightVibrationEndsAt)
        {
            Debug.Log("turn onvibration right");
            OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        }

        // Turn off vibration
        if (leftVibrationEndsAt <= now)
        {
            Debug.Log("turn off vibration left");
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
        if (rightVibrationEndsAt <= now)
        {
            Debug.Log("turn off vibration right");
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }
}
