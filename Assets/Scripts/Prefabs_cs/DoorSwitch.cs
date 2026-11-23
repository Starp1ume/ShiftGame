using UnityEngine;
public class DoorSwitch : MonoBehaviour
{
    //调用Door类的OpenDoor方法
    [SerializeField] private Door door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"进入开关：{other.name}，标签：{other.tag}");
        
        if (other.CompareTag("Player"))
        {
            door.OpenDoor();
        }
    }
}