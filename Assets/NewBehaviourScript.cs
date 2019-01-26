using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Dictionary<string, string[]> dialogues = new Dictionary<string, string[]>();
    public Text dialogueText;
    public GameObject dialogueContainer;
    public Text pressE;


    Rigidbody2D rb;
    GameObject objectToTalk;

    bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        dialogueContainer.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        onGround = true;

        dialogues.Add("Vasily", new string[]
                                {"Greetings and salutations, Stranger",
                                "I've been watching you for 13 years",
                                "And now...",
                                "I can offer you a special offer.",
                                "Are you interested?"});

        dialogues.Add("Evgenia", new string[]
                                 {"LalLalla",
                                  "My fantasy has ended here",
                                  "lol"});
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && !State.isDialogue)
        //right
        {
            rb.velocity = new Vector2(-4f, rb.velocity.y);
        } else if (Input.GetKey(KeyCode.D) && !State.isDialogue)
        // left
        {
            rb.velocity = new Vector2(4f, rb.velocity.y);
        } else
        // stop
        {
            rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.Space) && onGround && !State.isDialogue)
        // Jump
        {
            onGround = false;
            rb.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.E) && !State.isDialogue)
        // start dialogue
        {
            //Debug.Log(dialogues[objectToTalk.ToString().Split(' ')[0]][0]);
            if (objectToTalk)
            {
                var dialogue = dialogues[objectToTalk.ToString().Split(' ')[0]];
                var text = dialogueText;
                DialogueTrigger.TriggerDialogue(dialogue, dialogueContainer, text);
            }
        }
        rb.transform.rotation = Quaternion.identity;

        float minimal = 5f;
        foreach(var obj in GameObject.FindGameObjectsWithTag("Actionable"))
        {
            float distanceToObject = Vector2.Distance(obj.transform.position, rb.transform.position);
            if(distanceToObject < 3 && distanceToObject < minimal)
            {
                Debug.Log("wow");
                objectToTalk = obj; 
                pressE.text = "press E to talk with " + objectToTalk.ToString();
            } else
            {
                pressE.text = "";
                // Camera.mainCamera.
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.tag == "Ground")
        {
            onGround = true;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Debug.Log("wow");
    }

}
