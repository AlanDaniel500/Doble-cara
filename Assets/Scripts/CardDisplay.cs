using UnityEngine;
using UnityEngine.UI;
using CardData;
public class CardDisplay : MonoBehaviour
{
    
    
    public Text nameText;
    public Image cardImage;
    public Text effectText;

    public void SetData(Card cardData)
    {
        nameText.text = cardData.cardName;
        cardImage.sprite = cardData.cardSprite;

        if (cardData.cardType.Contains(CardType.GainPoints))
        {
            effectText.text = $"+{cardData.pointsGained} puntos";
        }
        else if (cardData.cardType.Contains(CardType.StealPoints))
        {
            effectText.text = $"-{cardData.pointsStolen} puntos al rival";
        }
    }
}
