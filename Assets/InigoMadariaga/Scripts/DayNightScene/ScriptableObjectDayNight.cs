using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "new Object", menuName = "DayNight Scriptable")]
public class ScriptableObjectDayNight : ScriptableObject
{
    public GameObject dayObject;
    public GameObject nightObject;
    public string nombre;
    public bool isItDay = true;




}
