

using UnityEngine;

public class GameConst :MonoBehaviour
{




    public static int layerCube
    {
        get { return LayerMask.NameToLayer("Cube"); }

    }

    public static int layerDisabledCubes
    {
        get { return LayerMask.NameToLayer("disabledCubes"); }
       
    }

    public static int layerMyCubes
    {
        get { return LayerMask.NameToLayer("myCubes"); }

    }
    public static int layerAiCubes
    {
        get { return LayerMask.NameToLayer("aiCubes"); }

    }
    public static int layerPlayer
    {
        get { return LayerMask.NameToLayer("Player"); }

    }
    public static int layerAi
    {
        get { return LayerMask.NameToLayer("Ai"); }

    }

    public static int layerDefaut
    {
        get { return 0; }

    }
    // Start is called before the first frame update

}
