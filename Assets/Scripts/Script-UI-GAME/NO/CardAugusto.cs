using UnityEngine;
using UnityEngine.UI;
using CardSystem;

public class CardAugusto : MonoBehaviour
{
    public Image artworkImage;
    public TMPro.TextMeshProUGUI titleText;

    public void Setup(CardData data)
    {
        titleText.text = data.cardName;
        artworkImage.sprite = data.cardImage; // Cambio hecho acá
    }
}
