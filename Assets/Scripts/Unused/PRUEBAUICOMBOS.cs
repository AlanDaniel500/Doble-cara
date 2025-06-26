using UnityEngine;

public class PRUEBAUICOMBOS : MonoBehaviour
{
    public ComboUIManager comboUIManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            comboUIManager.MostrarCombo("Cerbero", 15);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            comboUIManager.Limpiar();
        }
    }
}
