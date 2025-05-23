using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Salud del Jugador")]
    public int maxHealth = 100;
    public int currentHealth;

    public HealtBar healthBar; // Arrastrá la barra de vida del jugador
    public GameObject loseScreen; // Panel que se activa si el jugador muere

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Debug.Log("PlayerStats iniciado: maxHealth=" + maxHealth);
    }

    private void Update()
    {
        CheckIfDead();
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Player recibió daño: {damage}");
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    private void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Jugador derrotado");
            if (loseScreen != null)
                loseScreen.SetActive(true);
        }
    }
}
