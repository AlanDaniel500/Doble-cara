using UnityEngine;
using System.Collections;
using CardSystem;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance;

    public bool isPlayerTurn = true;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        FindFirstObjectByType<EnemyAI>()?.InitializeAIHand();
    }

    public void EndTurn()
    {
        if (!isPlayerTurn) return;

        Debug.Log("Turno del jugador finalizado.");

        ComboArea comboArea = FindFirstObjectByType<ComboArea>();
        if (comboArea == null)
        {
            Debug.LogWarning("No se encontr� ComboArea.");
            return;
        }

        int count = comboArea.GetCardCount();

        if (count == 1)
        {
            // Una sola carta: da�o directo
            CardData card = comboArea.GetSingleCard();
            if (card != null)
            {
                FindFirstObjectByType<EnemyAI>()?.TakeDamage(card.damage);
                Debug.Log($"Da�o directo aplicado por carta: {card.cardName} ? {card.damage}");
            }
            comboArea.Clear();
        }
        else if (count > 1)
        {
            // 2 o m�s ? combo
            comboArea.CheckCombo();
        }
        else
        {
            Debug.Log("No se jug� ninguna carta este turno.");
        }

        isPlayerTurn = false;

        isPlayerTurn = false;
        StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        Debug.Log("Turno de la IA iniciado...");
        yield return new WaitForSeconds(1f); // Peque�a pausa

        var ai = FindFirstObjectByType<EnemyAI>();
        if (ai != null)
        {
            ai.TakeTurn();
        }

        yield return new WaitForSeconds(2f); // Espera por animaciones, etc

        StartPlayerTurn(); // Pasa el turno al jugador
    }

    public void StartPlayerTurn()
    {
        isPlayerTurn = true;
        Debug.Log("Turno del jugador iniciado.");
    }
}