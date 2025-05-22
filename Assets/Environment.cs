using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;
    [SerializeField] GameObject parent;
    public int numberOfObjects;
    public float planeSize;

    private void Awake()
    {

        for (int i = 0; i < numberOfObjects; i++)
        {
            float x = Random.Range(-planeSize, planeSize);
            float z = Random.Range(-planeSize, planeSize);
            Vector3 tempPos = new Vector3(x, 0, z);
            GameObject tempGameObject = Instantiate(gameObjects[Random.Range(0, gameObjects.Length)], parent.transform);
            tempGameObject.transform.position = tempPos;
            tempGameObject.transform.rotation = Quaternion.Euler(0, Random.Range(0,360), 0);
        }
    }
}
