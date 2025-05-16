using UnityEngine;
using UnityEngine.UI; 

public class EnemyAI : MonoBehaviour
{
    [Header("Salud del Enemigo")]
    public int maxHealth = 100;
    public int currentHealth;

    public HealtBar healthBar; // Referencia a la barra de salud
    public GameObject winScreen;  // Arrastrar en el Inspector tu panel de victoria

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Debug.Log("EnemyAI iniciado: maxHealth=" + maxHealth);
    }

    private void Update()
    {
        EnemyDead();
    }


    /// <summary>
    /// Aplica daño al enemigo y actualiza la barra. 
    /// Si llega a 0, muestra la pantalla de victoria.
    /// </summary>
    public void TakeDamage(int damage)
    {
        Debug.Log($"TakeDamage() recibido: {damage}");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void EnemyDead()
    {
        if (currentHealth <= 0)
        {
            winScreen.SetActive(true);
        }
    }

}

