using NUnit.Framework;
using CardSystem;
using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<CardData> allCards = new List<CardData>();

    private int currentIndex = 0;


     private void Start()
    {
        CardData[] cards = Resources.LoadAll<CardData>("Cards");

        allCards.AddRange(cards);

    } 
   

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
            return;

        CardData nextCard = allCards[currentIndex];
        handManager.AddCardToHand(nextCard);
        currentIndex = (currentIndex + 1) % allCards.Count; // Ciclo a través de las cartas
    }
}