
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
    private float money;

    private void Start()
    {
        money = 20;
        scoreText.text = "Dinero: " + money;
        StartTimer(30);
    }
    // Update is called once per frame
  
    public void StartTimer(float duration)
    {
        StartCoroutine(RunTimer(duration));
    }

    private IEnumerator RunTimer(float duration)
    {
        while (true) {
            yield return new WaitForSeconds(duration);
            money--;
            scoreText.text = "Dinero: " + money + "\nSe acaba el tiempo! (Dinero -1)";
            StartCoroutine(UIReset());
            Debug.Log("EVENT!");
        }
        
    }
   
        private IEnumerator UIReset()
    {
        yield return new WaitForSeconds(5);
        scoreText.text = "Dinero: " + money;
    }
        public void penalty()
    {
        money=money-2;
        scoreText.text = "Dinero: " + money + "\nFotomulta! (Dinero -2)";
        StartCoroutine(UIReset());
    }
    public void accident()
    {
        money = money - 2;
        scoreText.text = "Dinero: " + money + "\nEso va a salir caro! (Dinero -2)";
        StartCoroutine(UIReset());
    }
}
