using System.Collections.Generic;
using UnityEngine;
using CardData;

public class CardGameManager : MonoBehaviour
{
    public List<Card> allCards; // Asignás las ScriptableObjects acá
    public GameObject cardPrefab;
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    public int playerScore = 0;
    public int enemyScore = 0;

    public void DrawCard()
    {
        if (allCards.Count == 0) return;

        var randomCard = allCards[Random.Range(0, allCards.Count)];

        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i])
            {
                GameObject cardGO = Instantiate(cardPrefab, cardSlots[i].position, Quaternion.identity);
                cardGO.GetComponent<CardDisplay>().SetData(randomCard);

                ApplyCardEffect(randomCard);

                availableCardSlots[i] = false;
                allCards.Remove(randomCard);
                return;
            }
        }
    }

    private void ApplyCardEffect(Card card)
    {
        if (card.cardType.Contains(CardType.GainPoints))
        {
            playerScore += card.pointsGained;
            Debug.Log($"Ganas {card.pointsGained} puntos. Total: {playerScore}");
        }
        else if (card.cardType.Contains(CardType.StealPoints))
        {
            enemyScore -= card.pointsStolen;
            Debug.Log($"El rival pierde {card.pointsStolen} puntos. Total: {enemyScore}");
        }

        if (playerScore >= 15)
        {
            Debug.Log("¡Ganaste la partida!");
        }
    }
}
