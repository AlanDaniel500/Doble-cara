using UnityEngine;
using System.Collections.Generic;
using CardSystem;
using System;
using UnityEditor;

public class HandManager : MonoBehaviour
{
    public DeckManager deckManager; 

    public Transform handTransform; // Transform de la mano donde se colocarán las cartas
    public float fanSpread = 7.5f; // Ángulo de separación entre cartas
    public float cardSpacing = 100f; // Espacio entre cartas en la mano
    public float verticalSpacing = 100f; // Espacio vertical entre cartas

    public List<GameObject> cardsInHand = new List<GameObject>(); // Lista para almacenar las cartas en la mano


    public void AddCardToHand(CardData cardData)
    {
        GameObject newCard = new GameObject("Card_" + cardData.cardName);
        newCard.transform.SetParent(handTransform, false);
        newCard.transform.position = handTransform.position;

        // 1) Visual
        var renderer = newCard.AddComponent<SpriteRenderer>();
        renderer.sprite = cardData.cardImage;

        // 2) Collider
        var box = newCard.AddComponent<BoxCollider2D>();
        box.size = renderer.bounds.size;

        // 3) Draggable
        newCard.AddComponent<CardDraggable>();

        // 4) CardInfo para luego recuperar CardData
        var info = newCard.AddComponent<CardInfo>();
        info.data = cardData;

        cardsInHand.Add(newCard);
        UpdateHandVisuals();
    }



    public void RemoveCardFromHand(GameObject card)
    {
        if (cardsInHand.Contains(card))
        {
            cardsInHand.Remove(card);
            GameManager.Instance.DeckManager?.RemoveFromHand();
            UpdateHandVisuals();
        }
    }

    private void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));

            float normalizePosition = (2f * i) / (cardCount - 1) - 1f;
            float verticalOffset = (verticalSpacing * (1 *  normalizePosition * normalizePosition));
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
        }

    }
}