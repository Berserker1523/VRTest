using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class trafficLight : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject truck;
    Collider coll; 
    public string isColor;
    string isNext;
    Material temp;
    string[] colors;
    GameObject canvas;
    bool isCol;
    public Material greenOn, redOn, yellowOn;
    List<Material> lights = new List<Material>();
    private void Start()
    {
        //canvas= GameObject.FindGameObjectWithTag("Score");
        
        colors = new string[] { "green", "yellow", "red" };
        isColor = colors[Random.value<0.5?0:2];
        coll=GetComponent<Collider>();
        Material[] matArray = gameObject.GetComponent<MeshRenderer>().materials;
        for (int i=0;i<matArray.Length;i++)
        {
            lights.Add(matArray[i]);
        }
        StartCoroutine(lightBehaviour());
        
    }
    IEnumerator lightBehaviour()
    {
        while (true)
        {
            //Debug.Log(isColor);
            switch (isColor)
            {
                case "green":
                    temp = lights[2];
                    lights[2] = greenOn;
                    gameObject.GetComponent<MeshRenderer>().materials = lights.ToArray();
                    yield return new WaitForSeconds(8);
                    lights[2] = temp;
                    gameObject.GetComponent<MeshRenderer>().materials = lights.ToArray();
                    isColor = colors[1];
                    isNext = colors[2];
                    break;
                case "yellow":
                    temp = lights[0];
                    lights[0] = yellowOn;
                    gameObject.GetComponent<MeshRenderer>().materials = lights.ToArray();
                    yield return new WaitForSeconds(2);
                    lights[0] = temp;
                    gameObject.GetComponent<MeshRenderer>().materials = lights.ToArray();
                    isColor = isNext;
                    break;
                case "red":
                    temp = lights[1];
                    lights[1] = redOn;
                    gameObject.GetComponent<MeshRenderer>().materials = lights.ToArray();
                    yield return new WaitForSeconds(10);
                    lights[1] = temp;
                    gameObject.GetComponent<MeshRenderer>().materials = lights.ToArray();
                    isColor = colors[1];
                    isNext = colors[0];
                    break;

            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && isColor == "red")
        {
            var prob = Random.value;
            //Debug.Log("Tocó");
            if (Vector3.Dot(col.gameObject.transform.forward, transform.forward) < 0) {
                if (prob < 0.2 ) {
                    Vector3 lightPos = col.gameObject.transform.position;
                    Vector3 lightDirection = gameObject.transform.forward;
                    Quaternion lightRotation = gameObject.transform.rotation;
                    float spawnDistance = 3;
                    Vector3 spawnPos = lightPos + (-lightDirection * spawnDistance);
                    var spawn = Instantiate(truck, spawnPos + (transform.right * 10), lightRotation * Quaternion.Euler(0, -90, 0));
                    spawn.GetComponent<Rigidbody>().AddForce(spawn.transform.forward * 8000000);
                    spawn.GetComponent<AudioSource>().Play();
                }
                else if (prob >= 0.2 && prob < 0.6)
                {
                    //canvas.GetComponentInChildren<Text>().GetComponent<Score>().penalty();
                }
            }
        }
       
    }
    
}
