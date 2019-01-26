using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Interactive
{
    public abstract class ButtonInteractiveObject : MonoBehaviour
    {
        [SerializeField] protected GameObject triggerObject;

        [SerializeField] private float triggerDistance;

        [SerializeField] private KeyCode triggerButton;

        [SerializeField] private GameObject canvasObject;

        [SerializeField] private GameObject inviteTextPrefab;

        [SerializeField] private string inviteText;

        protected abstract IEnumerator Interact();

        private bool triggered;

        private bool interacting;

        protected void StartInteracting()
        {
            if (currentText != null)
                currentText.SetActive(false);
            
            interacting = true;
        }

        protected void FinishInteracting()
        {
            if (currentText != null)
                currentText.SetActive(true);
            
            interacting = false;
        }

        protected GameObject currentText;

        private void Start()
        {
            StartCoroutine(TrackObject());
        }

        IEnumerator TrackObject()
        {
            while (true)
            {
                yield return new WaitWhile(() => interacting);
                
                if ((triggerObject.transform.position - transform.position).magnitude < triggerDistance && !triggered)
                {
                    Debug.Log("Triggered");
                    triggered = true;

                    currentText = Instantiate(inviteTextPrefab, TextPosition, Quaternion.identity,
                        canvasObject.transform);
                    currentText.GetComponent<Text>().text = inviteText;
                    
                    Debug.Log("Created: " + currentText);
                }

                if ((triggerObject.transform.position - transform.position).magnitude > triggerDistance && triggered)
                {
                    Debug.Log("Untriggered!");
                    triggered = false;
                    
                    Destroy(currentText);
                    currentText = null;
                }

                if (triggered && Input.GetKey(triggerButton))
                {
                    StartCoroutine(Interact());
                }

                yield return null;
            }
        }


        protected Vector2 TextPosition
        {
            get
            {
                Vector3 position = transform.position + Vector3.up;
                Vector2 textPosition = Camera.main.WorldToScreenPoint(position);

                return textPosition;
            }
        }
        
    }
}