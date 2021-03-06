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

        [SerializeField] private Color initialTextColor;

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

                    yield return new WaitForSeconds(0.1f);
                    currentText = Instantiate(inviteTextPrefab, TextPosition, Quaternion.identity,
                        canvasObject.transform);
                    var text = currentText.GetComponent<Text>();
                    text.text = InviteText;
                    text.color = initialTextColor;
                    currentText.SetActive(true);

                    StartCoroutine(UpdateCurrentTextPosition());
                    
                    Debug.Log("Created: " + currentText);
                }

                if ((triggerObject.transform.position - transform.position).magnitude > triggerDistance && triggered)
                {
                    Debug.Log("Untriggered!");
                    triggered = false;
                    
                    StopCoroutine(nameof(UpdateCurrentTextPosition));
                    
                    Destroy(currentText);
                    currentText = null;
                }

                if (triggered && Input.GetButton(triggerButton))
                {
                    Debug.Log("Interact");
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


        public string InviteText
        {
            get { return inviteText; }
            set
            {
                inviteText = value;
                if (currentText != null)
                    currentText.GetComponent<Text>().text = inviteText;
            }
        }

        public Color TextColor
        {
            get { return currentText.GetComponent<Text>().color; }
            set { currentText.GetComponent<Text>().color = value; }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position + (Vector3)textOffset, Vector3.one * 0.5f);
        }

        IEnumerator UpdateCurrentTextPosition()
        {
            while (true)
            {
                if (currentText != null)
                    currentText.gameObject.transform.position = TextPosition;

                yield return null;
            }
        }

        protected void DestroyThisObject()
        {
            if (currentText != null)
                Destroy(currentText);
            
//            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}