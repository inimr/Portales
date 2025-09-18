using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarLentes : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.RotarSkydome += ActualizarRotacion;
    }
    void OnDisable()
    {
        EventManager.RotarSkydome -= ActualizarRotacion;
    }

    void ActualizarRotacion()
    {
        //transform.localRotation = Quaternion.Euler(EventManager.rotacionDia,transform.localRotation.y,10);
        //transform.localEulerAngles = new Vector3(EventManager.rotacionDia, 0, 10);
        transform.localRotation = Quaternion.Euler(EventManager.rotacionDia, 0, 0);
    }
}
