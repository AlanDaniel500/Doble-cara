using UnityEngine;
using CardSystem;

public class CardSpawner : MonoBehaviour
{
    public GameObject cardPrefab; // El prefab visual
    public CardData cardData;     // El SO con los datos

    private void Start()
    {
        GameObject card = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity, transform);

        CardDisplay display = card.GetComponent<CardDisplay>();
        if (display != null)
        {
            display.cardData = cardData;
            display.LoadCardData();
        }
    }
}

