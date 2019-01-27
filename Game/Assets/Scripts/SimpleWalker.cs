using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class SimpleWalker : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Animator animator;
    [SerializeField] private float speed;

    private readonly int speedHash = Animator.StringToHash("speed");
    private readonly int clickHash = Animator.StringToHash("Click");
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        rigidbody2D.velocity = Vector2.right * Input.GetAxisRaw("Horizontal") * speed;

        if (rigidbody2D.velocity.x > 0.01)
            spriteRenderer.flipX = false;
        else if (rigidbody2D.velocity.x < -0.01)
            spriteRenderer.flipX = true;
        
        animator.SetFloat(speedHash, rigidbody2D.velocity.magnitude);
    }

    public void Flip(bool flip)
    {
        spriteRenderer.flipX = flip;
    }

    public void Click()
    {
        animator.SetTrigger(clickHash);
    }
}
