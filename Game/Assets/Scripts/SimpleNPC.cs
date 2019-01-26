using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleNPC : Interactive.ButtonInteractiveObject
{
    protected override IEnumerator Interact()
    {
        StartInteracting();
        Debug.Log("Hello");
        yield return new WaitForSeconds(1);
        FinishInteracting();
        yield break;
    }
}
   