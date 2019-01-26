using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorEnterScript : Interactive.ButtonInteractiveObject
{
    [SerializeField] private KeyCode upButton;
    [SerializeField] private KeyCode downButton;
    [SerializeField] private KeyCode exitButton;
    [SerializeField] private float speed;
    [SerializeField] private int maxFloor;
    [SerializeField] private int minFloor;
    [SerializeField] private int currentFloor;
    [SerializeField] private float floorHeight;
    [SerializeField] private string tip;

    private Transform prevObjectParent;
    private bool isExit;
    private bool isRiding;
    private string prevText;
    
    protected override IEnumerator Interact()
    {
        StartInteracting();
        yield return new WaitForSeconds(0.1f);
        isExit = false;

        prevObjectParent = triggerObject.transform.parent;
        triggerObject.transform.parent = transform;
        triggerObject.GetComponent<SimpleWalker>().enabled = false;
        
        currentText.SetActive(true);
        prevText = currentText.GetComponent<Text>().text;
        currentText.GetComponent<Text>().text = tip;

        while (!isExit)
        {
            yield return new WaitWhile(() => isRiding);

            if (Input.GetKey(upButton) && currentFloor != maxFloor)
            {
                StartCoroutine(Ride(1));
                currentFloor++;
                continue;
            }
            
            if (Input.GetKey(downButton) && currentFloor != minFloor)
            {
                StartCoroutine(Ride(-1));
                currentFloor--;
                continue;
            }

            if (Input.GetKey(exitButton))
            {
                isExit = true;
            }

            yield return null;
        }

        currentText.GetComponent<Text>().text = prevText;
        triggerObject.GetComponent<SimpleWalker>().enabled = true;
        triggerObject.transform.parent = prevObjectParent;
        yield return new WaitForSeconds(0.1f);
        FinishInteracting();
    }

    private IEnumerator Ride(float dir)
    {
        isRiding = true;
        currentText.SetActive(false);
        
        float prevFloorY = transform.position.y;
        float destination = prevFloorY + dir * floorHeight;
        
        while (Mathf.Abs(transform.position.y - destination) > 0.0001)
        {
            float delta = dir * Mathf.Min(speed * Time.fixedDeltaTime, Mathf.Abs(transform.position.y - destination));
            transform.position += delta * Vector3.up;
            
            yield return new WaitForFixedUpdate();
        }

        currentText.transform.position = TextPosition;
        currentText.SetActive(true);
        isRiding = false;
    }
    
    
}
