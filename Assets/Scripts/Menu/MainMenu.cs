using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}

