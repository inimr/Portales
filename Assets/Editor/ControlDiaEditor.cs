using UnityEngine;
using UnityEditor;

public class ControlDiaNoche : MonoBehaviour
{
    public bool show = false;
    public int num1;
    public float num2;
    public bool anotherToggle = false;
    public GameObject go;
}/*
public class ControlDiaEditor : Editor
{
    void UnityEditor.OnInspectorGUI()
    {
        ControlDiaNoche controlDiaNoche = target as ControlDiaNoche;
        controlDiaNoche.show = EditorGUILayout.Toggle("Hello World"); //Returns true when user clicks
    }
}*/
