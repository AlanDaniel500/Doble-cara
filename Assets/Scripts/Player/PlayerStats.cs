using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Salud del Jugador")]
    public int maxHealth = 1000;
    public int currentHealth;

    [Header("Referencias")]
    public HealtBar healthBar;     // Arrastrá la barra de vida desde el Inspector
    public GameObject loseScreen;  // UI de derrota, se activa al morir

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Debug.Log($"[PlayerStats] Iniciado con vida: {currentHealth}/{maxHealth}");
    }

    private void Update()
    {
        CheckIfDead();   

    }


    // Recibe daño y actualiza la vida del jugador.
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        Debug.Log($"[PlayerStats] Daño recibido: {damage}. Vida actual: {currentHealth}/{maxHealth}");
    }

    // Cura al jugador por una cantidad específica.
    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
    }


    //Verifica si el jugador murió.
    private void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("[PlayerStats] El jugador ha sido derrotado.");

            if (loseScreen != null)
                loseScreen.SetActive(true);
        }
    }
}

