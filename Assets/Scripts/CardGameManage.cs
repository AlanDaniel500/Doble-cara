using System.Collections.Generic;
using UnityEngine;

public class CardGameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;



    public void DrawCard()
    {
       if(deck.Count >= 1)
        {
           Card ranCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    /*ranCard.gameObject.SetActive(true);
                    ranCard.transform.position = cardSlots[i].position;*/ //Sale error en esas partes
                    availableCardSlots[i] = false;
                    deck.Remove(ranCard);
                    return;
                }
            }
        }
    }

}


public class Card
{
    public string name;
    public int value;

    public Card(string name, int value)
    {
        this.name = name;
        this.value = value;
    }
}

