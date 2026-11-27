using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject activeMark;
    [SerializeField] private Transform respawnPoint;

    public Vector2 RespawnPosition => respawnPoint ? respawnPoint.position : transform.position;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out _))
            GameManager.Current.SaveCheckpoint(this);
    }

    public void ShowActive(bool active) => activeMark.SetActive(active);
}
