using UnityEngine;

public class DropArea : MonoBehaviour, ICardDropArea
{
    public void OnCardDrop(GameObject card)
    {
        Debug.Log("Carta soltada: " + card.name);
        card.transform.position = transform.position;
        Destroy(card.GetComponent<CardDraggable>());

        // quitar de la mano...
        FindFirstObjectByType<HandManager>()?.RemoveCardFromHand(card);

        // Recuperar CardData desde CardInfo
        var info = card.GetComponent<CardInfo>();
        if (info != null && info.data != null)
        {
            EnemyAI enemy = FindFirstObjectByType<EnemyAI>();
            if (enemy != null)
                enemy.TakeDamage(info.data.damage);
        }
        else
        {
            Debug.LogError("DropArea: no encontré CardInfo o data en " + card.name);
        }
    }
}
