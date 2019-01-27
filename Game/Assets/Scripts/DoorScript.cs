using System.Collections;
using UnityEngine;

public class DoorScript : Interactive.ButtonInteractiveObject
{
    [SerializeField] private bool isOpen;
    [SerializeField] private Sprite openedDoor;
    [SerializeField] private Sprite closedDoor;
    [SerializeField] private float width;
    [SerializeField] private string toOpenInvite;
    [SerializeField] private string toCloseInvite;

    private SpriteRenderer spriteRenderer;
    private Transform child;
    private new Collider2D collider;
    
    protected override IEnumerator Interact()
    {
        StartInteracting();
        yield return new WaitForSeconds(0.2f);
        triggerObject.GetComponent<SimpleWalker>().Click();
        yield return new WaitForSeconds(0.3f);
        IsOpen = !IsOpen;
        yield return new WaitForSeconds(0.5f);
        FinishInteracting();
    }

    public bool IsOpen
    {
        private get { return isOpen; }
        set
        {
            isOpen = value;

            spriteRenderer.sprite = isOpen ? openedDoor : closedDoor;
            child.transform.position += (isOpen ? -1 : 1) * width * Vector3.right;
            collider.isTrigger = isOpen;
            InviteText = isOpen ? toCloseInvite : toOpenInvite;
        }
    }

    private void Awake()
    {
        child = transform.GetChild(0);
        spriteRenderer = child.GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
    }
}
