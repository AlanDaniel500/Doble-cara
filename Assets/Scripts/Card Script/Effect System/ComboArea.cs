using UnityEngine;
using System.Collections.Generic;
using CardSystem;

public class ComboArea : MonoBehaviour, ICardDropArea
{
    private List<CardData> cardsInCombo = new();
    private List<GameObject> cardObjects = new();

    public EnemyAI enemy;

    public void OnCardDrop(GameObject card)
    {
        card.transform.position = transform.position;

        // No destruir el ComboDrag. Así puede sacarse.

        FindFirstObjectByType<HandManager>()?.RemoveCardFromHand(card);

        var info = card.GetComponent<CardInfo>();
        if (info != null && info.data != null && !cardsInCombo.Contains(info.data))
        {
            cardsInCombo.Add(info.data);
            cardObjects.Add(card);
        }

        Debug.Log("Carta colocada en ComboArea: " + card.name);
    }

    public int GetCardCount()
    {
        return cardsInCombo.Count;
    }

    public CardData GetSingleCard()
    {
        return cardsInCombo.Count == 1 ? cardsInCombo[0] : null;
    }

    public void RemoveCard(GameObject card)
    {
        var info = card.GetComponent<CardInfo>();
        if (info == null || info.data == null) return;

        if (cardsInCombo.Contains(info.data))
        {
            cardsInCombo.Remove(info.data);
            cardObjects.Remove(card);

            // Volver a la mano
            FindFirstObjectByType<HandManager>()?.AddCardToHand(info.data);
            Destroy(card);
        }
    }

    public void CheckCombo()
    {
        if (cardsInCombo.Count == 0)
        {
            Debug.Log("No hay cartas en el ComboArea.");
            return;
        }

        Dictionary<int, int> numberCount = new();
        Dictionary<CardData.CardType, int> typeCount = new();
        int totalDamage = 0;

        foreach (var card in cardsInCombo)
        {
            // Contar por número
            if (!numberCount.ContainsKey(card.cardNumber))
                numberCount[card.cardNumber] = 1;
            else
                numberCount[card.cardNumber]++;

            // Contar por tipo
            if (!typeCount.ContainsKey(card.cardType))
                typeCount[card.cardType] = 1;
            else
                typeCount[card.cardType]++;
        }

        // Combos por número
        foreach (var pair in numberCount)
        {
            int count = pair.Value;

            if (count == 2)
            {
                Debug.Log("Combo: Gemelos (x2) ? 10 de daño");
                totalDamage += 10;
            }
            else if (count == 3)
            {
                Debug.Log(" Combo: Cerbero (x3) ? 20 de daño");
                totalDamage += 20;
            }
            else if (count == 4)
            {
                Debug.Log(" Combo: Riders (x4) ? 40 de daño");
                totalDamage += 40;
            }
        }

        // Combos por tipo
        foreach (var pair in typeCount)
        {
            var type = pair.Key;
            int count = pair.Value;

            if (count >= 4)
            {
                switch (type)
                {
                    case CardData.CardType.Sangre:
                        Debug.Log(" Combo: GRAN DOLOR (4 Sangre) ? 75 de daño");
                        totalDamage += 75;
                        break;

                    case CardData.CardType.Oscuridad:
                        Debug.Log(" Combo: ABISMO (4 Oscuridad) ? 250 de daño");
                        totalDamage += 250;
                        break;

                    case CardData.CardType.Muerte:
                        Debug.Log(" Combo: PANDEMONIO (4 Muerte) ? 100 de daño");
                        totalDamage += 100;
                        break;
                }
            }
        }

        // Aplicar daño si hay algo
        if (totalDamage > 0)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(totalDamage);
                Debug.Log(" Daño total aplicado: " + totalDamage);
            }
            else
            {
                Debug.LogWarning("No hay EnemyAI asignado.");
            }
        }
        else
        {
            Debug.Log(" No se activó ningún combo.");
        }

        // Limpiar cartas
        Clear();
    }


    public void Clear()
    {
        foreach (GameObject card in cardObjects)
        {
            Destroy(card); // Opcional: o devolverla al mazo
        }

        cardsInCombo.Clear();
        cardObjects.Clear();
    }
}
