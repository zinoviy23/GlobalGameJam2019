using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TextScript {

    int maxPhrase;
    
    [SerializeField]
    string[] masPhrase;

    public TextScript(string[] mas)
    {
        maxPhrase = masPhrase.Length;
        masPhrase = mas;
    }

    public string getPhrase(int num)
    {
        if (num > -1 && num < masPhrase.Length) return masPhrase[num];
        return null;
    }

    public int getMax()
    {
        return masPhrase.Length;
    }
}
