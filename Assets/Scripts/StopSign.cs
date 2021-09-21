using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StopSign : MonoBehaviour
{
    public GameObject truck;
    private bool stopped = false;
    GameObject canvas;
    private void Start()
    {
        //canvas = GameObject.FindGameObjectWithTag("Score");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            stopped = false;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Tocó");
            var speed = col.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            if (Vector3.Dot(col.gameObject.transform.forward, transform.forward) < 0 ) {
                if (!stopped &&  Random.value < 0.2)
                {
                    Debug.Log("activado");
                    Vector3 lightPos = col.gameObject.transform.position;
                    Vector3 lightDirection = gameObject.transform.forward;
                    Quaternion lightRotation = gameObject.transform.rotation;
                    float spawnDistance = 3;
                    Vector3 spawnPos = lightPos + (-lightDirection * spawnDistance);
                    var spawn = Instantiate(truck, spawnPos + (transform.right * 10), lightRotation * Quaternion.Euler(0, -90, 0));
                    spawn.GetComponent<Rigidbody>().AddForce(spawn.transform.forward * 8000000);
                    spawn.GetComponent<AudioSource>().Play();
                }
                else if (!stopped &&  Random.value >= 0.2 && Random.value < 0.6)
                {
                    //canvas.GetComponentInChildren<Text>().GetComponent<Score>().penalty();
                }
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var speed = other.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            if (speed < 0.5)
            {
                Debug.Log("Frenó");
                stopped = true;
            }
        }
       
    }
}
