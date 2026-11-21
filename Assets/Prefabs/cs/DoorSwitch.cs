using UnityEngine;
public class DoorSwitch : MonoBehaviour
{
    //调用Door类的OpenDoor方法
    [SerializeField] private Door door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.OpenDoor();
        }
    }
}