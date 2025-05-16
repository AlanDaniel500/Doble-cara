using System;
using UnityEditor;
using UnityEngine;

public class DropArea : MonoBehaviour, ICardDropArea
{
    public void OnCardDrop(GameObject card)
    {
        Debug.Log("Carta soltada en zona de drop: " + card.name);


        //Fijar la posición en el centro del DropArea
        card.transform.position = transform.position;

        // Desactivar su capacidad de ser movida
        var drag = card.GetComponent<CardDraggable>();
        if (drag != null)
            Destroy(drag);

        // Removerla de la mano
        var hand = FindFirstObjectByType<HandManager>();
        if (hand != null)
            hand.RemoveCardFromHand(card);
    }
}
