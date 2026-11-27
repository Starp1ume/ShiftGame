using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject doorbody;

    public bool IsOpen => !doorbody.activeSelf;

    // 开门时同时移除门体显示和碰撞。
    public void OpenDoor() => doorbody.SetActive(false);
}
