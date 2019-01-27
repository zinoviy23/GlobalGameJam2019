using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMachine2 : MonoBehaviour
{

    public Text text;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Interact());
    }

    IEnumerator Interact()
    {
        text.text = "Молодец!!! Ты справился!";
        yield return new WaitForSeconds(3f);
        text.text = "И после упорных трудов...";
        yield return new WaitForSeconds(3f);
        text.text = "Он обустроит свой дом";
        yield return new WaitForSeconds(3f);
        text.text = "Но станет ли это домом?";
        yield return new WaitForSeconds(10f);
        text.text = "Конец";
        yield return new WaitForSeconds(1);
        Application.Quit();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
