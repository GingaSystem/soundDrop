using System;
using System.Collections;

using UnityEngine;

public class VibrationController
{
    private static long leftVibrationEndsAt = Int64.MaxValue;
    private static long rightVibrationEndsAt = Int64.MaxValue;

    public static IEnumerator VibrateLeftFor(float seconds)
    {
        leftVibrationEndsAt = DateTime.Now.Ticks + (long)(seconds * Math.Pow(10, 9));
        return VibrationCoroutine(seconds, OVRInput.Controller.LTouch);
    }

    public static IEnumerator VibrateRightFor(float seconds)
    {
        rightVibrationEndsAt = DateTime.Now.Ticks + (long)(seconds * Math.Pow(10, 9));
        return VibrationCoroutine(seconds, OVRInput.Controller.RTouch);
    }

    private static IEnumerator VibrationCoroutine(float seconds, OVRInput.Controller controller)
    {
        // Turn on vibration
        OVRInput.SetControllerVibration(1, 1, controller);
        yield return new WaitForSeconds(seconds);

        // Turn off vibration
        if (leftVibrationEndsAt <= DateTime.Now.Ticks) {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        }
        if (rightVibrationEndsAt <= DateTime.Now.Ticks)
        {
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        }
    }
}
