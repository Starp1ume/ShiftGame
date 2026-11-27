using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] private string firstScene;

    // 标题页与通关页共用，跳转目标由各场景配置。
    public void StartGame()
    {
        SceneManager.LoadScene(firstScene);
    }
}