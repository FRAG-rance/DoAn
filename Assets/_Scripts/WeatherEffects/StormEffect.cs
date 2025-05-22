using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;

public class StormEffect : MonoBehaviour
{
    private Vector3 currentRotation;
    [SerializeField] private GameObject StormSFX;
    [SerializeField] private GridData gridData;
    [SerializeField] private CameraController cameraControl;
    [HideInInspector] public UnityEvent<List<Vector3>> OnStormEvent;
    [HideInInspector] public UnityEvent OnStormEventFinished;


    /*public void SetWindDirection(Vector3 direction)
    {
        currentRotation = direction.normalized;
    }*/

    public void OnStormFinished()
    {
        OnStormEventFinished?.Invoke();
    }

    public void ActivateStormEffect() 
    {
        RandommizeWindDirection();
        //Debug.Log(currentRotation);
        SetStormVisual(currentRotation);
        StartCoroutine(cameraControl.HandleStormCameraPhysics(currentRotation,5));
        List<Vector3> damagedBuilding = GetFirstFacingBuildings();
        OnStormEvent?.Invoke(damagedBuilding);
    }

    private void RandommizeWindDirection()
    {
        List<Vector3> windDirection = new List<Vector3>() { new Vector3(1, 0, 0), 
                                                            new Vector3(-1, 0, 0), 
                                                            new Vector3(0, 0, 1), 
                                                            new Vector3(0, 0, -1)   };
        int roll = Random.Range(0, 4);
        currentRotation = windDirection[roll];
    }

    private void SetStormVisual(Vector3 direction)
    {
        GameObject storm = Instantiate(StormSFX);
        if (direction == Vector3.right)
        {
            storm.transform.position = new Vector3(-5, 1, 0);
        }
        else if (direction == Vector3.left) {
            storm.transform.position = new Vector3(5, 1, 0);
            storm.transform.rotation = Quaternion.Euler(-90, 180, 0);
        } else if (direction == Vector3.forward) {
            storm.transform.position = new Vector3(0, 1, 5);
            storm.transform.rotation = Quaternion.Euler(-90, 90, 0);
        } else if (direction == Vector3.back) {
            storm.transform.position = new Vector3(0, 1, -5);
            storm.transform.rotation = Quaternion.Euler(-90, 270, 0);
        }
        StartCoroutine(Wait5SecsToDestroy(storm, OnStormFinished));
    }

    private List<Vector3> GetFirstFacingBuildings()
    {
        //vector3.right =   (1,0,0)
        //vector3.left  =   (-1,0,0)
        //vector3.foward=   (0,0,1)
        //vector3.back  =   (0,0,-1)

        List<Vector3Int> positions = PlacementSystem.furnitureData.GetAllBuildingLocation();
        List<Vector3> result = new();

        // Handle single object case
        if (positions.Count == 1)
        {
            result.Add(positions[0]);
            return result;
        }

        // Determine which axis to group by based on wind direction
        bool isHorizontalWind = Mathf.Abs(currentRotation.x) > Mathf.Abs(currentRotation.z);
        //Debug.Log(isHorizontalWind);

        // Group positions by the appropriate axis
        Dictionary<float, List<Vector3>> columns = new Dictionary<float, List<Vector3>>();
        foreach (var pos in positions)
        {
            float key = isHorizontalWind ? pos.z : pos.x;
            if (!columns.ContainsKey(key))
            {
                columns[key] = new List<Vector3>();
            }
            columns[key].Add(pos);
        }

        //debug check for columns


        // Sort the column by the appropriate axis
        foreach (var column in columns.Values)
        {
            column.Sort((a, b) =>
            {
                if (isHorizontalWind)
                {
                    // For horizontal wind (left/right), sort by X
                    return currentRotation.x > 0 ? a.x.CompareTo(b.x) : b.x.CompareTo(a.x);
                }
                else
                {
                    // For vertical wind (forward/back), sort by Z
                    return currentRotation.z < 0 ? a.z.CompareTo(b.z) : b.z.CompareTo(a.z);
                }
            });
        }

        /*Debug.Log("----columns-----");
        foreach (var (value, list) in columns)
        {
            Debug.Log(value + ":");
            foreach (var el in list)
            {
                Debug.Log(el);
            }
        }*/

        foreach (var column in columns.Values)
        {
            /*if (currentRotation == new Vector3(1, 0, 0) || currentRotation == new Vector3(0, 0, 1))
            {
                result.Add(column[0]);
            }
            if (currentRotation == new Vector3(-1, 0, 0) || currentRotation == new Vector3(0, 0, -1))
            {
                result.Add(column[column.Count-1]);
            }*/
            result.Add(column[0]);
        }
        

        // Find the first object that's facing against the wind
        /*foreach (var pos in column)
        {
            float dotProduct = Vector3.Dot(pos, currentRotation);
            if (dotProduct < 0) // Object is facing against the wind
            {
                result.Add(pos);
                break; // Move to next column
            }


        }*/


        

        //debug check for output
        /*Debug.Log("answer______________");
        foreach (var pos in result)
        {
            Debug.Log(pos);
        }*/
        return result;
    }

    IEnumerator Wait5SecsToDestroy(GameObject storm, System.Action callback)
    {
        yield return new WaitForSeconds(5);
        //Debug.Log("waited 5s");
        Destroy(storm);
        callback?.Invoke();
    }
}
