using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BuildingData : MonoBehaviour
{
    [SerializeField] protected Vector3Int currentPosition;

    [SerializeField] protected HealthTracker _healthTracker;

    [SerializeField] protected float currentBuildingHealth;
    [SerializeField] protected float maxBuildingHealth;
    [SerializeField] protected int cost;
    [SerializeField] protected int econ;
    [SerializeField] protected int initEcon;

    protected float lastDamageTime = 0f;
    protected float damageCooldown = 0.5f;
    private Coroutine damageOverTimeCoroutine;


    private void Start()
    {
    }

    public float GetCurrentHealth()
    {
        return currentBuildingHealth;
    }

    public float GetBuildingMaxHealth()
    {
        return maxBuildingHealth;
    }

    public int GetBuildingEcon()
    {
        return econ;
    }

    public int GetBaseEcon()
    {
        return initEcon; 
    }
    public void SetBuildingEcon(int econ)
    {
        this.econ = econ;
    }

    protected virtual void Update()
    {
        if (currentBuildingHealth <= 0)
        {
            Kill();
        }
    }

    protected virtual void Kill()
    {        
        ObjectPlacer.Instance.RemoveObjectAt(currentPosition);
        PlacementSystem.furnitureData.RemoveObjectAt(currentPosition);
        EconSystem.Instance.DeductEcon(10);
        EconSystem.Instance.UpdateEconVisual();
        Destroy(gameObject);
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
    public void TakeDamageOverTime(float damage, float time)
    {
        if (damageOverTimeCoroutine != null)
        {
            StopCoroutine(damageOverTimeCoroutine);
        }
        
        damageOverTimeCoroutine = StartCoroutine(DamageOverTimeCoroutine(damage, time));
    }

    private IEnumerator DamageOverTimeCoroutine(float totalDamage, float duration)
    {
        float elapsedTime = 0f;
        float damagePerSecond = totalDamage / duration;
        
        while (elapsedTime < duration)
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                TakeDamage(damagePerSecond * Time.deltaTime);
            }
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        damageOverTimeCoroutine = null;
    }

    public void Upgrade(float amount)
    {
        if (Time.time - lastDamageTime < damageCooldown)
        {
            return;
        }

        currentBuildingHealth += amount;
        maxBuildingHealth += amount;

        lastDamageTime = Time.time;
        _healthTracker.UpdateSliderValue(currentBuildingHealth, maxBuildingHealth);
        
    }

    public void Repair()
    {
        if (Time.time - lastDamageTime < damageCooldown)
        {
            return;
        }

        currentBuildingHealth = maxBuildingHealth;
        lastDamageTime = Time.time;
        _healthTracker.UpdateSliderValue(currentBuildingHealth, maxBuildingHealth);
    }

    public void Initialize(StructureData currentData)
    {
        currentPosition = new Vector3Int(   Mathf.RoundToInt(transform.position.x),
                                            Mathf.RoundToInt(transform.position.y),
                                            Mathf.RoundToInt(transform.position.z));

        maxBuildingHealth = currentData.Health;
        currentBuildingHealth = maxBuildingHealth;
        cost = currentData.Cost;
        econ = currentData.Econ;
        initEcon = econ;
    }
}
