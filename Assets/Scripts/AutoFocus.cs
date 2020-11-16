	
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
 
public class AutoFocus : MonoBehaviour
{
    void Start()
    {
        // Setting up callbacks with the Vuforia behaviour
        var vuforia = VuforiaARController.Instance;
        vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        vuforia.RegisterOnPauseCallback(OnPaused);
    }

    private void OnVuforiaStarted()
    {
        //This specifies how the camera will focus
        CameraDevice.Instance.SetFocusMode(
        // "CONTINUOUSAUTO" means that the camera will automatically focus on the subject
        CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    private void OnPaused(bool paused)
    {
        if (!paused) // resumed
        {
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }

}