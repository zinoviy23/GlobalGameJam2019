using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleWalker : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.MovePosition(transform.position +
                                 Vector3.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime * 5); 
    }
}
