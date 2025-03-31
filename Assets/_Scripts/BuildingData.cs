using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : MonoBehaviour
{
    private StructureSO database;
    [SerializeField] private float currentBuildingHealth;
    [SerializeField] private float maxBuildingHealth = 100f;

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
        Debug.Log("building strcuk");
        currentBuildingHealth -= damage;
    }
}
