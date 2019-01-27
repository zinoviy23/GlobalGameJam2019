using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTrain : MonoBehaviour
{
    public GameObject train;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "ЖД")
        {
            train.SetActive(true);
        }
    }
}
