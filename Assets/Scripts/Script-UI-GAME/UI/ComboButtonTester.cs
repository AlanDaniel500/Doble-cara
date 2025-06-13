using UnityEngine;

public class ComboButtonTester : MonoBehaviour
{
    [SerializeField] private EnemyTurnCounter enemyTurnCounter;
    [SerializeField] private CardSpawner cardSpawner;

    public void OnComboPressed()
    {
        if (cardSpawner != null)
        {
            if (!cardSpawner.HayCartasSeleccionadas())
            {
                Debug.Log("Tenes que seleccionar mínimo una carta");
                return;
            }

            cardSpawner.EliminarCartasSeleccionadas();
            CardSelector.ReiniciarContador();
        }

        if (enemyTurnCounter != null)
        {
            enemyTurnCounter.OnPlayerTurnEnd();
        }
    }
}
