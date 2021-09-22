using UnityEngine;

public class MotorcycleHand : MonoBehaviour
{
    //public event Action<float> accelerationInput;

    [SerializeField] private Animator animator;
    [SerializeField] public OVRInput.Controller controller;
    [SerializeField] public Transform handAnchor;
    [SerializeField] private Transform gripAnchor;

    private Quaternion originalRotation;
    public bool grabbed;

    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetInteger(Animator.StringToHash("Pose"),(int)OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller));

        bool flex = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controller);
        animator.SetBool(Animator.StringToHash("Grab"), flex);

        if(!flex && grabbed)
        {
            grabbed = false;
            transform.SetParent(handAnchor);
            transform.localPosition = Vector3.zero;
            transform.localRotation = originalRotation;
        }

        if(grabbed)
        {
            //accelerationInput?.Invoke(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger));
            //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y - handAnchor.localEulerAngles.y, transform.localEulerAngles.z);
        }
            
        /*float pinch = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
        animator.SetFloat("Pinch", pinch);*/
    }

    private void OnTriggerStay(Collider other)
    {
        if (((controller == OVRInput.Controller.LTouch && other.name == "HandleLeft") || (controller == OVRInput.Controller.RTouch && other.name == "HandleRight")) && animator.GetBool("Grab"))
        {
            grabbed = true;
            transform.SetParent(gripAnchor);
            transform.position = gripAnchor.position;
        }
    }
}
