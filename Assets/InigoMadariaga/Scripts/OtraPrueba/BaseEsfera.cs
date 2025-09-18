using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Events;
using Unity.Mathematics;

public class BaseEsfera : PiezasPuzzleLuces
{
    public EsferaPuzzle esfera;
    public bool esOcupada;
    [SerializeField] private Transform snapTransform;
    [SerializeField] private List<DecalProjector> decalsAEncender;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip encender;
    [SerializeField] AudioClip apagar;
  
    // Evento que se muestra en el Inspector para poder a�adir todas las acciones que queramos
    // que se ejecuten cuando una esfera entra en el Trigger
    [Header("Arrastrar las acciones del Trigger")]
    [SerializeField] private UnityEvent<bool> myTrigger;
    

    
    // AL REHACER EL SNAP SI LO SACAS DE DENTRO VUELVE A HACER EL ONTRIGGER ENTERO, Y SOLO QUEREMOS SNAPEARLO


    
    private void Start(){
        ActivarDesactivar(esPositivo);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Esfera" && esOcupada == false)
        {
            audioSource.PlayOneShot(encender);
            esOcupada = true;
            esfera = other.GetComponent<EsferaPuzzle>();
            // Codigo de Jokin modificado
            esfera.transform.rotation = quaternion.identity;
            Rigidbody rb = esfera.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
            PickableItem pickable = esfera.GetComponent<PickableItem>();
            pickable.CurrentObject = null;
            // pickable.IsPlaced = true;
            GameManager.instance.IsObjectPickedUp = false;
            esfera.transform.position = snapTransform.position;
            esfera.transform.parent = snapTransform;

            ActivarDesactivar(esfera.esPositivo);
        }
    }

    private void OnTriggerExit(Collider other){
        if (other.tag == "Esfera" && esfera.gameObject == other.gameObject) audioSource.PlayOneShot(apagar);

    }

    /*private void OnTriggerExit(Collider other)
    {
        // Aqui tenemos un problema que es al pasar la segunda pelota hace el exit, ademas, los dos tienen el mismo tag
        // PROBLEMA
        if (other.tag == "Esfera" && esfera.gameObject == other.gameObject)
        {
            if (esfera.esPositivo)
            {
                DesactivarCosas();
            }
            else
            {
                // Logica de cuando la esfera no esta activada, si hiciese falta
                return;
            }
        }
    }   */

    public void ActivarDesactivar(bool esferaPositiva){

        esPositivo = esferaPositiva;
        myTrigger.Invoke(esPositivo);
        if (decalsAEncender.Count > 0)
        {
            for (int i = 0; i < decalsAEncender.Count; i++)
            {
                decalsAEncender[i].material.SetFloat("_Encendido", esPositivo ? 1 : 0);
            }
        }
    }
}
