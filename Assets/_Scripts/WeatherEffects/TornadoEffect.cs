using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class TornadoEffect : MonoBehaviour
{
    [SerializeField] private GameObject flamePillarPrefab;
    [SerializeField] private ObjectPlacer _objectplacer;
    [HideInInspector] public UnityEvent<Vector3> OnTornadoEvent;
    [SerializeField] private Vector3 currentGridCell;

    // Start is called before the first frame update
    void Start()
    {
        if (flamePillarPrefab == null)
        {
            Debug.LogError("Flame Column prefab not set in TornadoEffect!");
        }
    }

    private IEnumerator IterateGridPosition(List<Vector3> pathPositions)
    {
        GameObject flamePillar = Instantiate(flamePillarPrefab);
        float duration = 1f;

        //edgecase
        OnTornadoEvent?.Invoke(pathPositions[0]);
        for (int i = 0; i < pathPositions.Count + 1; i++) { 
            //retarded but works
            if(i+1 == pathPositions.Count)
            {
                break;
            }
            Vector3 start = pathPositions[i];
            Vector3 end = pathPositions[i + 1];
            float elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float pct = elapsedTime / duration;
                flamePillar.transform.position = Vector3.Lerp(start, end, pct);
                OnTornadoEvent?.Invoke(flamePillar.transform.position);
                yield return null;
            }
        }
        Destroy(flamePillar);
    }

    public void TornadoEvent()
    {
        List<Vector3> availablePositions = _objectplacer.GetAllBuildingLocation();
        Vector3 startPosition = availablePositions[Random.Range(0, availablePositions.Count)];
        availablePositions.Remove(startPosition);
        Vector3 endPosition = availablePositions[Random.Range(0,availablePositions.Count)];
        
        // Get all grid positions between start and end
        List<Vector3> pathPositions = GetGridPositionsBetween(startPosition, endPosition);
        foreach (Vector3 pos in pathPositions)
        {
            Debug.Log(pos);
        }
        StartCoroutine(IterateGridPosition(pathPositions));
    }

    private List<Vector3> GetGridPositionsBetween(Vector3 start, Vector3 end)
    {
        Debug.Log(start + ", " + end); 
        List<Vector3> positions = new List<Vector3>();
        Vector3Int startGrid = Vector3Int.RoundToInt(start);
        Vector3Int endGrid = Vector3Int.RoundToInt(end);
        // Calculate the differences
        int dx = endGrid.x - startGrid.x;
        int dy = endGrid.y - startGrid.y;
        int dz = endGrid.z - startGrid.z;
    
        // Get the number of steps needed (using the maximum difference)
        int steps = Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy), Mathf.Abs(dz));
    
        // Calculate the increment per step
        float xStep = dx / (float)steps;
        float yStep = dy / (float)steps;
        float zStep = dz / (float)steps;
    
        // Generate all positions
        for (int i = 0; i <= steps; i++)
        {
            Vector3 pos = new Vector3(
                Mathf.RoundToInt(startGrid.x + (xStep * i)),
                Mathf.RoundToInt(startGrid.y + (yStep * i)),
                Mathf.RoundToInt(startGrid.z + (zStep * i))
            );
            Debug.Log(pos);
            if (!positions.Contains(pos))
            {
                positions.Add(pos);
            }
        }
        
        /*foreach(Vector3 pos in positions)
        {
            Debug.Log(pos);
        }*/
        return positions;
    }
}
