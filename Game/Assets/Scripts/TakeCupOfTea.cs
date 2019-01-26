using UnityEngine;
using Interactive;

public class TakeCupOfTea : InteractiveObject
{   
    public override void Interact()
    {
        Debug.Log("I take cup of tea!");
    }
}