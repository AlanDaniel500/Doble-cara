using UnityEngine;
using System.Collections.Generic;
using CardSystem;

public class ComboArea : MonoBehaviour, ICardDropArea
{
    private List<CardData> cardsInCombo = new();
    private List<GameObject> cardObjects = new();
    public EnemyAI enemy;
    public PlayerStats player;

    // Efectos especiales activados
    private bool efectoATR = false;
    private bool efectoParca = false;
    private bool efectoTriada = false;
    //private bool efectoMejoraAbismo = false;
    //private bool efectoDebilitaPandemonio = false;


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
        ActivarEfectosPorNumero();

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
                Debug.Log("Combo: Gemelos (x2): 10 de daño");
                totalDamage += 10;
            }
            else if (count == 3)
            {
                Debug.Log(" Combo: Cerbero (x3): 20 de daño");
                totalDamage += 20;
            }
            else if (count == 4)
            {
                Debug.Log(" Combo: Riders (x4): 40 de daño");
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
                        Debug.Log("Combo: GRAN DOLOR (4 Sangre): 75 de daño");
                        totalDamage += 75;
                        //player.Heal(75); 
                        break;

                    case CardData.CardType.Oscuridad:
                        Debug.Log("Combo: ABISMO (4 Oscuridad): 250 de daño");
                        totalDamage += 250;
                        break;

                    case CardData.CardType.Muerte:
                        Debug.Log("Combo: PANDEMONIO (4 Muerte): 100 de daño");
                        totalDamage += 100;
                        break;
                }
            }
        }


        // Aplicar daño si hay algo
        if (efectoATR)
        {
            Debug.Log("ATR efecto aplicado: daño +20%");
            totalDamage = Mathf.RoundToInt(totalDamage * 1.2f);
            efectoATR = false;
        }

        if (efectoParca)
        {
            if (totalDamage % 2 == 0)
            {
                Debug.Log("Parca mejora combos pares: daño +25%");
                totalDamage = Mathf.RoundToInt(totalDamage * 1.25f);
            }
            efectoParca = false;
        }

        if (efectoTriada)
        {
            Debug.Log("Triada: daño x3");
            totalDamage *= 3;
            efectoTriada = false;
        }

        if (totalDamage > 0)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(totalDamage);
                Debug.Log("Daño total aplicado: " + totalDamage);
            }
        }

        // Limpiar cartas
        Clear();
    }

    private void ActivarEfectosPorNumero()
    {
        Dictionary<int, int> numeroCount = new();

        foreach (var card in cardsInCombo)
        {
            if (!numeroCount.ContainsKey(card.cardNumber))
                numeroCount[card.cardNumber] = 1;
            else
                numeroCount[card.cardNumber]++;
        }

        foreach (var pair in numeroCount)
        {
            int numero = pair.Key;
            int cantidad = pair.Value;

            if (numero == 1 && cantidad >= 2 && cantidad <= 4)
            {
                efectoATR = true;
                Debug.Log(" ATR activado: siguiente combo será mejorado");
            }
            else if (numero == 2 && cantidad >= 2 && cantidad <= 4)
            {
                efectoParca = true;
                if (player != null)
                    player.TakeDamage(20); // te hace daño
                Debug.Log("Parca activado: -20 de vida, mejora combos pares");
            }
            else if (numero == 3 && cantidad >= 2 && cantidad <= 4)
            {
                efectoTriada = true;
                Debug.Log(" Triada activado: siguiente combo será triplicado");
            }
        }
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
