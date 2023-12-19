using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [HideInInspector]
    public ObjPooling<CubePixel> poolingCubePixel;
    public int cubeCount;
    public CubePixel cubePixelPrefab;

    [HideInInspector]
    public ObjPooling<ParticleSystem> pooligCubeParticle;
    public int cubeParticleCunt;
    public ParticleSystem cubeParticlePrefab;


    

    
    // Start is called before the first frame update
    void Awake()
    {
        poolingCubePixel = new ObjPooling<CubePixel>(cubePixelPrefab, cubeCount, transform);
        pooligCubeParticle = new ObjPooling<ParticleSystem>(cubeParticlePrefab, cubeParticleCunt, transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < 500; i++)
            {
                CubePixel aa =  poolingCubePixel.GetObjectFromPool();

                aa.gameObject.SetActive(true);
                aa.transform.position = Vector3.zero;
            }
            
        }
    }
}
