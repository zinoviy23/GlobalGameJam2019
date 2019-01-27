﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogScript : MonoBehaviour {

    //
    int condition;
    TextScript textScript;
    int maxPhrase;
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
        condition++;
        if (condition == maxPhrase) endDialog = true;
        return textScript.getPhrase(condition);
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
