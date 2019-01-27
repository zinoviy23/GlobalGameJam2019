using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    [SerializeField] public GameObject[] toDisable;
    [SerializeField] public GameObject train;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject item in toDisable)
        {
            item.SetActive(false);
        }

        train.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
