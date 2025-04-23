using UnityEngine;

public class CenterCardDropArea : MonoBehaviour, ICardDropArea
{
    public void OnCardDrop(Card card)
    {
        card.transform.position = transform.position;
        Debug.Log("Card dropped in left area");
    }
}
