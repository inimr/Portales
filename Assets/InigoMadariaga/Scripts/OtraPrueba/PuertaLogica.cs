using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class PuertaLogica : PiezasPuzzleLuces
{
    [Header("Condiciones a cumplir para completarlo")]
    [SerializeField] private List<PiezasPuzzleLuces> objetosQueLlegan;
    [SerializeField] private List<bool> estadoObjQueLlegan;
    [SerializeField] private List<DecalProjector> listaDecals;
    [SerializeField] private List<DecalProjector> listaDecalsInternos;
    [SerializeField] private UnityEvent<bool> onChange;


    // HEMOS CREADO UN INT J PARA CONTROLAR EL CONSEGUIDO, SI NO FUNCIONA
    // BORRAR TODO LO J, SACAR EL CONSEGUIDO Y VOLVER A PONER EL RETURN
    public void CompararValoresListas()
    {
        int j = 0;
        
        for (int i = 0; i < objetosQueLlegan.Count; i++)
        {
            if (objetosQueLlegan[i].esPositivo != estadoObjQueLlegan[i])
            {
                listaDecalsInternos[i].material.SetFloat("_Encendido", 0);
                esPositivo = false;
            }
            else{
                listaDecalsInternos[i].material.SetFloat("_Encendido", 1);
                j++;
            }
        }
        if (j == estadoObjQueLlegan.Count)
        {
            esPositivo = true;
        }
            EncenderApagarDecals();
            onChange.Invoke(esPositivo);
    }  

    void EncenderApagarDecals()
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
