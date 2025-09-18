using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsferaPuzzleOG : PiezasPuzzleLuces
{
    [SerializeField] private BaseEsferaOG baseTriggereada;
    [SerializeField] private BaseEsferaOG baseInicial;
    [SerializeField] EstadoEsfera estadoEsfera;
    [SerializeField] private Material matActivado, matActivadoLoD, matDesactivado;
    [SerializeField] private MeshRenderer rendererHijoUno, rendererHijoDos;
    public bool isKinematicActivado = false;

    private void Start()
    {
        if(baseInicial != null)
        {
            baseTriggereada = baseInicial;
        }
        
    }

    void OnEnable()
    {
        EventManager.OnActualizarEsferas += CambiarNocheYDia;
        EventManager.OnRotando += RotandoDial;
        
    }

    private void OnDisable()
    {
        EventManager.OnActualizarEsferas -= CambiarNocheYDia;
        EventManager.OnRotando -= RotandoDial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            
            baseTriggereada = other.gameObject.GetComponent<BaseEsferaOG>();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        baseTriggereada = null;
    }

    public void Release()
    {
        if(baseTriggereada != null)
        {
            baseTriggereada.OnTriggerEnter(GetComponent<Collider>());
        }
      
    }
    private void CambiarNocheYDia()
    {
        if(EventManager.diaAnterior == EventManager.esDia){
            if (estadoEsfera == EstadoEsfera.Dia)
            {
                //Logica de las esferas de dia

                if (EventManager.esDia)
                {
                    // Esto es lo que pasara CUANDO pasemos de NOCHE a DIA
                    rendererHijoUno.material = matActivado;
                    rendererHijoDos.material = matActivadoLoD;
                    esPositivo = true;
                }
                else
                {
                    // de DIA a NOCHE

                    rendererHijoUno.material = matDesactivado;
                    rendererHijoDos.material = matDesactivado;
                    
                }
            }   
            else if (estadoEsfera == EstadoEsfera.Noche)
            {
                // Logica de las esferas de noche

                if (EventManager.esDia)
                {
                    // Esto es lo que pasara CUANDO pasemos de NOCHE a DIA
                    rendererHijoUno.material = matDesactivado;
                    rendererHijoDos.material = matDesactivado;
                    
                }
                else
                {
                    // de  DIA a NOCHE
                    rendererHijoUno.material = matActivado;
                    rendererHijoDos.material = matActivadoLoD;
                    esPositivo = true;
                    
                }
            }  
        return;
    }
        
        //Aqui cambiaremos el valor de lo que sea del material en el cambio NocheDia
        
          
        if (estadoEsfera == EstadoEsfera.Dia)
        {
            //Logica de las esferas de dia

            if (EventManager.esDia)
            {
                // Esto es lo que pasara CUANDO pasemos de NOCHE a DIA
                rendererHijoUno.material = matActivado;
                rendererHijoDos.material = matActivadoLoD;
                esPositivo = true;
                if (baseTriggereada){
                    baseTriggereada.esfera = gameObject.GetComponent<EsferaPuzzle>();
                    baseTriggereada.ActivarCosas();
                }
            }
            else
            {
                // de DIA a NOCHE

                rendererHijoUno.material = matDesactivado;
                rendererHijoDos.material = matDesactivado;
                

                baseTriggereada?.DesactivarCosas();
                esPositivo = false;
            }
        }
        else if (estadoEsfera == EstadoEsfera.Noche)
        {
            // Logica de las esferas de noche

            if (EventManager.esDia)
            {
                // Esto es lo que pasara CUANDO pasemos de NOCHE a DIA
                rendererHijoUno.material = matDesactivado;
                rendererHijoDos.material = matDesactivado;
                
                baseTriggereada?.DesactivarCosas();
                esPositivo = false;
            }
            else
            {
                // de  DIA a NOCHE
                rendererHijoUno.material = matActivado;
                rendererHijoDos.material = matActivadoLoD;
                esPositivo = true;
                

                if (baseTriggereada){
                    baseTriggereada.esfera = gameObject.GetComponent<EsferaPuzzle>();
                    baseTriggereada.ActivarCosas();
                }
            }
        }       

    }

    private void RotandoDial(){
        if(estadoEsfera != EstadoEsfera.Neutra){
            rendererHijoUno.material = matDesactivado;
            rendererHijoDos.material = matDesactivado;

        }
    }

   public void OnInteractuar()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
     
        
    }

    public void OnCoger()
    {
        if(baseTriggereada != null) 
        {
            baseTriggereada.esOcupada = false;
            baseTriggereada.DesactivarCosas();
            baseTriggereada.esfera = null;
            
        }

        
    }

    public enum EstadoEsfera
    {
        Dia,
        Noche,
        Neutra
    }


}
