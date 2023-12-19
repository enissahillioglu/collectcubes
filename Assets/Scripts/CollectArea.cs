using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectArea : MonoBehaviour
{
    List<CubePixel> cubes = new List<CubePixel>();
    public float collectSpeed = 60;

    public bool myArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cubes.Count != 0)
        {
            for (int i = 0; i < cubes.Count; i++)
            {

                Vector3 dir = transform.position - cubes[i].transform.position;



                dir = dir.normalized;
                cubes[i].transform.position += dir * collectSpeed * Time.deltaTime;

                dir = transform.position - cubes[i].transform.position;

                if (dir.sqrMagnitude <= 1f)
                {
                    
                    if (myArea)
                    {
                        
                     }
                    else
                    {
                        
                    }
                    cubes[i].Collected(myArea,transform.position);
                    cubes.Remove(cubes[i]);
                    i--;

                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
       

        if (!other.gameObject.layer.Equals(GameConst.layerDisabledCubes) && other.GetComponent<CubePixel>() )
        {
            cubes.Add(other.GetComponent<CubePixel>());

            other.gameObject.layer = GameConst.layerDisabledCubes;
        }
    }
}
