using UnityEngine;

public class DropArea : MonoBehaviour, ICardDropArea
{
    public void OnCardDrop(GameObject card)
    {

        // Opcional: mover la carta al centro del área
        card.transform.position = transform.position;
    }
}