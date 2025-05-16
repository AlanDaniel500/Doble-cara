using System;
using UnityEngine;
using UnityEngine.UI;

public class DrawCardButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(DrawButton);
    }
    public void DrawButton()
    {
        var deck = GameManager.Instance.DeckManager;
        var hand = GameManager.Instance.HandManager;

        if (deck != null && hand != null)
        {
            deck.DrawCard(hand);
        }
        else
        {
            Debug.LogWarning("Deck o HandManager no asignados.");
        }
    }
}