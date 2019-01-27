using System.Collections;
using Interactive;
using UnityEngine;
using UnityEngine.UI;

namespace Doctor
{
    [RequireComponent(typeof(DialogScript))]
    public class SpeakingDoctor : Interactive.ButtonInteractiveObject
    {
        [SerializeField] private GameObject nextStep;
        [SerializeField] private Color textColor;
        [SerializeField] private bool isLooping;
        [SerializeField] private bool nextStepAfterFirstInteraction;

        private DialogScript dialog;

        private string current;
        
        protected override IEnumerator Interact()
        {
            StartInteracting();
            
            triggerObject.GetComponent<SimpleWalker>().enabled = false;
            
            yield return new WaitForSeconds(0.1f);
            
            currentText.SetActive(true);
            string prevText = InviteText;
            Color prevColor = TextColor;
            TextColor = textColor;
            
            if (current == null || !isLooping)
                current = dialog.nextPhrase();
                
            bool destroy = !isLooping && dialog.endDialog;
            
            Debug.Log(destroy + " " + current);
            InviteText = current;
            
            yield return new WaitForSeconds(2f);

            InviteText = prevText;
            TextColor = prevColor;

            triggerObject.GetComponent<SimpleWalker>().enabled = true;

            if (destroy || nextStepAfterFirstInteraction)
            {
                if (!nextStep.activeSelf)
                    nextStep.SetActive(true);
                else if (!nextStep.GetComponent<ButtonInteractiveObject>().enabled)
                    nextStep.GetComponent<ButtonInteractiveObject>().enabled = true;
                
                if (destroy)
                    DestroyThisObject();
            }

            FinishInteracting();
        }

        private void Awake()
        {
            dialog = GetComponent<DialogScript>();
        }

        public void SetText(params string[] str)
        {
            dialog = GetComponent<DialogScript>();
            
            dialog.textScript = new TextScript(str);
            dialog.maxPhrase = str.Length - 1;
        }
    }
}