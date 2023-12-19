using UnityEngine;
using UnityEditor;

public class Level : MonoBehaviour
{


    public Transform myStartPos, aiStartPos;
    public Transform myAreaPos, aiAreaPos;
    public Transform timeChallengeSpawnerPos;
    public int cubeCount;
    public Transform cubesParent;
    public bool generateFromTexture=true;

    void Update()
    { 

    }

    public void Init(LevelContainer.LevelData data)
    {
        if (data.levelType == LevelType.TIME_CHALLENGE)
            generateFromTexture = false;

        if (generateFromTexture)
        {
            CreateFromTexture(data.texture, data.scaleFactor);
        }
       
    }

    void CreateFromTexture(Texture2D tex,int scaleFactor)
    {
        cubeCount = 0;
        cubesParent = (new GameObject("cubesParent")).transform;
        cubesParent.transform.parent = transform;
       
        int standardSize = Mathf.Max(tex.width, tex.height) / scaleFactor;
        int sizeX = standardSize;
        int sizeY = standardSize;
        Vector3 center = new Vector3((tex.width / sizeX) / 2f, 0, (tex.height / sizeY) / 2f);

        for (int i = 0; i < tex.width; i++)
        {
            for (int j = 0; j < tex.height; j++)
            {
                if (i % sizeX == 0 && j % sizeY == 0)
                {
                    if (i > 0 && j > 0 && i < tex.width - 0 && j < tex.height - 0)
                    {

                        Color pixelcolor = tex.GetPixel(i, j);

                        if (pixelcolor.a < 1f) continue;

                        cubeCount++;

                        CubePixel cube = PoolManager.Instance.poolingCubePixel.GetObjectFromPool();
                        LevelManager.Instance.activeCubes.Add(cube.transform);
                        cube.transform.parent = cubesParent;
                        cube.gameObject.SetActive(true);

                        cube.transform.position = Vector3.zero;

                        Vector3 pos = new Vector3((float)i / sizeX, 0, (float)j / sizeY);

                        pos -= center;

                        pos *= 0.5f;

                        cube.Init(pos, pixelcolor,false);
                    }
                }
            }
        }
    }
}
 