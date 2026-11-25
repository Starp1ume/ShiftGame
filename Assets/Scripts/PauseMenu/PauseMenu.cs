using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
#if ENABLE_INPUT_SYSTEM
        bool escapePressed = Keyboard.current?.escapeKey.wasPressedThisFrame == true;
#else
        bool escapePressed = Input.GetKeyDown(KeyCode.Escape);
#endif

        if (escapePressed)
        {
            SetPaused(!panel.activeSelf);
        }
    }

    private void SetPaused(bool paused)
    {
        panel.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
    }

    public void ContinueGame()
    {
        SetPaused(false);
    }

    public void RestartLevel()
    {
        SetPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}