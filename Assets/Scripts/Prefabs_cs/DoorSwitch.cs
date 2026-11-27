using UnityEngine;
using UnityEngine.Events;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private Door door;
    [SerializeField] private GameObject pressedMark;
    [SerializeField] private UnityEvent onPressed = new UnityEvent();
    private bool pressed;

    // 每次加载关卡后只触发一次，可用事件追加联动。
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pressed || !other.TryGetComponent<PlayerController>(out _)) return;
        pressed = true;
        if (pressedMark) pressedMark.SetActive(true);
        if (door) door.OpenDoor();
        GameManager.Current?.PlayDoor();
        onPressed.Invoke();
    }
}
