using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript {

    int maxPhrase;
    string[] masPhrase;

    public TextScript(string[] mas)
    {
        maxPhrase = masPhrase.Length;
    }

    public string getPhrase(int num)
    {
        if (num > -1 && num < maxPhrase) return masPhrase[num];
        return "None";
    }

    public int getMax()
    {
        return maxPhrase;
    }
}
