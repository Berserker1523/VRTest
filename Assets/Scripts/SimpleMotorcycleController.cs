using UnityEngine;
using System.Collections.Generic;

public class SimpleMotorcycleController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.Wheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.Wheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.Wheel, axleInfo.WheelModel);
        }
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider, GameObject WheelModel)
    {

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        WheelModel.transform.position = position;
        WheelModel.transform.rotation = rotation;
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider Wheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
    public GameObject WheelModel;
}