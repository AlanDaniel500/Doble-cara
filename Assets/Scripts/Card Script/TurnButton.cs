using UnityEngine;
using System.Collections.Generic;

public class TurnButton : MonoBehaviour
{
    public void OnEndTurnButton()
    {
        TurnManager.Instance.EndTurn();
    }
}
