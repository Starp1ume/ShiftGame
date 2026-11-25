using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PauseMenu : MonoBehaviour
{
    // 暂停菜单面板
    [SerializeField] private GameObject panel;

    // 音量滑块
    [SerializeField] private Slider volumeSlider;

    private void Awake()
    {
        // 初始化暂停菜单面板
        panel.SetActive(false);

        // 初始化音量滑块
        volumeSlider.SetValueWithoutNotify(AudioListener.volume);
    }

    private void Update()
    {
#if ENABLE_INPUT_SYSTEM
        // 检查是否按下了 ESC 键
        bool escapePressed = Keyboard.current?.escapeKey.wasPressedThisFrame == true;
#else
        // 检查是否按下了 ESC 键
        // 并更新暂停菜单面板的状态
        bool escapePressed = Input.GetKeyDown(KeyCode.Escape);
#endif

        if (escapePressed)
        {
            SetPaused(!panel.activeSelf);
        }
    }

    // 设置游戏状态为暂停或继续
    private void SetPaused(bool paused)
    {
        panel.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
    }

    // 继续游戏
    public void ContinueGame()
    {
        SetPaused(false);
    }

    // 重新开始当前关卡
    public void RestartLevel()
    {
        SetPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 设置音量
    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}