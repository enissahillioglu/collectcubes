using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelCreator))] // MyScript, editörün iliþkilendirildiði scriptin adýdýr
public class LevelEditor : Editor
{
    private Texture2D selectedTexture;
    

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelCreator script = (LevelCreator)target;

        GUILayout.Space(10);

         
        selectedTexture = EditorGUILayout.ObjectField("Select a Texture", selectedTexture, typeof(Texture2D), false) as Texture2D;

        GUILayout.Space(10);

         
       
        if (GUILayout.Button("Create"))
        {
            if (selectedTexture != null)
            {
                script.Create(selectedTexture);
            }
            else
            {
                Debug.LogError("No texture selected!");
            }
        }

        if (GUILayout.Button("Clear"))
        { 
                script.Clear();

           
        }
        if (GUILayout.Button("Save Level"))
        { 
                script.Save();
            
        }
    }
}