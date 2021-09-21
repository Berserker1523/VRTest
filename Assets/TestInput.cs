using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(OVRManager.systemHeadsetType.ToString());
        Debug.Log(OVRManager.boundary.GetDimensions(OVRBoundary.BoundaryType.PlayArea));
        Debug.Log(OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea));
        foreach(Vector3 a in OVRManager.boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea))
        {
            Debug.Log(a);
        }
        //OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
        OVRManager.InputFocusAcquired += Unpause;
        OVRManager.InputFocusLost += Pause;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        /*transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);*/
        transform.position = new Vector3(transform.position.x + OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x * 2f * Time.deltaTime, transform.position.y + (OVRInput.Get(OVRInput.Touch.SecondaryIndexTrigger) ? 2f * Time.deltaTime : 0) + OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) * 2f * Time.deltaTime - OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) * 2f * Time.deltaTime, transform.position.z + OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y * 2f * Time.deltaTime);
        OVRInput.SetControllerVibration(Mathf.Abs(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)), Mathf.Abs(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)), OVRInput.Controller.LTouch);
    }

    void Pause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }

    void Pause()
    {
        Pause(true);
    }

    void Unpause()
    {
        Pause(false);
    }
}
