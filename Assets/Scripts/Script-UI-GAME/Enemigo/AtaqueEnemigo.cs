using UnityEngine;
using TMPro;

public class AtaqueEnemigo : MonoBehaviour
{
    [Header("Daño del enemigo")]
    [SerializeField] private int damage = 100;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI damageText;

    private PlayerHealthUI playerHealthUI;

    private void Start()
    {
        playerHealthUI = FindObjectOfType<PlayerHealthUI>();
        UpdateDamageUI();
    }

    public void EjecutarAtaque()
    {
        Debug.Log("¡El enemigo ataca!");

        if (playerHealthUI != null)
        {
            playerHealthUI.TakeDamage(damage);
        }

        UpdateDamageUI();
    }

    private void UpdateDamageUI()
    {
        if (damageText != null)
            damageText.text = damage.ToString(); // Eliminado el signo "-"
    }

    public void SetDamage(int nuevoDaño)
    {
        damage = nuevoDaño;
        UpdateDamageUI();
    }
}
