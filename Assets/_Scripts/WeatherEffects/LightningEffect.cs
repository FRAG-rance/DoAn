using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class LightningEffect : MonoBehaviour
{
    [SerializeField] private GameObject lightingSFX;
    [HideInInspector] public UnityEvent<Vector3> OnLightningStrike;

    private void Start()
    {

    }

    void ActivateLightningEffect()
    {
    }

    public void LightningStrike()
    {
        GameObject temp = Instantiate(lightingSFX);
        Vector3 randomPosition = RandomGridPosition(5, 5);// hard coded also 
        temp.transform.position = randomPosition;
        Destroy(temp, 5);
        OnLightningStrike?.Invoke(randomPosition);
    }

    public Vector3Int RandomGridPosition(int gridX, int gridZ)
    {
        int x = Random.Range(-gridX, gridX);
        int z = Random.Range(-gridZ, gridZ);
        return new Vector3Int(x, 0, z);
    }
}
