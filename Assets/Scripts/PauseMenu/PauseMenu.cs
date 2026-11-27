using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Slider volumeSlider;
    private PlayerController player;

    private void Awake()
    {
        player = FindAnyObjectByType<PlayerController>();
        panel.SetActive(false);
        volumeSlider.SetValueWithoutNotify(AudioListener.volume);
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;
        if (keyboard.rKey.wasPressedThisFrame) RestartLevel();
        else if (keyboard.escapeKey.wasPressedThisFrame) SetPaused(!panel.activeSelf);
    }

    // 同步切换暂停面板、游戏时间和玩家控制。
    private void SetPaused(bool paused)
    {
        panel.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
        if (player) player.enabled = !paused;
        if (paused) resumeButton.Select();
    }

    private void OnDisable()
    {
        if (panel && panel.activeSelf) SetPaused(false);
    }

    public void ContinueGame() => SetPaused(false);

    public void RestartLevel()
    {
        SetPaused(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetVolume(float value) => AudioListener.volume = value;
}
