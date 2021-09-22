using UnityEngine;

public class SimpleMotorcycleController : MonoBehaviour
{
    [SerializeField] private GameObject handle;
    [SerializeField] private MotorcycleHand leftHand;
    [SerializeField] private MotorcycleHand righHand;
   
    private const float Acceleration = 5f;
    private const float Friction = 2.5f;
    private const float MaxForwardVelocity = 15f;
    private const float MaxBackwardVelocity = -5f;
    private const float MaxVibrationActivationVelocity = 2.5f;
    private const float MaxSteeringAngle = 45f; // maximum steer angle the wheel can have
    private const float HandsForwardDirectionMaxSeparation = 0.4f;//magic numbers, seen with logs about my hands distance in forward direction
    private const float HandsForwardDirectionMinSeparation = 0.1f;
    private float velocity;

    public void FixedUpdate()
    {
        OVRInput.FixedUpdate();

        float motor = 0f;//Acceleration * Input.GetAxis("Vertical");
        if (righHand.grabbed)
        {
            float input = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, righHand.controller);
            motor += Acceleration * input;
            if (input > 0 && velocity < MaxVibrationActivationVelocity)
                OVRInput.SetControllerVibration(1, 1, righHand.controller);
        }
        if (leftHand.grabbed)
        {
            float input = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, leftHand.controller);
            motor -= 2 * Acceleration * input;
            if (input > 0 && velocity > 0)
                OVRInput.SetControllerVibration(1, 1, leftHand.controller);
        }

        velocity = Mathf.Clamp(velocity + (motor - (motor == 0? Mathf.Lerp(velocity, 0, Friction * Time.deltaTime) : 0)) * Time.deltaTime, MaxBackwardVelocity, MaxForwardVelocity);
        transform.Translate(Vector3.forward * velocity * Time.deltaTime);

        float steering = 0f;//maxSteeringAngle * Input.GetAxis("Horizontal");
        if (righHand.grabbed && leftHand.grabbed)
        {
            //normalization and clamping
            steering = Mathf.Clamp((leftHand.handAnchor.localPosition.z - righHand.handAnchor.localPosition.z) / HandsForwardDirectionMaxSeparation, -HandsForwardDirectionMaxSeparation, HandsForwardDirectionMaxSeparation);

            if (Mathf.Abs(steering) < HandsForwardDirectionMinSeparation)
                steering = 0;
            else
                steering /= HandsForwardDirectionMaxSeparation;
            steering *= MaxSteeringAngle;

            handle.transform.localEulerAngles = new Vector3(handle.transform.localEulerAngles.x, handle.transform.localEulerAngles.y, steering);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(transform.localEulerAngles.y, transform.localEulerAngles.y + steering, Mathf.Abs(velocity) / 5f * Time.deltaTime), transform.localEulerAngles.z);
        }
    }

    void Start()
    {
        OVRManager.InputFocusAcquired += Unpause;
        OVRManager.InputFocusLost += Pause;
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