using System;
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

        [SerializeField] private string triggerButton;

        [SerializeField] private GameObject canvasObject;

        [SerializeField] private GameObject inviteTextPrefab;

        [SerializeField] private string inviteText;

        [SerializeField] private Vector2 textOffset;

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
                
                if (currentText != null)
                    Debug.Log(currentText.transform.position + " " + transform.position);
                
                if ((triggerObject.transform.position - transform.position).magnitude < triggerDistance && !triggered)
                {
                    Debug.Log("Triggered");
                    triggered = true;

                    yield return new WaitForSeconds(0.1f);
                    currentText = Instantiate(inviteTextPrefab, TextPosition, Quaternion.identity,
                        canvasObject.transform);
                    currentText.GetComponent<Text>().text = inviteText;
                    currentText.SetActive(true);
                    
                    Debug.Log("Created: " + currentText);
                }

                if ((triggerObject.transform.position - transform.position).magnitude > triggerDistance && triggered)
                {
                    Debug.Log("Untriggered!");
                    triggered = false;
                    
                    Destroy(currentText);
                    currentText = null;
                }

                if (triggered && Input.GetButton(triggerButton))
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
                Vector3 position = transform.position + (Vector3)textOffset;
                Vector2 textPosition = Camera.main.WorldToScreenPoint(position);

                return textPosition;
            }
        }
        
    }
}