using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bad : Interactive.ButtonInteractiveObject
{
    bool invis;
	// Use this for initialization
    void Awake()
    {
        invis = true;
    }

    protected override IEnumerator Interact()
    {
        if(invis)
        {
            StartInteracting();
            yield return new WaitForSeconds(0.2f);
            triggerObject.GetComponent<SimpleWalker>().Click();
            yield return new WaitForSeconds(0.3f);
            invis = false;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(7f);

            SceneManager.LoadScene(4);
            FinishInteracting();
        }
    }
}