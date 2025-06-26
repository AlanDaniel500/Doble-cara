using UnityEngine;
using System.Collections.Generic;
using TMPro;
using CardSystem;

public class ComboPreviewUpdater : MonoBehaviour
{
    [SerializeField] private ComboUIManager comboUIManager;
    [SerializeField] private ManagerCombos managerCombos;
    [SerializeField] private CardSpawner cardSpawner;

    private List<CardData> cartasPrevias = new List<CardData>();

    private void Update()
    {
        List<CardData> cartasActuales = cardSpawner.ObtenerCartasSeleccionadas();

        // Si no cambió la selección, no actualizamos nada
        if (cartasActuales.Count == cartasPrevias.Count)
        {
            bool iguales = true;
            for (int i = 0; i < cartasActuales.Count; i++)
            {
                if (cartasActuales[i] != cartasPrevias[i])
                {
                    iguales = false;
                    break;
                }
            }

            if (iguales) return;
        }

        cartasPrevias = new List<CardData>(cartasActuales); // Guardamos nueva selección

        if (cartasActuales.Count == 0)
        {
            comboUIManager.Limpiar(); // No hay nada seleccionado
            return;
        }

        ICombo mejorCombo = managerCombos.RevisarMejorCombo(cartasActuales);

        if (mejorCombo != null)
        {
            int daño = mejorCombo.CalcularDaño(cartasActuales);
            comboUIManager.MostrarCombo(mejorCombo.Nombre, daño);
        }
        else
        {
            comboUIManager.Limpiar(); // No hay combo válido
        }
    }
}
