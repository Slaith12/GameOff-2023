using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardResizer))]
public class CardSizeEditor : Editor
{
    private void OnEnable()
    {
        CardResizer resizer = (CardResizer)target;
        if(resizer.currentSize == 0)
        {
            resizer.Awake();
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CardResizer resizer = (CardResizer)target;
        float size = EditorGUILayout.FloatField("Size", resizer.currentSize);
        if (size != resizer.currentSize && size > 0)
            resizer.currentSize = size;
    }
}
