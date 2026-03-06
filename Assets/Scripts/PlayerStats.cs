using System;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Base Stats")]
    public int maxHealth = 100;

    [SerializeField] private int currentHealth;
    [SerializeField] private int gold;

    [Header("HUD References")]
    [SerializeField] private TextMeshProUGUI healthLabel;
    [SerializeField] private TextMeshProUGUI goldLabel;

    public int CurrentHealth => currentHealth;
    public int Gold => gold;

    public event Action OnStatsChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        currentHealth = maxHealth;
        gold = 0;
    }

    public void SetHUDReferences(TextMeshProUGUI healthLabel_, TextMeshProUGUI goldLabel_)
    {
        healthLabel = healthLabel_;
        goldLabel = goldLabel_;
        RefreshHUD();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnStatsChanged?.Invoke();
        RefreshHUD();
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        OnStatsChanged?.Invoke();
        RefreshHUD();
    }

    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        OnStatsChanged?.Invoke();
        RefreshHUD();
    }

    private void RefreshHUD()
    {
        if (healthLabel != null)
            healthLabel.text = $"Health: {currentHealth}/{maxHealth}";
        if (goldLabel != null)
            goldLabel.text = $"Gold: {gold}";
    }

    public void WriteToSaveData(SaveData data)
    {
        data.playerHealth = currentHealth;
        data.playerGold = gold;
    }

    public void ReadFromSaveData(SaveData data)
    {
        currentHealth = data.playerHealth;
        gold = data.playerGold;
        RefreshHUD();
    }
}
