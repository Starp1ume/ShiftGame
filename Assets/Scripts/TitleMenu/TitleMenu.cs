using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    [SerializeField] private string firstScene;

    public void StartGame()
    {
        SceneManager.LoadScene(firstScene);
    }
}