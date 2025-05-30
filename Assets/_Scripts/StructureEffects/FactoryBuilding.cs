using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FactoryBuilding : BuildingData
{
    [SerializeField] private GameObject ExplosionVFX;
    
    [ContextMenu("Kill building")] 
    protected override void Kill()
    {
        base.Kill();
        InstantiateExplosionVFX();
        Vector2Int size = new Vector2Int(2, 2);
        List<Vector3> damagedBuilding = ObjectPlacer.Instance.GetSurroundingGameObject(currentPosition, size);
        
        foreach (var buildingLocation in damagedBuilding)
        {
            GameObject currentBuilding = ObjectPlacer.Instance.GetGameObject(buildingLocation);
            if (!currentBuilding)
            {
                continue;
            }
            BuildingData buildingData = currentBuilding.GetComponent<BuildingData>();
            buildingData.TakeDamage(80f);
            EconSystem.Instance.DeductEcon(50);
        }

    }
    private void InstantiateExplosionVFX()
    {
        GameObject temp = Instantiate(ExplosionVFX);
        temp.transform.position = transform.position;
        Destroy(temp, 2);
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    protected override void Update()
    {
        base.Update();
    }
}
