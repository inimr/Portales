using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Events;
using UnityEngine;

public class ControlSol : PiezasPuzzleLuces
{
    
    [SerializeField] private List<DecalProjector> decalsAEncender;
    [SerializeField] private UnityEvent<bool> myTrigger;
    [SerializeField] private Material matSol;
    
    void OnEnable()
    {
        EventManager.OnActualizarEsferas += CambiarNocheADia;
        
    }

    private void OnDisable()
    {
        EventManager.OnActualizarEsferas -= CambiarNocheADia;
    }

    // Update is called once per frame
    void CambiarNocheADia()
    {
        if(EventManager.esDia){
            esPositivo = true;
            matSol.SetFloat("_esDia", 1);
        }
        else{
            esPositivo = false;
            matSol.SetFloat("_esDia", 0);
        }
        ActivarDesactivar(esPositivo);
    }
    public void ActivarDesactivar(bool activo){

        myTrigger.Invoke(activo);
        if (decalsAEncender.Count > 0)
        {
            for (int i = 0; i < decalsAEncender.Count; i++)
            {
                decalsAEncender[i].material.SetFloat("_Encendido", activo ? 1 : 0);
            }
        }
    }
}
