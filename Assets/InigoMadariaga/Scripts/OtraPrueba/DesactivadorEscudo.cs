using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivadorEscudo : PiezasPuzzleLuces
{
    
    [SerializeField]private GameObject objADesactivar, rayoDesactivador;
    
    public void ActivarDesactivar(bool activado){
        objADesactivar.SetActive(activado);
        rayoDesactivador.SetActive(activado);
    }/*
    public void CambiarEscudo()
    {
        esPositivo = !esPositivo;
        objADesactivar.SetActive(esPositivo);
        rayoDesactivador.SetActive(esPositivo);
        
    }

    public void Activado() //ESTOS DOS IGUAL DAN ERROR PORQUE LAS LINEAS ESTAN MAL PUESTAS, PERO NO DEBERIAN
    {
        esPositivo = true;
        objADesactivar.SetActive(esPositivo);
        rayoDesactivador.SetActive(esPositivo);
        
        
    }

    public void Desactivado()
    {
        esPositivo = false;
        objADesactivar.SetActive(esPositivo);
        rayoDesactivador.SetActive(esPositivo);
        
        
    }*/

}
