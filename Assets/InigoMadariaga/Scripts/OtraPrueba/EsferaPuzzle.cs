using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EsferaPuzzle : PiezasPuzzleLuces
{
    public enum EstadoEsfera
    {
        Dia,
        Noche,
        Neutra
    }

    [SerializeField] private BaseEsfera baseTriggereada;
    [SerializeField] private BaseEsfera baseInicial;
    [SerializeField] EstadoEsfera estadoEsfera;
    [SerializeField] private Material matActivado, matActivadoLoD, matDesactivado;
    [SerializeField] private MeshRenderer rendererHijoUno, rendererHijoDos;
    public bool isKinematicActivado = false;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip Colision_Esferas;
    private Rigidbody rb;

    private void Start()
    {
        if(baseInicial != null)
        {
            baseTriggereada = baseInicial;
            PelotaInteractuar(esPositivo);
        }
        
        rb = GetComponent<Rigidbody>();
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

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(Colision_Esferas);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {            
            baseTriggereada = other.gameObject.GetComponent<BaseEsfera>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        baseTriggereada = null;
    }

    public void Release()
    {
        if(baseTriggereada)
        {
            baseTriggereada.OnTriggerEnter(GetComponent<Collider>());
        }
        else{
            rb.constraints = RigidbodyConstraints.None;
        }
      
    }
    private void CambiarNocheYDia()
    {
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
            }
            else
            {
                // de DIA a NOCHE

                rendererHijoUno.material = matDesactivado;
                rendererHijoDos.material = matDesactivado;
                
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
                
                esPositivo = false;
            }
            else
            {
                // de  DIA a NOCHE
                rendererHijoUno.material = matActivado;
                rendererHijoDos.material = matActivadoLoD;

                esPositivo = true;
            }
            
        }       
        PelotaInteractuar(esPositivo);


    }

    private void PelotaInteractuar(bool activo){
        baseTriggereada?.ActivarDesactivar(activo);
    }

    private void RotandoDial(){
        if(estadoEsfera != EstadoEsfera.Neutra){
            rendererHijoUno.material = matDesactivado;
            rendererHijoDos.material = matDesactivado;

        }
    }

    public void OnInteractuar(){
        rb.isKinematic = false;
        rb.useGravity = true;

    }
    public void OnCoger()
    {
        print("pick");
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        StartCoroutine(LerpRotacion());

        if(baseTriggereada != null && baseTriggereada.esfera == this) 
        {
            baseTriggereada.esOcupada = false;
            PelotaInteractuar(false);
            if(baseTriggereada.esfera == this) baseTriggereada.esfera = null;
            
        }
        
    }

    IEnumerator LerpRotacion(){
        while(math.abs(transform.rotation.y + transform.rotation.x + transform.rotation.z )> 0.01){
            print("Corrutina");
            transform.rotation = Quaternion.Lerp(transform.rotation, quaternion.identity, Time.deltaTime * 5);
            yield return null;
        }
    }
}
