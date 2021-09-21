using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Rigidbody[] ragdollRigibodies;
    private Collider[] ragdollColliders;

    public bool IsEnabled { get; set; }

    private void Start()
    {
        ragdollRigibodies = transform.GetComponentsInChildren<Rigidbody>();
        ragdollColliders = transform.GetComponentsInChildren<Collider>();
        SetEnabled(false);
    }

    public void SetEnabled(bool enabled)
    {
        IsEnabled = enabled;
        SetRagdollRigidbodiesKinematic(!enabled);
        SetRagdollCollidersEnabled(enabled);
        if(animator != null)
            animator.enabled = !enabled;
    }

    private void SetRagdollRigidbodiesKinematic(bool kinematic)
    {
        foreach (Rigidbody rdrb in ragdollRigibodies)
        {
            rdrb.collisionDetectionMode = kinematic ? CollisionDetectionMode.Discrete : CollisionDetectionMode.Continuous;
            rdrb.isKinematic = kinematic;
        }
    }

    private void SetRagdollCollidersEnabled(bool enabled)
    {
        foreach (Collider rdc in ragdollColliders)
        {
            rdc.enabled = enabled;
        }
    }
}