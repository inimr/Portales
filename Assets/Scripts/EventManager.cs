using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{   
    public delegate void CambiarTiempo();
    public static event CambiarTiempo OnCambiarTiempo;
    public static event CambiarTiempo RotarSkydome;
    public static event CambiarTiempo OnActualizarDiales;
    public static event CambiarTiempo OnActualizarEsferas;
    public static event CambiarTiempo OnRotando;
    public static bool esDia = true;
    public static bool diaAnterior = true;
    public static float rotacionDia = 40;
    public static float dotRotacion;    // De 1 a -1

    private void Start() {
        transform.rotation = Quaternion.Euler(rotacionDia, 0, 0);
        dotRotacion = Vector3.Dot(transform.forward, Vector3.forward);
        ActualizarDia();
        ActualizarRotacion();
        ActualizarEsferas();
        ActualizarDiales();
    }

    public static void ActualizarDia(){
        
        if(OnCambiarTiempo != null)
        {

            /*if(eraDia != esDia) return;
            eraDia = esDia;*/
            if(dotRotacion >= 0){
                esDia = true;
            } 
            else {
                esDia = false;
            }
            OnCambiarTiempo();
        }
    }    

    public static void ActualizarRotacion(){
        if(RotarSkydome != null)
        {
            print("Es dia" + esDia);
            RotarSkydome();
        }
    }    

    public static void ActualizarDiales(){
        
        OnActualizarDiales();
    }
    
    public static void ActualizarEsferas(){
        OnActualizarEsferas();
    }
    public static void Rotando(){
        OnRotando();
    }
    
}
