using UnityEngine;
using UnityEngine.UI;
using CardSystem;

public class CardVisual : MonoBehaviour
{
    public Image image;
    public Text nameText;
    public CardData cardData; // Guarda los datos originales

    public void Setup(CardData data)
    {
        cardData = data;
        nameText.text = data.cardName;
        image.sprite = data.cardImage;
    }
}
