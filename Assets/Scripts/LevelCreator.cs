using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
 

public class LevelCreator : MonoBehaviour
{

    public CubePixel cubePrefab;
    public Level levelTemp;
    public int totalCube;
    Texture2D txt;
    [Range(1,35)]
    public int scaleFactor=30;
    public string levelInfo;
    public LevelType levelType;
    Level tempLevel;
    public LevelContainer levelContainer;
    [Range(1, 3)]
    public int aiPower=3;
    public int timeForChallenge = 30;
    // Start is called before the first frame update
    void Start()
    {
       Destroy(gameObject);
    }

    
    public void Create(Texture2D tex)
    {
        if (tempLevel)
        {
            DestroyImmediate(tempLevel.gameObject);
        }
        tempLevel=Instantiate(levelTemp, transform);
        txt = tex;
        totalCube = 0;
        GameObject parent = new GameObject("cubesParent");
        parent.transform.parent = tempLevel.transform;
        tempLevel.cubesParent = parent.transform;
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

                        totalCube++;

                        CubePixel cube = PrefabUtility.InstantiatePrefab(cubePrefab,parent.transform) as CubePixel  ;
                        cube.gameObject.SetActive(true);

                        cube.transform.position = Vector3.zero;

                        Vector3 pos = new Vector3((float)i / sizeX, 0, (float)j / sizeY); 

                        pos -= center;

                        pos *= 0.5f;

                        cube.Init(pos, pixelcolor);
                    }
                }
            }
        }
        tempLevel.cubeCount = totalCube;

       
    }

    public void Clear()
    {
        if (tempLevel)
        {
            DestroyImmediate(tempLevel.gameObject);
        }

        totalCube = 0;
    }

    public void Save()
    {
        if (tempLevel)
        {
            string prefabPath = "Assets/LEVELS";
            if (levelInfo == "")
                levelInfo = "LEVEL 000";
            string prefabName = levelInfo + ".prefab";


            if (AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath + "/" + prefabName) != null)
            {
                string finalPrefabName = prefabName + ".prefab";
                int option = EditorUtility.DisplayDialogComplex("Prefab Exists",
                    "A prefab with the same name already exists. What would you like to do?",
                    "Rename",
                    "Overwrite",
                    "Cancel");

                if (option == 0)
                {
                    prefabName += "(1)";
                    prefabName = prefabName + ".prefab";
                }
                else if (option == 1)
                {

                    AssetDatabase.DeleteAsset(prefabPath + "/" + finalPrefabName);
                }
                else
                {
                    return;
                }
            }

            Level tmp = Instantiate(tempLevel,  tempLevel.transform.position, tempLevel.transform.rotation, tempLevel.transform.parent);
            DestroyImmediate(tmp.cubesParent.gameObject);
            GameObject prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(tmp.gameObject, prefabPath + "/" + prefabName, InteractionMode.UserAction);
           
            Selection.activeObject = prefab;
            LevelContainer.LevelData levelData = new LevelContainer.LevelData();
            levelData.level = prefab.GetComponent<Level>();
            levelData.texture = txt;
            levelData.uniqueID = levelInfo;
            levelData.levelType = levelType;
            levelData.scaleFactor = scaleFactor;
            levelData.aiPower = aiPower;
            levelData.time = timeForChallenge;
            levelContainer.levels.Add(levelData);
            EditorUtility.SetDirty(levelContainer);
            DestroyImmediate(tmp .gameObject);



        }
       
    }

    void SpawnPixel(Texture2D tex, int width, int height, int pixelSizeX, int pixelSizeY, Vector3 center)
    {
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // Create(txt);
        }
    }
}
