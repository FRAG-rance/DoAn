using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class StormEffect : MonoBehaviour
{
    private Vector3 currentRotation = Vector3.right;
    [SerializeField] private ObjectPlacer _objectplacer;
    public void StormFineShyt()
    {
        //vector3.right =   (1,0,0)
        //vector3.left  =   (-1,0,0)
        //vector3.foward=   (0,0,1)
        //vector3.back  =   (0,0,-1)

        if (currentRotation == Vector3.left || currentRotation == Vector3.right) { 
        
        }


        List<Vector3> positions = _objectplacer.GetAllBuildingLocation();
        List<Vector3> temp = positions;
        List<Vector3> result = new();

        int i = 0;
        temp.Sort((a, b) => Vector3.Dot(a, currentRotation).CompareTo(Vector3.Dot(b, currentRotation)));
        foreach(var position in temp)
        {
            Debug.Log(i + " " + position);
            i++;
        }
        i = 0;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
