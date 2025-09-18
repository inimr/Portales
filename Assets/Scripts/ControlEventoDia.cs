using UnityEngine.Rendering;
using UnityEngine;
using System;
using Unity.Mathematics;
using Unity.VisualScripting;

public class ControlEventoDia : MonoBehaviour
{
    private float normalizarDot;
    private float remapValorDia;
    private float remapValorNoche;
    private float dotRot;
    [SerializeField] Color SkyColorDia;
    [SerializeField] Color EquatorColorDia;
    [SerializeField] Color GroundColorDia;
    [SerializeField] Color SkyColorNoche;
    [SerializeField] Color EquatorColorNoche;
    [SerializeField] Color GroundColorNoche;
    [SerializeField] Color fogDia;
    [SerializeField] Color fogNoche;
    [SerializeField] Color oceanoDia;
    [SerializeField] Color oceanoNoche;
    [SerializeField] Volume volumenDia;
    [SerializeField] Volume volumenNoche;
    [SerializeField] Light luzNoche;
    [SerializeField] Light luzDia;
    [SerializeField] Material oceano;
    [SerializeField] float instensidadLuzDia;
    [SerializeField] float instensidadLuzNoche;
    
    void OnEnable()
    {
        EventManager.RotarSkydome += ActualizarRotacion;
    }
    void OnDisable()
    {
        EventManager.RotarSkydome -= ActualizarRotacion;
    }

    void ActualizarRotacion()
    {
        transform.rotation = Quaternion.Euler(EventManager.rotacionDia,0,0);
        //dotRot = Vector3.Dot(transform.rotation.eulerAngles, Vector3.up);
        dotRot = EventManager.dotRotacion;
        //print ("Dot " + dotRot);
        normalizarDot = (dotRot+1)/2;
        remapValorDia = Mathf.Clamp(dotRot, 0, 1);
        remapValorNoche = math.abs(Mathf.Clamp(dotRot, -1, 0));

        LerpColor();
        CambiarLuz();
        CambiarVolumen();
        
    }

    void LerpColor(){
        RenderSettings.ambientSkyColor = Color.Lerp(SkyColorDia, SkyColorNoche, 1-normalizarDot);
        RenderSettings.ambientEquatorColor = Color.Lerp(EquatorColorDia, EquatorColorNoche, 1-normalizarDot);
        RenderSettings.ambientGroundColor = Color.Lerp(GroundColorDia, GroundColorNoche, 1-normalizarDot);
        RenderSettings.fogColor = Color.Lerp(fogDia, fogNoche, 1-normalizarDot);
        
        oceano.SetColor("_BottomColor", Color.Lerp(oceanoDia, oceanoNoche, 1-normalizarDot));
        //oceano.SetFloat("_WaterShadow", Mathf.SmoothStep(-0.2f, -9, 1-normalizarDot));
    }
    
    void CambiarLuz(){
        luzNoche.intensity = Mathf.SmoothStep(0, instensidadLuzNoche, remapValorNoche);
        luzDia.intensity = Mathf.SmoothStep(0, instensidadLuzDia, remapValorDia);
        if(dotRot >= 0){
            RenderSettings.sun = luzDia;
        }
        else{
            RenderSettings.sun = luzNoche;

        }
    }
    void CambiarVolumen(){
        volumenNoche.weight = Mathf.SmoothStep(0, 1, remapValorNoche);
        volumenDia.weight = Mathf.SmoothStep(0, 1, remapValorDia);
    }
}
