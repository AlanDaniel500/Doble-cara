using UnityEngine;
using CardSystem;

public class CardDisplay : MonoBehaviour
{
    public CardData cardData;
    public SpriteRenderer cardRenderer; 

    public void LoadCardData()
    {
        if (cardData != null && cardRenderer != null)
        {
            cardRenderer.sprite = cardData.cardImage;
        }
    }
}