using System.Collections.Generic;
using UnityEngine;

public class ComboZonaManager : MonoBehaviour
{
    [SerializeField] private int maxCards = 4;
    [SerializeField] private float cardWidth = 190f;
    [SerializeField] private float cardHeight = 266f;
    [SerializeField] private float spacing = 10f;

    private List<GameObject> cartasEnZona = new List<GameObject>();
    private RectTransform zonaRect;

    private void Awake()
    {
        zonaRect = GetComponent<RectTransform>();
    }

    public bool TryAgregarCarta(GameObject carta)
    {
        if (cartasEnZona.Count >= maxCards)
        {
            Debug.Log("Zona combo llena.");
            return false;
        }

        if (cartasEnZona.Contains(carta))
            return false;

        cartasEnZona.Add(carta);

        // Establecer el nuevo padre
        carta.transform.SetParent(transform, false);

        // Redimensionar carta
        RectTransform cartaRect = carta.GetComponent<RectTransform>();
        cartaRect.sizeDelta = new Vector2(cardWidth, cardHeight);

        // Resetear anclas y pivote
        cartaRect.anchorMin = new Vector2(0.5f, 0.5f);
        cartaRect.anchorMax = new Vector2(0.5f, 0.5f);
        cartaRect.pivot = new Vector2(0.5f, 0.5f);

        // Resetear rotación y escala (por si acaso)
        cartaRect.localRotation = Quaternion.identity;
        cartaRect.localScale = Vector3.one;

        AcomodarCartas();
        return true;
    }

    public void SacarCarta(GameObject carta)
    {
        if (cartasEnZona.Contains(carta))
        {
            cartasEnZona.Remove(carta);
            AcomodarCartas();
        }
    }

    private void AcomodarCartas()
    {
        float totalWidth = cartasEnZona.Count * cardWidth + (cartasEnZona.Count - 1) * spacing;
        float startX = -totalWidth / 2f + cardWidth / 2f;

        for (int i = 0; i < cartasEnZona.Count; i++)
        {
            RectTransform cartaRect = cartasEnZona[i].GetComponent<RectTransform>();
            float xPos = startX + i * (cardWidth + spacing);

            cartaRect.anchoredPosition = new Vector2(xPos, 0);
        }
    }

    public bool EstaLlena()
    {
        return cartasEnZona.Count >= maxCards;
    }

    public List<GameObject> GetCartas()
    {
        return cartasEnZona;
    }

    public void LimpiarZona()
    {
        cartasEnZona.Clear();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
