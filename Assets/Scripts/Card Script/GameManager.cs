using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //public OptionsManager optionManager { get; private set; } Aun no esta implementado
    //public AudioManager AudioManager { get; private set; } Aun no esta implementado
    public HandManager HandManager { get; private set; }
    public DeckManager DeckManager { get; private set; }

    private int playerHealth;
    private int enemyHealth;
    private int difficulty;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            if (transform.parent == null)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning("GameManager debe estar en la raíz para usar DontDestroyOnLoad correctamente.");
            }

            InitializeManagers();
        }
    }


    private void Start()
    {
        if (DeckManager != null && HandManager != null)
        {
            DeckManager.DealStartingHand(HandManager);
        }
    }

    private void InitializeManagers()
    {
        DeckManager = GetComponent<DeckManager>();
        HandManager = FindFirstObjectByType<HandManager>();
    }

    public int PlayerHealth
    {
        get { return playerHealth; }
        set { playerHealth = value; }
    }

    public int EnemyHealth
    {
        get { return enemyHealth; }
        set { enemyHealth = value; }
    }

    public int Difficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }
}
