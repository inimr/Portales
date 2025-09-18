using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Events;
using UnityEditor;
using UnityEngine.Rendering.UI;

public class BaseEsferaOG : PiezasPuzzleLuces
{
    public EsferaPuzzle esfera;
    public bool esOcupada;
    [SerializeField] private Transform snapTransform;
    [SerializeField] private List<DecalProjector> decalsAEncender;
  
    // Evento que se muestra en el Inspector para poder a�adir todas las acciones que queramos
    // que se ejecuten cuando una esfera entra en el Trigger
    [Header("Arrastrar las acciones del Trigger")]
    [SerializeField] private UnityEvent myTrigger;
    

    
    // AL REHACER EL SNAP SI LO SACAS DE DENTRO VUELVE A HACER EL ONTRIGGER ENTERO, Y SOLO QUEREMOS SNAPEARLO


    



    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Esfera" && esOcupada == false)
        {
            esOcupada = true;
            esfera = other.GetComponent<EsferaPuzzle>();
            // Codigo de Jokin modificado
            Rigidbody rb = esfera.GetComponent<Rigidbody>();
            rb.rotation = Quaternion.identity;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.useGravity = false;
            PickableItem pickable = esfera.GetComponent<PickableItem>();
            pickable.CurrentObject = null;
            // pickable.IsPlaced = true;
            GameManager.instance.IsObjectPickedUp = false;
            esfera.transform.position = snapTransform.position;
            esfera.transform.parent = snapTransform;

            if (esfera.esPositivo)
            {
                ActivarCosas();
                //HEMOS MOVIDO TODO EL CODIGO DE JOKIN PARA ABAJO DE AQUI AHI, PARA SNAPEAR
                // LAS ESFERAS DESACTIVADAS
              
                
            }
        }
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
        myTrigger.Invoke();

        if (decalsAEncender.Count > 0)
            {
                for (int i = 0; i < decalsAEncender.Count; i++)
                {
                    decalsAEncender[i].material.SetFloat("_Encendido", esPositivo ? 1 : 0);
                }
            }
    }
    public void DesactivarCosas()
    {
       if(esfera ==null) return;
       

        if (esfera.esPositivo)
        {
            esPositivo = false;
            myTrigger.Invoke(); 
            
            if (decalsAEncender.Count > 0)
            {
                for (int i = 0; i < decalsAEncender.Count; i++)
                {
                    decalsAEncender[i].material.SetFloat("_Encendido", 0);
                }
            }
        }
       

    }

    public void ActivarCosas()
    {
        esPositivo = true;
        if (decalsAEncender.Count > 0)
        {
            for (int i = 0; i < decalsAEncender.Count; i++)
            {
                decalsAEncender[i].material.SetFloat("_Encendido", 1);
            }
        }
        myTrigger.Invoke();
    }

 
}
