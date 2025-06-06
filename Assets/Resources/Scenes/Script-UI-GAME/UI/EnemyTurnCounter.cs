using UnityEngine;
using TMPro;

public class EnemyTurnCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private int maxTurns = 2;

    private int currentTurn;

    void Start()
    {
        ResetCounter();
    }

    public void OnPlayerTurnEnd()
    {
        currentTurn--;

        if (currentTurn <= 0)
        {
            TriggerEnemyAction(); // Acá se ataca o hace algo
            ResetCounter();
        }

        UpdateText();
    }

    private void TriggerEnemyAction()
    {
        Debug.Log("¡El enemigo ataca!");
        // Acá va la lógica del ataque enemigo
    }

    private void ResetCounter()
    {
        currentTurn = maxTurns;
        UpdateText();
    }

    private void UpdateText()
    {
        turnText.text = currentTurn.ToString();
    }

    // Podés usar esto si necesitás cambiar la dificultad por nivel
    public void SetMaxTurns(int newMax)
    {
        maxTurns = newMax;
        ResetCounter();
    }
}
