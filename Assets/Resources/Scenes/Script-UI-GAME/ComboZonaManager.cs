using System.Collections.Generic;
using UnityEngine;

public class ComboZonaManager : MonoBehaviour
{
    [SerializeField] private int maxCards = 4; // Máximo cartas permitidas
    [SerializeField] private float cardSpacing = 150f; // Espacio entre cartas en la UI
    [SerializeField] private Vector2 startPosition = new Vector2(-225f, 0f); // Punto donde empieza la fila de cartas

    private List<GameObject> cartasEnCombo = new List<GameObject>();

    // Intentar agregar una carta a la zona combo
    public bool TryAddCard(GameObject carta)
    {
        if (cartasEnCombo.Count >= maxCards)
        {
            Debug.Log("Zona combo llena, no se puede agregar más cartas.");
            return false;
        }

        if (!cartasEnCombo.Contains(carta))
        {
            cartasEnCombo.Add(carta);
            // Cambiar padre para que la carta quede dentro de la zona combo (UI)
            carta.transform.SetParent(this.transform, false);

            AcomodarCartas();

            return true;
        }

        return false; // La carta ya estaba en la lista
    }

    // Sacar una carta de la zona combo
    public void RemoveCard(GameObject carta)
    {
        if (cartasEnCombo.Contains(carta))
        {
            cartasEnCombo.Remove(carta);

            // Opcional: si querés devolver la carta a otro padre (ej. mano del jugador),  
            // eso lo manejás en el código que llame a RemoveCard.

            AcomodarCartas();
        }
    }

    // Ordenar las cartas en fila dentro de la zona combo
    private void AcomodarCartas()
    {
        for (int i = 0; i < cartasEnCombo.Count; i++)
        {
            Vector2 nuevaPos = startPosition + new Vector2(cardSpacing * i, 0);
            cartasEnCombo[i].GetComponent<RectTransform>().anchoredPosition = nuevaPos;
        }
    }

    // Método para limpiar toda la zona combo si necesitás
    public void ClearCombo()
    {
        cartasEnCombo.Clear();
        // Podés agregar lógica para devolver cartas a la mano, etc.
    }

    // Método para obtener la lista actual de cartas (por si necesitás)
    public List<GameObject> GetCartasEnCombo()
    {
        return cartasEnCombo;
    }
}
