using UnityEngine;

public class HumanMovement : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private GameObject human;
    [SerializeField] private Ragdoll ragdoll;
    private Transform targetPoint;

    private void Start()
    {
        targetPoint = pointA;
    }

    private void Update()
    {
        if (ragdoll.IsEnabled)
            enabled = false;

        if(Vector3.Distance(targetPoint.position, human.transform.position) > 0.5f)
        {
            human.transform.position = Vector3.MoveTowards(human.transform.position, targetPoint.position, Time.deltaTime * 1f);
            var lookPos = targetPoint.position - human.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            human.transform.rotation = Quaternion.Slerp(human.transform.rotation, rotation, Time.deltaTime * 10f);
        }
        else
        {
            ChangeTargetPoint();
        }
    }

    private void ChangeTargetPoint()
    {
        if(targetPoint == pointA)
            targetPoint = pointB;
        else
            targetPoint = pointA;
    }    
}
