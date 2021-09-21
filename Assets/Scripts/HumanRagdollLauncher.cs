using UnityEngine;

public class HumanRagdollLauncher : MonoBehaviour
{
    [SerializeField] private Ragdoll ragdoll;
    private AudioSource audioSource;
    private Collider collider;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!ragdoll.IsEnabled && collision.gameObject.tag == "Player")
        {
            ragdoll.SetEnabled(true);
            audioSource.Play();
            collider.enabled = false;
        }
    }
}
