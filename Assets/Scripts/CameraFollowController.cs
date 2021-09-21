using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public GameObject objectToFollow;
    public Vector3 offset;
    public float followSpeed = 10;
    public float lookSpeed = 10;

    void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }

    public void LookAtTarget()
    {
        Vector3 lookDirection = objectToFollow.transform.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.deltaTime);
    }

    public void MoveToTarget()
    {
        Vector3 targetPos = objectToFollow.transform.position + objectToFollow.transform.forward * offset.z +
            objectToFollow.transform.right * offset.x +
            objectToFollow.transform.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }
}
