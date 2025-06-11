using UnityEngine;
using CardSystem;

public class CardSpawner : MonoBehaviour
{
    public Transform spawnZone; // Objeto vacío donde aparecerán las cartas
    public string cardDataFolder = "Cards";
    public int cartasIniciales = 8;
    public float offsetX; //Ajusta el espacio entre las cartas


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
            CrearCartaEnMundo(carta, i);
        }
    }

    void CrearCartaEnMundo(CardData data, int index)
    {
        GameObject cartaGO = new GameObject("Carta_" + data.cardName);

        // Coordenadas absolutas
        float x = -10.03f + index * offsetX;
        float y = -7.82f;            // Altura donde aparecen las cartas

        cartaGO.transform.position = new Vector3(x, y, 0f);

        var sr = cartaGO.AddComponent<SpriteRenderer>();
        sr.sprite = data.cardImage;

        cartaGO.AddComponent<BoxCollider2D>();

        var info = cartaGO.AddComponent<CardInfo>();
        info.data = data;

        cartaGO.AddComponent<CardSelector>();
    }


}
