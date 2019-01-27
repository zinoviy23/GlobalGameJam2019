using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bad : Interactive.ButtonInteractiveObject
{
    bool invis;
	// Use this for initialization
	void Start () {
        invis = true;
	}
	
	// Update is called once per frame
	void Update () {
		
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
            yield return new WaitForSeconds(0.5f);
            FinishInteracting();
        }
    }
}