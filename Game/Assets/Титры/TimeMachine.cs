using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeMachine : MonoBehaviour {

    public Text text;
	// Use this for initialization
	void Start () {
        StartCoroutine(Interact());
    }

    IEnumerator Interact()
    {
        text.text = "Он ненавидел это дом всем сердцем...";
        yield return new WaitForSeconds(3f);
        text.text = "И всегда мечтал переехать...";
        yield return new WaitForSeconds(3f);
        text.text = "И вот он уже построил себе дом...";
        yield return new WaitForSeconds(3f);
        text.text = "На берегу тихого океана...";
        yield return new WaitForSeconds(3f);
        text.text = "И теперь ему не хватает только...";
        yield return new WaitForSeconds(5f);
        text.text = "КРОВАТИ!!!";
        yield return new WaitForSeconds(3f);
        text.text = "А ещё надо сходить к врачу...";
        yield return new WaitForSeconds(1.7f);
        text.text = "Который живёт в подвале...";
        yield return new WaitForSeconds(1.7f);
        text.text = "Он давно к нему записался...";
        yield return new WaitForSeconds(1.7f);
        text.text = "И всё не может до него дойти.";
        yield return new WaitForSeconds(3f);

	    SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
