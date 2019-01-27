using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ElevatorEnterScript : Interactive.ButtonInteractiveObject
{
    [SerializeField] private string upButtonAxis;
    [SerializeField] private string downButtonAxis;
    [SerializeField] private string exitButton;
    [SerializeField] private float speed;
    [SerializeField] private int maxFloor;
    [SerializeField] private int minFloor;
    [SerializeField] private int currentFloor;
    [SerializeField] private float floorHeight;
    [SerializeField] private string tip;
    [SerializeField] private string leftDoorName;
    [SerializeField] private string rightDoorName;
    [SerializeField] private string openedDoorLayer;
    [SerializeField] private string closedDoorLayer;
    [SerializeField] private string playerLayerInElevator;
    [SerializeField] private string playerLayer;
    [SerializeField] private float doorsOffset;
    
    private Transform prevObjectParent;
    private bool isExit;
    private bool isRiding;
    private int animatingCount;
    private string prevText;
    
    private SpriteRenderer leftDoorOnFloor;
    private SpriteRenderer rightDoorOnFloor;
    
    protected override IEnumerator Interact()
    {
        StartInteracting();
        prevObjectParent = triggerObject.transform.parent;
        triggerObject.transform.parent = transform;
        triggerObject.GetComponent<SimpleWalker>().enabled = false;

        StartCoroutine(AnimateDoor(leftDoorOnFloor, -doorsOffset, openedDoorLayer));
        StartCoroutine(AnimateDoor(rightDoorOnFloor, doorsOffset, openedDoorLayer));

        yield return new WaitWhile(() => animatingCount != 0);
        
        StartCoroutine(AnimateTriggerMoving(triggerObject, transform.position));

        triggerObject.GetComponent<SpriteRenderer>().sortingLayerName = playerLayerInElevator;
        
        yield return new WaitWhile(() => animatingCount != 0);
        
        yield return new WaitForSeconds(0.6f);
        
        StartCoroutine(AnimateDoor(leftDoorOnFloor, doorsOffset, closedDoorLayer));
        StartCoroutine(AnimateDoor(rightDoorOnFloor, -doorsOffset, closedDoorLayer));

        yield return new WaitWhile(() => animatingCount != 0);
        
        isExit = false;
        
        
        currentText.SetActive(true);
        prevText = InviteText;
        InviteText = tip;

        while (!isExit)
        {
            yield return new WaitWhile(() => isRiding);

            if (Input.GetAxisRaw(upButtonAxis) > 0.01 && currentFloor != maxFloor)
            {
                StartCoroutine(Ride(1));
                currentFloor++;
                UpdateDoors();
                continue;
            }
            
            if (Input.GetAxisRaw(downButtonAxis) < -0.01 && currentFloor != minFloor)
            {
                StartCoroutine(Ride(-1));
                currentFloor--;
                UpdateDoors();
                continue;
            }

            if (Input.GetButton(exitButton))
            {
                isExit = true;
            }

            yield return null;
        }

        StartCoroutine(AnimateDoor(leftDoorOnFloor, -doorsOffset, openedDoorLayer));
        StartCoroutine(AnimateDoor(rightDoorOnFloor, doorsOffset, openedDoorLayer));

        yield return new WaitWhile(() => animatingCount != 0);
        
        triggerObject.GetComponent<SpriteRenderer>().sortingLayerName = playerLayer;
        
        StartCoroutine(AnimateTriggerMoving(triggerObject, transform.position + Vector3.left * 1.1f));
        
        yield return new WaitForSeconds(0.6f);
        
        StartCoroutine(AnimateDoor(leftDoorOnFloor, doorsOffset, closedDoorLayer));
        StartCoroutine(AnimateDoor(rightDoorOnFloor, -doorsOffset, closedDoorLayer));

        yield return new WaitWhile(() => animatingCount != 0);
        
        InviteText = prevText;
        triggerObject.GetComponent<SimpleWalker>().enabled = true;
        triggerObject.transform.parent = prevObjectParent;
        yield return new WaitForSeconds(0.1f);
        FinishInteracting();
    }

    private void UpdateDoors()
    {
        var leftDoorObj = GameObject.Find($"{leftDoorName}{currentFloor}");
        var rightDoorObj = GameObject.Find($"{rightDoorName}{currentFloor}");
        
        leftDoorOnFloor = leftDoorObj != null ? leftDoorObj.GetComponent<SpriteRenderer>() : null;

        rightDoorOnFloor = rightDoorObj != null ? rightDoorObj.GetComponent<SpriteRenderer>() : null;
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
        yield return new WaitForSeconds(0.1f);
        currentText.SetActive(true);
        isRiding = false;
    }

    IEnumerator AnimateTriggerMoving(GameObject obj, Vector3 point, Wrapper wrapper = null)
    {
        animatingCount++;
        if (wrapper != null)
            wrapper.value = true;

        while (Math.Abs(obj.transform.position.x - point.x) > 0.001)
        {
            Vector3 moveResult = Vector3.MoveTowards(
                new Vector3(obj.transform.position.x, 0),
                new Vector3(point.x, 0),
                5 * Time.fixedDeltaTime);
            
            obj.transform.position = new Vector3(moveResult.x, obj.transform.position.y);
            
            yield return new WaitForFixedUpdate();
        }

        animatingCount--;
        if (wrapper != null)
            wrapper.value = false;
    }

    class Wrapper
    {
        public bool value = true;
    }

    IEnumerator AnimateDoor(SpriteRenderer door, float offset, string layer)
    {
        door.sortingLayerName = layer;
        
        Wrapper wrapper = new Wrapper();;
        StartCoroutine(AnimateTriggerMoving(door.gameObject, door.transform.position + Vector3.right * offset,
            wrapper));
        
        yield return new WaitWhile(() => wrapper.value);
    }

    private void Awake()
    {
        UpdateDoors();
    }
}
