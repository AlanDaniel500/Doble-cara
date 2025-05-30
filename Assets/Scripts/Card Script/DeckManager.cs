using NUnit.Framework;
using CardSystem;
using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.Rendering.GPUSort;
using UnityEditor;

public class DeckManager : MonoBehaviour
{
    public List<CardData> allCards = new List<CardData>();

    [Header("Configuración de mano")]
    public int startingHandSize = 3;
    public int maxHandSize = 10;
    public int currentHandSize = 0;

    private int currentIndex = 0;
    private bool isInitialized = false;

    private void Start()
    {
        CardData[] cards = Resources.LoadAll<CardData>("Cards");

        allCards.AddRange(cards);

    }

    public void InitializeDeck()
    {
        if (isInitialized) return;

        CardData[] cards = Resources.LoadAll<CardData>("Cards");

        if (cards.Length == 0)
        {
            Debug.LogWarning("No se encontraron cartas en Resources/Cards");
        }

        allCards.Clear();
        allCards.AddRange(cards);

        ShuffleDeck(); //baraja las cartas al iniciar

        isInitialized = true;
    }


    private void ShuffleDeck()
    {
        for (int i = 0; i < allCards.Count; i++)
        {
            int randomIndex = Random.Range(i, allCards.Count);
            (allCards[i], allCards[randomIndex]) = (allCards[randomIndex], allCards[i]);
        }
    }

    public CardData DrawCardForAI()
    {
        if (allCards.Count == 0) return null;

        int index = Random.Range(0, allCards.Count);
        return allCards[index];
    }

    public void DealStartingHand(HandManager handManager)
    {
        InitializeDeck();


    for (int i = 0; i < startingHandSize; i++)
        {
            DrawCard(handManager);
        }
    }

    public void DrawCard(HandManager handManager)
    {
        if (allCards.Count == 0)
        {
            Debug.LogWarning("No hay más cartas en el mazo.");
            return;
        }

        if (currentHandSize >= maxHandSize)
        {
            Debug.Log("La mano ya está llena.");
            return;
        }

        CardData nextCard = allCards[currentIndex];
        handManager.AddCardToHand(nextCard);
        currentHandSize++;

        currentIndex = (currentIndex + 1) % allCards.Count;
    }

    public void DealStartingHand(HandManager handManager, int minimumCards)
    {
        for (int i = 0; i < minimumCards; i++)
        {
            DrawCard(handManager);
        }
    }


    public void RemoveFromHand()
    {
        if (currentHandSize > 0)
            currentHandSize--;
    }

}