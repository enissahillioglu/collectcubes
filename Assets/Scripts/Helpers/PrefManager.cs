using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefManager 
{
    // Start is called before the first frame update
    public static int level
    {
        get
        {
            return PlayerPrefs.GetInt("m_Level",1);  
        }
        set
        {
            PlayerPrefs.SetInt("m_Level", value);
        }
    }

    public static int scene
    {
        get
        {
            return PlayerPrefs.GetInt("m_scene", 0);
        }
        set
        {
            PlayerPrefs.SetInt("m_scene", value);
        }
    }

    public static int coin
    {
        get
        {
            return PlayerPrefs.GetInt("m_coin", 0);
        }
        set
        {
            PlayerPrefs.SetInt("m_coin", value);
        }
    }
 
}
