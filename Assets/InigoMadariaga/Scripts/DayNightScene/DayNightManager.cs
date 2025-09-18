using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    
    private bool isDay = true;
    
    [SerializeField] private ObjectManager manager;
    
    
    public void DayNightTime()
    {
        isDay = !isDay;



        foreach (ObjectLogic logica in manager.affectedObjectList)
        {
           
            if(isDay == true)
            {
                
                Debug.Log("Es de dia");
                GameObject instancia = Instantiate(logica.SO.dayObject, logica.transform.position, Quaternion.identity);
                ObjectLogic logicaInstancia = instancia.GetComponent<ObjectLogic>();
                manager.affectedObjectList.Remove(logica);
                manager.affectedObjectList.Add(logicaInstancia); //ESTO HARA QUE EL FOREACH SEA INFINITO??
                Destroy(logica);
            }
            else
            {
                Debug.Log("Es de noche");
            }
        }       
        
        /*foreach (ObjectLogic logica in manager.affectedObjectList) //NO SE SI ES MEJOR ESTO O DESTRUIR Y CREAR LA VERDAD
       {
            MeshFilter objetoFinal = logica.GetComponent<MeshFilter>();
            MeshCollider meshFinal = logica.GetComponent<MeshCollider>();

            MeshFilter objetoDia = logica.SO.dayObject.GetComponent<MeshFilter>();
            MeshCollider meshDia = logica.SO.dayObject.GetComponent<MeshCollider>();

            MeshFilter objetoNoche = logica.SO.nightObject.GetComponent<MeshFilter>();
            MeshCollider meshNoche = logica.SO.dayObject.GetComponent<MeshCollider>();

            if (isDay == true)
            {
                
                objetoFinal.sharedMesh = objetoDia.sharedMesh;
                meshFinal.sharedMesh = objetoDia.sharedMesh;
                Debug.Log("Es de dia");
            }
            else
            {
                objetoFinal.sharedMesh = objetoNoche.sharedMesh;
                meshFinal.sharedMesh = objetoNoche.sharedMesh;
                Debug.Log("Es de noche");
            }
       }    */ 
    }

   
}

