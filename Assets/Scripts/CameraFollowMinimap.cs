using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMinimap : MonoBehaviour
{

    public GameObject objectToFollow;
    public Vector3 offset;

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveToTarget();
        LookAtTarget();
    }

    public void MoveToTarget()
    {
        transform.position = new Vector3( objectToFollow.transform.position.x, 40, objectToFollow.transform.position.z);
    }

    public void LookAtTarget()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.down, objectToFollow.transform.forward);
    }
}
