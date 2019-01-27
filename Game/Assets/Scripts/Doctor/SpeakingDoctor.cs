using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Doctor
{
    [RequireComponent(typeof(DialogScript))]
    public class SpeakingDoctor : Interactive.ButtonInteractiveObject
    {
        [SerializeField] private GameObject attackableDoctor;
        
        private DialogScript dialog;
        
        protected override IEnumerator Interact()
        {
            StartInteracting();
            
            triggerObject.GetComponent<SimpleWalker>().enabled = false;
            
            yield return new WaitForSeconds(0.1f);
            
            currentText.SetActive(true);
            string prevText = InviteText;

            string current = dialog.nextPhrase();

            bool destroy = false;
            if (current != null)
                InviteText = current;
            else
                destroy = true;
            
            if (!destroy)
                yield return new WaitForSeconds(2f);

            InviteText = prevText;

            triggerObject.GetComponent<SimpleWalker>().enabled = true;

            if (destroy)
            {
                attackableDoctor.SetActive(true);
                DestroyThisObject();
            }

            FinishInteracting();
        }

        private void Awake()
        {
            dialog = GetComponent<DialogScript>();
        }
    }
}