using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using System;

[CustomEditor(typeof(Geometry2D))]
public class Geometry2DInspector : Editor
{
    private GUIStyle _pointLabelStyle = null;
    private ReorderableList _pointsReorderableList = null;

    private Tool _lastUsedTool = Tool.None;

    private void OnEnable()
    {
        _pointsReorderableList = new ReorderableList(serializedObject, serializedObject.FindProperty("points"));
        _pointsReorderableList.drawElementCallback = _OnDrawPointsListElement;

        _pointLabelStyle = new GUIStyle();
        _pointLabelStyle.normal.textColor = Color.white;
        _pointLabelStyle.fontSize = 20;

        _lastUsedTool = Tools.current;
        Tools.current = Tool.None;
    }
    private void OnDisable()
    {
        _pointsReorderableList = null;

        if (Tools.current == Tool.None)
        {
            Tools.current = _lastUsedTool;
        }
    }

    private void _OnDrawPointsListElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        EditorGUI.PropertyField(rect, _pointsReorderableList.serializedProperty.GetArrayElementAtIndex(index), GUIContent.none);
    }


    public override void OnInspectorGUI()
    {

        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "points");
        _pointsReorderableList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }

    private void OnSceneGUI()
    {
        Geometry2D path2D = target as Geometry2D;
        if (null == path2D) return;

        //Edit Points
        for (int i = path2D.points.Length; i-- > 0;)
        {
            Vector2 point = path2D.points[i];
            EditorGUI.BeginChangeCheck();
            //point = Handles.PositionHandle(point, Quaternion.identity);
            point = Handles.FreeMoveHandle(point, Quaternion.identity, 1, Vector3.one, Handles.CircleHandleCap);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(path2D, "Edit Path Point");
                path2D.points[i] = point;
            }
        }

        //Draw Points and labels
        Handles.color = Color.yellow;
        for (int i = path2D.points.Length; i-- > 0;)
        {
            Vector2 point = path2D.points[i];
            Handles.DrawSolidDisc(point, Vector3.forward, 0.1f);

            Vector2 labalPos = point;
            labalPos.y -= 0.2f;
            Handles.Label(labalPos, (i + 1).ToString(), _pointLabelStyle);
            if (i > 0)
            {
                Handles.DrawLine(path2D.points[i], path2D.points[i - 1]);
            }
            else
            {
                Handles.DrawLine(path2D.points[i], path2D.points[path2D.points.Length - 1]);
            }
        }
        if (Event.current.type == EventType.Layout)
        {
            HandleUtility.AddDefaultControl(0);
        }
    }
}
