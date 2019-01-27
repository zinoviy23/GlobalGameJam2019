using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Doctor
{
    public class AttackableDoctorScript : Interactive.ButtonInteractiveObject
    {
        [SerializeField] private FaceApi.FaceApiScript faceScript;
        [SerializeField] private Slider timeSlider;
        [SerializeField] private GameObject faceObject;
        [SerializeField] private string playerTip;
        [SerializeField] private float playerTimeForAntics;
        [SerializeField] private string playerWaitingTip;
        [SerializeField] private string formatString;
        [SerializeField] private Vector2 usersTextOffset;
        [SerializeField] private string simpleRoar;
        [SerializeField] private string[] goodAnswers;
        [SerializeField] private string[] badAnswers;
        [SerializeField] private string goodResultSpeech;
        [SerializeField] private string badResultSpeech;
        [SerializeField] private SpeakingDoctor lastDoctor;

        private FaceApi.FaceResult faceResult;
        
        private Text userTip;

        private bool isWaiting;
        
        bool waitingForRequest;

        private int counter = 0;
        
        protected override IEnumerator Interact()
        {
            StartInteracting();

            yield return new WaitForSeconds(0.1f);
            
            triggerObject.GetComponent<SimpleWalker>().enabled = false;
            
            currentText.SetActive(true);
            faceObject.SetActive(true);
            timeSlider.gameObject.SetActive(true);

            userTip.text = playerTip;
            timeSlider.maxValue = playerTimeForAntics;

            counter++;
            Vector3 position = currentText.transform.position;
            currentText.transform.position = UsersTextPosition;
            var text = currentText.GetComponent<Text>();
            string prevText = InviteText;
            InviteText = string.Concat(Enumerable.Repeat(simpleRoar, counter));
            
            StartCoroutine(Wait());
            
            yield return new WaitWhile(() => isWaiting);
            
            waitingForRequest = true;
            StartCoroutine(faceScript.AnalyzePicture());
            userTip.text = playerWaitingTip;
            yield return new WaitWhile(() => waitingForRequest);

            userTip.text = string.Format(formatString, faceResult.faceAttributes.emotion.anger);
            
            yield return new WaitForSeconds(3f);
            
            faceObject.SetActive(false);
            timeSlider.gameObject.SetActive(false);

            bool goToDoor = false;
            bool lastGood = false;
            currentText.transform.position = TextPosition;
            if (faceResult.faceAttributes.emotion.anger > 0.4)
            {
                goToDoor = true;
                InviteText = goodAnswers[counter - 1];
                lastGood = true;
            } 
            else if (counter == 3)
            {
                goToDoor = true;
                InviteText = badAnswers[counter - 1];
                lastGood = false;
            }
            else
            {
                InviteText = badAnswers[counter - 1];
            }
            
            yield return new WaitForSeconds(2f);
            
            currentText.SetActive(false);
            currentText.transform.position = position;
            InviteText = prevText;
            
            triggerObject.GetComponent<SimpleWalker>().enabled = true;
            
            yield return new WaitForSeconds(0.1f);

            if (goToDoor)
            {
                lastDoctor.SetText(lastGood ? goodResultSpeech : badResultSpeech);
                lastDoctor.gameObject.SetActive(true);
                DestroyThisObject();
            }

            FinishInteracting();
        }

        public Vector2 UsersTextPosition
        {
            get
            {
                Vector3 position = triggerObject.transform.position + (Vector3)usersTextOffset;
                Vector2 textPosition = Camera.main.WorldToScreenPoint(position);

                return textPosition;
            }
        }

        private void Awake()
        {
            userTip = timeSlider.transform.GetChild(2).GetComponent<Text>();
            
            FaceApi.FaceApiScript.OnRequest += (obj) =>
            {
                faceResult = obj;
                waitingForRequest = false;
            };
        }

        IEnumerator Wait()
        {
            isWaiting = true;

            timeSlider.value = 0;
            while (Math.Abs(timeSlider.value - timeSlider.maxValue) > 0.0001)
            {
                timeSlider.value += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }            
            
            isWaiting = false;
        }
    }
}