using CardSystem;
using UnityEngine;

public class ComboSystem : MonoBehaviour
{
    public CardData testCard;
    private CardData lastCard = null;

    void OnMouseDown()
    {
        PlayCard(testCard);
    }

    public void PlayCard(CardData currentCard)
    {
        if (lastCard != null && currentCard.cardNumber == lastCard.cardNumber)
        {
            Debug.Log($"Combo activado: dos cartas seguidas con número {currentCard.cardNumber}");
        }
        else
        {
            Debug.Log($"Carta jugada: {currentCard.cardName} (número {currentCard.cardNumber})");
        }

        lastCard = currentCard;
    }
}








