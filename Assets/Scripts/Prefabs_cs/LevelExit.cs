using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string nextScene;
    private bool leaving;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (leaving || !other.TryGetComponent<PlayerController>(out _) || string.IsNullOrEmpty(nextScene)) return;
        // 切场前锁定出口，避免重复触发。
        leaving = true;
        GameManager.Current?.PlayLevelClear();
        SceneManager.LoadScene(nextScene);
    }
}
