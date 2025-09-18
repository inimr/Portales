using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class PuertaLogicaOG : PiezasPuzzleLuces
{
    [Header("Condiciones a cumplir para completarlo")]
    [SerializeField] private List<PiezasPuzzleLuces> objetosQueLlegan;
    [SerializeField] private List<bool> estadoObjQueLlegan;
    [SerializeField] private List<DecalProjector> listaDecals;
    [SerializeField] private List<DecalProjector> listaDecalsInternos;
    [SerializeField] private UnityEvent onActivate;
    [SerializeField] private UnityEvent onDeactivate;



    private void Conseguido()
    {
        Debug.Log("Enhorabuena! Puerta logica superada!");
        esPositivo = true;
        ActivarDecals();
        onActivate.Invoke();
        Debug.Log("Esta puerta logica esta activada");
    }


    // HEMOS CREADO UN INT J PARA CONTROLAR EL CONSEGUIDO, SI NO FUNCIONA
    // BORRAR TODO LO J, SACAR EL CONSEGUIDO Y VOLVER A PONER EL RETURN
    public void CompararValoresListas()
    {
        int j = 0;
        
        for (int i = 0; i < objetosQueLlegan.Count; i++)
        {
            if (objetosQueLlegan[i].esPositivo != estadoObjQueLlegan[i])
            {
                Debug.Log("Los parametros que llegan no son correctos");
                listaDecalsInternos[i].material.SetFloat("_Encendido", 0);
                esPositivo = false;
                ApagarDecals();
                onDeactivate.Invoke();
               
                //return;

            }
            else{
                listaDecalsInternos[i].material.SetFloat("_Encendido", 1);
                j++;
            }
        }
        if (j == estadoObjQueLlegan.Count)
        {
            Conseguido();
        }
       
    }  

    public void ActivarDecals()
    {
        if(listaDecals.Count > 0) 
        {
            for(int i = 0;i < listaDecals.Count; i++)
            {
                listaDecals[i].material.SetFloat("_Encendido", 1);
            }
        }
    }
    
    public void ApagarDecals()
    {
        if (esPositivo) { return; } 
        if (listaDecals.Count > 0)
        {
            for (int i = 0; i < listaDecals.Count; i++)
            {
                listaDecals[i].material.SetFloat("_Encendido", 0);
            }
        }
    }
}
