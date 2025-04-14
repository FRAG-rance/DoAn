using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : MonoBehaviour
{
    private StructureSO database;
    [SerializeField] private HealthTracker _healthTracker;
    [SerializeField] private float currentBuildingHealth;
    [SerializeField] private float maxBuildingHealth = 100f;

    private float lastDamageTime = 0f;
    private float damageCooldown = 0.5f;

    private void Start()
    {
        currentBuildingHealth = maxBuildingHealth; //hard coded
    }

    private float GetBuildingMaxHealth()
    {
        return maxBuildingHealth;
    }
    private void Update()
    {
        if(currentBuildingHealth < 0) Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (Time.time - lastDamageTime < damageCooldown)
        {
            return;
        }

        currentBuildingHealth -= damage;
        lastDamageTime = Time.time;
        _healthTracker.UpdateSliderValue(currentBuildingHealth, maxBuildingHealth);
    }
}
