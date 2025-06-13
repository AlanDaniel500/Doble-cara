using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConditionManager : MonoBehaviour
{
    [Header("Victory Panel")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button exitVictoryButton;

    [Header("Defeat Panel")]
    [SerializeField] private GameObject defeatPanel;
    [SerializeField] private Button tryAgainButton;
    [SerializeField] private Button exitDefeatButton;

    void Start()
    {
        // Ocultamos ambos paneles al inicio
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);

        // Listeners de los botones
        exitVictoryButton.onClick.AddListener(ExitToMenu);
        exitDefeatButton.onClick.AddListener(ExitToMenu);
        tryAgainButton.onClick.AddListener(RestartLevel);
        nextLevelButton.onClick.AddListener(NextLevel);

        // Aquí elegís mostrar derrota o victoria según una condición.
        // Como esta escena es solo derrota, mostramos solo derrota:
        ShowDefeatPanel();
    }

    public void ShowVictoryPanel()
    {
        victoryPanel.SetActive(true);
        defeatPanel.SetActive(false);
    }

    public void ShowDefeatPanel()
    {
        defeatPanel.SetActive(true);
        victoryPanel.SetActive(false);
    }

    private void ExitToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene("GAME");
    }

    private void NextLevel()
    {
        SceneManager.LoadScene("MejorasScene");
    }
}

