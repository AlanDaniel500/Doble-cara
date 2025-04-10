using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DealCard : MonoBehaviour
{
    public GameObject dealCard;

    public void DealMyNewCard()
    {
        dealCard.SetActive(true);
    }
}
