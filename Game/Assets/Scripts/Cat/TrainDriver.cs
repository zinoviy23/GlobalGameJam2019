using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDriver : MonoBehaviour
{
    private float speed = 0.0f;
    private Vector2 target;
    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        target = new Vector2(10.56f, 1.51f);
        position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    public void StartTrain()
    {
        speed = 10.0f;
    }
}
