using UnityEngine;
using System.Collections.Generic;
using CardSystem;

public class ComboButtonTester : MonoBehaviour
{
    [SerializeField] private EnemyTurnCounter enemyTurnCounter;
    [SerializeField] private CardSpawner cardSpawner;
    [SerializeField] private ManagerCombos managerCombos;
    [SerializeField] private RecibeDanoEnemigo enemigo;

    public void OnComboPressed()
    {
        if (cardSpawner == null || managerCombos == null || enemigo == null)
        {
            Debug.LogError("Faltan referencias necesarias en ComboButtonTester");
            return;
        }

        if (!cardSpawner.HayCartasSeleccionadas())
        {
            Debug.Log("Tenés que seleccionar mínimo una carta");
            return;
        }

        List<CardData> cartasSeleccionadas = cardSpawner.ObtenerCartasSeleccionadas();
        if (cartasSeleccionadas == null || cartasSeleccionadas.Count == 0)
        {
            Debug.LogWarning("No hay cartas seleccionadas");
            return;
        }

        ICombo comboActivo = managerCombos.RevisarMejorCombo(cartasSeleccionadas);
        if (comboActivo == null)
        {
            Debug.LogWarning("No se encontró ningún combo válido");
            return;
        }

        int dañoAplicado = 0;

        if (comboActivo is ComboBasico comboBasico)
        {
            CardData cartaMayor = comboBasico.ObtenerCartaDeMayorValor(cartasSeleccionadas);
            dañoAplicado = cartaMayor != null ? cartaMayor.damage : 0;
        }
        else
        {
            // En combos especiales, podés definir otra lógica de daño
            dañoAplicado = 15; // valor temporal
        }

        Debug.Log($"Combo detectado: {comboActivo.Nombre}, daño: {dañoAplicado}");

        enemigo.AplicarDanoDesdeCombo(dañoAplicado);

        cardSpawner.EliminarCartasSeleccionadas();
        CardSelector.ReiniciarContador();
        enemyTurnCounter.OnPlayerTurnEnd();
    }
}

