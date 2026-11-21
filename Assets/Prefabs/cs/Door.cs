using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;

    [SerializeField] private GameObject doorbody;

    public void OpenDoor()
    {
        isOpen = true;
        doorbody.SetActive(false);
    }
}