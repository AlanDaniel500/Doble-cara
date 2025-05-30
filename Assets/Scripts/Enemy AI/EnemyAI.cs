using UnityEngine;
using System.Collections.Generic;
using CardSystem;

public class EnemyAI : MonoBehaviour
{
    [Header("Salud del Enemigo")]
    public int maxHealth = 100;
    public int currentHealth;

    public HealtBar healthBar;
    public GameObject winScreen;

    [Header("Cartas")]
    public DeckManager deckManager;
    public int maxHandSize = 5;
    private List<CardData> hand = new();

    public PlayerStats player; // Referencia al jugador para aplicar daño
    public Transform handTransform;
    private List<GameObject> enemyCardObjects = new();

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        // Robar mano inicial
        for (int i = 0; i < maxHandSize; i++)
        {
            var card = deckManager.DrawCardForAI();
            if (card != null)
            {
                hand.Add(card);
                CreateCardVisual(card);
            }
        }

        LayoutEnemyHand(); // ? posicionar visualmente
    }
    private void Update()
    {
        EnemyDead();
    }




    private void LayoutEnemyHand()
    {
        for (int i = 0; i < enemyCardObjects.Count; i++)
        {
            float offset = (i - (enemyCardObjects.Count - 1) / 2f) * 1.5f;
            enemyCardObjects[i].transform.localPosition = new Vector3(offset, 0, 0);
            enemyCardObjects[i].transform.localRotation = Quaternion.identity;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"TakeDamage() recibido: {damage}");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void EnemyDead()
    {
        if (currentHealth <= 0)
        {
            winScreen.SetActive(true);
        }
    }

    public void InitializeAIHand()
    {
        hand.Clear();
        enemyCardObjects.Clear();

        for (int i = 0; i < maxHandSize; i++)
        {
            var card = deckManager.DrawCardForAI();
            if (card != null)
            {
                hand.Add(card);
                CreateCardVisual(card);
            }
        }

        LayoutEnemyHand();
    }

    private int EvaluarComboIA(List<CardData> mano)
    {
        Dictionary<int, int> contadorNumeros = new();
        int dañoTotal = 0;


        foreach (CardData carta in mano)
        {
            int numero = carta.cardNumber;
            if (!contadorNumeros.ContainsKey(numero))
                contadorNumeros[numero] = 1;
            else
                contadorNumeros[numero]++;
        }

        foreach (var par in contadorNumeros)
        {
            int cantidad = par.Value;

            if (cantidad == 2)
            {
                Debug.Log("IA hace combo: Gemelos (x2) ? 10 de daño");
                dañoTotal += 10;
            }
            else if (cantidad == 3)
            {
                Debug.Log("IA hace combo: Cerbero (x3) ? 20 de daño");
                dañoTotal += 20;
            }
            else if (cantidad >= 4)
            {
                Debug.Log("IA hace combo: Riders (x4+) ? 40 de daño");
                dañoTotal += 40;
            }
        }

        return dañoTotal;
    }


    private GameObject CreateCardVisual(CardData data)
    {
        GameObject card = new GameObject("EnemyCard_" + data.cardName);
        card.transform.SetParent(handTransform, false);
        card.transform.localPosition = Vector3.zero;

        // Imagen de la carta
        var renderer = card.AddComponent<SpriteRenderer>();
        renderer.sprite = data.cardImage;
        renderer.color = Color.gray;

        // Orden de render para que no se oculten unas con otras
        renderer.sortingOrder = enemyCardObjects.Count;

        // Guardar info
        var info = card.AddComponent<CardInfo>();
        info.data = data;

        enemyCardObjects.Add(card);
        return card;
    }



    private int GetNumeroMasFrecuente()
    {
        Dictionary<int, int> conteo = new();
        foreach (var c in hand)
        {
            if (!conteo.ContainsKey(c.cardNumber))
                conteo[c.cardNumber] = 1;
            else
                conteo[c.cardNumber]++;
        }

        int maxNumero = -1;
        int maxCantidad = 0;

        foreach (var par in conteo)
        {
            if (par.Value > maxCantidad)
            {
                maxCantidad = par.Value;
                maxNumero = par.Key;
            }
        }

        return maxNumero;
    }

    private void RemoveComboCards(int numero)
    {
        for (int i = hand.Count - 1; i >= 0; i--)
        {
            if (hand[i].cardNumber == numero)
            {
                Destroy(enemyCardObjects[i]);
                hand.RemoveAt(i);
                enemyCardObjects.RemoveAt(i);
            }
        }
    }


    public void TakeTurn()
    {
        Debug.Log("IA está tomando su turno.");

        // Robar hasta el máximo
        while (hand.Count < maxHandSize)
        {
            var card = deckManager.DrawCardForAI();
            if (card != null)
            {
                hand.Add(card);
                Debug.Log("IA robó: " + card.cardName);
                CreateCardVisual(card); // esto ya incluye orden de render
            }
        }

        // Acomodar visualmente
        LayoutEnemyHand();

        // Evaluar combo
        int dañoCombo = EvaluarComboIA(hand);

        if (dañoCombo > 0)
        {
            player.TakeDamage(dañoCombo);
            Debug.Log("IA aplicó combo por: " + dañoCombo + " de daño");

            // Eliminar solo cartas del combo (del mismo número más común)
            int numeroMasRepetido = GetNumeroMasFrecuente();
            RemoveComboCards(numeroMasRepetido);
        }
        else
        {
            // Jugar una carta aleatoria
            int index = Random.Range(0, hand.Count);
            CardData carta = hand[index];
            GameObject cartaGO = enemyCardObjects[index];

            hand.RemoveAt(index);
            enemyCardObjects.RemoveAt(index);

            cartaGO.transform.SetParent(null);
            cartaGO.transform.position = new Vector3(0, 0, 0);
            cartaGO.transform.localScale = Vector3.one * 1.2f;

            player.TakeDamage(carta.damage);
            Debug.Log("IA juega carta suelta: " + carta.cardName + " ? daño " + carta.damage);
        }
    }

}