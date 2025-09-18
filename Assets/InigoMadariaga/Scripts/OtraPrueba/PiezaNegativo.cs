using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class PiezaNegativo : PiezasPuzzleLuces
{
    [SerializeField] List<DecalProjector> listaDecals;
    [Header("Arrastrar las acciones del Trigger")]
    [SerializeField] private UnityEvent<bool> myTrigger;

    // Metodo para cambiar el sentido del booleano.
    public void CambiarSigno(bool activo)
    {
        esPositivo = !activo;
        CambiarSignoADecals();
        myTrigger.Invoke(esPositivo);
    }
    public void CambiarSignoADecals()
    {
        if (listaDecals.Count > 0)
        {
            for (int i = 0; i < listaDecals.Count; i++)
            {
                listaDecals[i].material.SetFloat("_Encendido", esPositivo ? 1 : 0);                
            }
        }

    }
}
