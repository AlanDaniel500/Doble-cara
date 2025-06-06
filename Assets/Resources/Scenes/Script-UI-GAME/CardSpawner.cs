using System.Collections.Generic;
using UnityEngine;
using CardSystem;

public class CardSpawner : MonoBehaviour
{
    public Transform manoJugadorPanel;
    public GameObject cartaPrefab;
    public string cardDataFolder = "Cards"; // carpeta dentro de Resources

    public int cartasIniciales = 8;

    private void Start()
    {
        CargarCartasIniciales();
    }

    void CargarCartasIniciales()
    {
        CardData[] todasLasCartas = Resources.LoadAll<CardData>(cardDataFolder);

        if (todasLasCartas.Length == 0)
        {
            Debug.LogWarning("No se encontraron cartas en Resources/" + cardDataFolder);
            return;
        }

        for (int i = 0; i < cartasIniciales; i++)
        {
            CardData carta = todasLasCartas[Random.Range(0, todasLasCartas.Length)];
            GameObject nuevaCarta = Instantiate(cartaPrefab, manoJugadorPanel);
            CardDisplay display = nuevaCarta.GetComponent<CardDisplay>();
            display.CardData = carta;
            display.LoadCardData();
        }
    }
}
