using StructableObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

[CreateAssetMenu]
public class LevelContainer : StructableObject<LevelContainer>
{


    [System.Serializable]
    public struct LevelData
    {
        public string uniqueID; // for analytics
        public Level level;
        public LevelType levelType;
        public Texture2D texture;
        public int scaleFactor;
         public int time;
        [Range(1,3)]
        public int aiPower;
       

    } 
    public List<LevelData> levels;
     

}