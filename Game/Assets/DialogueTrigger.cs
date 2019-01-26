using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public static GameObject dialogueContainer;
    public static Text text;
    public static Queue<string> sentences;

    public static void TriggerDialogue(string[] dialogue, GameObject dialContainer, Text txt)
    {
        State.sentences = new Queue<string>();
        foreach (string sentence in dialogue)
        {
            State.sentences.Enqueue(sentence);
        }
        dialogueContainer = dialContainer;
        text = txt;
        dialogueContainer.SetActive(true);
        text.text = State.sentences.Dequeue();
        State.isDialogue = true;
    }

    public void NextDialogue()
    {
        if (State.sentences.Count == 0)
        {
            dialogueContainer.SetActive(false);
            State.isDialogue = false;
            return;
        }
        text.text = State.sentences.Dequeue();
    }

}
