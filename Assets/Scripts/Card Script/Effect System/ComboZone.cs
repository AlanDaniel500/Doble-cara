using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class ComboZone : MonoBehaviour
{
   public List<CardData> comboCards = new List<CardData>();
   

    public void AddCard(CardData card)
    {
        comboCards.Add(card);
        Debug.Log($"Card {card.cardName} added to ComboZone.");
    }


    public void RemoveCard(CardData card)
    {
        if (comboCards.Contains(card))
        {
            comboCards.Remove(card);
            Debug.Log($"Card {card.cardName} removed from ComboZone.");
        }
    }

    public List<CardData> GetComboCards()
    {
        return comboCards;
    }

    public void ClearCombo()
    {
        comboCards.Clear();
    }


}
