using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class HandManagerAA : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab visual de la carta
    public Transform handZone;    // Zona donde se instancian las cartas
    public List<GameObject> currentHand = new List<GameObject>();

    public void AddCardToHand(CardData data)
    {
        GameObject newCard = Instantiate(cardPrefab, handZone);
        
        // Le pasamos la info del ScriptableObject a la carta visual
        CardAugusto cardScript = newCard.GetComponent<CardAugusto>();
        if (cardScript != null)
        {
            cardScript.Setup(data);
        }

        currentHand.Add(newCard);
    }

    public void ClearHand()
    {
        foreach (var card in currentHand)
        {
            Destroy(card);
        }
        currentHand.Clear();
    }
    
    public void RemoveCardFromHand(GameObject card)
    {
        if (currentHand.Contains(card))
        {
            currentHand.Remove(card);
        }
    }

}
