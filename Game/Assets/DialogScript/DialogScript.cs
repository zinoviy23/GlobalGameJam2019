using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScript : MonoBehaviour {

    //
    public int condition;
    public TextScript textScript;
    public int maxPhrase;
    public bool endDialog;

	// Use this for initialization
	void Start () {
        setDialog(textScript);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string nextPhrase()
    {
        if (!endDialog)
        {
            condition++;
            if (condition == maxPhrase) endDialog = true;
            return textScript.getPhrase(condition);
        }
        return null;

    }

    public void setDialog(TextScript ts) {
        textScript = ts;
        endDialog = false;
        maxPhrase = textScript.getMax();
        condition = -1;
    }

    public bool getStart()
    {
        if (condition == -1) return false;
        return true;
    }
        
}
