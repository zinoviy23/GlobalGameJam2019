using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toast : Interactive.InteractiveObject
{
    public override void Interact()
    {
        StartCoroutine(TakeToast());
    }

    IEnumerator TakeToast()
    {
        const string toast = "Toast";

        foreach (var c in toast)
        {
            Debug.Log(c);
            yield return new WaitForSeconds(0.1f);
        }
    }
}