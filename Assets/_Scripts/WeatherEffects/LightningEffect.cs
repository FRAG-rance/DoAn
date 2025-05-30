using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class LightningEffect : MonoBehaviour
{
    [SerializeField] private GameObject lightingSFX;
    [SerializeField] private CameraController cameraControl;
    [HideInInspector] public UnityEvent<Vector3> OnLightningStrike;
    [HideInInspector] public UnityEvent OnLightningEventFinished;

    private void Start()
    {
        if (lightingSFX == null)
        {
            Debug.LogError("Lightning SFX prefab not set in LightningEffect!");
        }
    }

    public void OnLightningFinished()
    {
        OnLightningEventFinished?.Invoke();
    }

    public void ActivateLightningEffect()
    {
        StartCoroutine(Helpers.CallFunctionForTime(LightningStrike, (float) 5f + GameManager.sol, 1f, OnLightningFinished));
    }

    private void LightningStrike()
    {
        cameraControl.Flash(.25f, 0, .8f);
        GameObject temp = Instantiate(lightingSFX);
        Vector3 randomPosition = RandomGridPosition(Mathf.Clamp(GameManager.sol, 5, 15), Mathf.Clamp(GameManager.sol, 5, 15));
        temp.transform.position = randomPosition;
        Destroy(temp, 5);
        OnLightningStrike?.Invoke(randomPosition);
    }

    private Vector3Int RandomGridPosition(int gridX, int gridZ)
    {
        int x = Random.Range(-gridX, gridX);
        int z = Random.Range(-gridZ, gridZ);
        return new Vector3Int(x, 0, z);
    }
}
