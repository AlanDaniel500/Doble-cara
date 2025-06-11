using UnityEngine;

public class ComboButtonTester : MonoBehaviour
{
    [SerializeField] private EnemyTurnCounter enemyTurnCounter;

    public void OnComboPressed()
    {
        if (enemyTurnCounter != null)
        {
            enemyTurnCounter.OnPlayerTurnEnd();
        }
    }
}
