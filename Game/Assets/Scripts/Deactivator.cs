using UnityEngine;

public class Deactivator : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToDeactivate;

    private void Awake()
    {
        foreach (var obj in objectsToDeactivate)
        {
            obj.SetActive(false);
        }
    }
}