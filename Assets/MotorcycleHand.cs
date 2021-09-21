using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorcycleHand : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private OVRInput.Controller controller;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetInteger(Animator.StringToHash("Pose"),(int)OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, controller));

        bool flex = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, controller);
        animator.SetBool(Animator.StringToHash("Grab"), flex);

        /*float pinch = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller);
        animator.SetFloat("Pinch", pinch);*/
    }
}
