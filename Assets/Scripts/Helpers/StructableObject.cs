using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StructableObject
{
    
    public abstract class StructableObject<T> : ScriptableObject
    {
        [SerializeField]
        private T m_data;
        public T Data => m_data;

        public void SetData(T data)
        {
#if UNITY_EDITOR
            UnityEditor.Undo.RecordObject(this, "Set Data: " + typeof(T).ToString());
#endif

            m_data = data;

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}