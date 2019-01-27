using System;
using System.Collections;
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

        private FaceApi.FaceResult faceResult;
        
        private Text userTip;

        private bool isWaiting;
        
        bool waitingForRequest;
        
        protected override IEnumerator Interact()
        {
            StartInteracting();

            yield return new WaitForSeconds(0.1f);
            
            faceObject.SetActive(true);
            timeSlider.gameObject.SetActive(true);

            userTip.text = playerTip;
            timeSlider.maxValue = playerTimeForAntics;

            StartCoroutine(Wait());
            
            yield return new WaitWhile(() => isWaiting);

            waitingForRequest = true;
            StartCoroutine(faceScript.AnalyzePicture());
            userTip.text = playerWaitingTip;
            yield return new WaitWhile(() => waitingForRequest);

            userTip.text = $"Anger: {faceResult.faceAttributes.emotion.anger:F3}";
            
            yield return new WaitForSeconds(3f);
            
            faceObject.SetActive(false);
            timeSlider.gameObject.SetActive(false);
            
            yield return new WaitForSeconds(0.1f);
            
            DestroyThisObject();
            
            FinishInteracting();
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