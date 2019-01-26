using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DoorScript : Interactive.ButtonInteractiveObject
{
    [SerializeField] private bool isOpen;
    [SerializeField] private Sprite openedDoor;
    [SerializeField] private Sprite closedDoor;

    private SpriteRenderer spriteRenderer;
    
    protected override IEnumerator Interact()
    {
        yield return new WaitForSeconds(0.2f);
        IsOpen = !IsOpen;
    }

    public bool IsOpen
    {
        private get { return isOpen; }
        set
        {
            isOpen = value;

            spriteRenderer.sprite = isOpen ? openedDoor : closedDoor;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
