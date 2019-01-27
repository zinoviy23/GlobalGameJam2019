using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SpeakingCat
{
    [RequireComponent(typeof(DialogScript))]
    public class SpeakingCat : Interactive.ButtonInteractiveObject
    {
        //[SerializeField] private GameObject attackableDoctor;
        
        [SerializeField] public GameObject train;
        [SerializeField] public GameObject cat;

        private DialogScript dialog;
        
        protected override IEnumerator Interact()
        {
            StartInteracting();
            
            triggerObject.GetComponent<SimpleWalker>().enabled = false;
            
            yield return new WaitForSeconds(0.1f);
            
            currentText.SetActive(true);
            //string prevText = InviteText;

            string current = dialog.nextPhrase();

            bool destroy = false;
            if (current != null)
                InviteText = current;
            else
                destroy = true;
            
            if (!destroy)
                yield return new WaitForSeconds(2f);

            //InviteText = prevText;

            triggerObject.GetComponent<SimpleWalker>().enabled = true;

            if (destroy)
            {
                //attackableDoctor.SetActive(true);
                train.GetComponent<BoxCollider2D>().isTrigger = true;
                cat.SetActive(true);
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