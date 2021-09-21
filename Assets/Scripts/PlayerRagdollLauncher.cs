using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerRagdollLauncher : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Ragdoll ragdoll;

    [SerializeField] private AudioClip crashSound;
    GameObject canvas;
    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Score");
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Truck")
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(collision.transform.forward * 400000 + transform.up * 400000);
            ragdoll.GetComponents<AudioSource>()[0].Play();
            ragdoll.SetEnabled(true);


        }
        else if (collision.gameObject.tag == "SpeedBreaker")
        {
            var speed = rb.velocity.magnitude;
            Debug.Log(speed);
            var prob = Random.value;
            if (speed>=3 && prob<0.2)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(transform.forward * 400000 + transform.up * 200000);
                ragdoll.GetComponents<AudioSource>()[0].Play();
                ragdoll.SetEnabled(true);
            }
           else if (speed >= 3 && prob >=0.2 && prob < 0.6)
            {
                AudioSource crash = gameObject.AddComponent<AudioSource>();
                canvas.GetComponentInChildren<Text>().GetComponent<Score>().accident();
                crash.clip = crashSound;
                crash.Play();
                StartCoroutine(disableCollision(collision.gameObject.GetComponent<MeshCollider>()));
            }
        }
    }
    IEnumerator disableCollision(Collider collider)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        collider.enabled = true;
    }
    }
