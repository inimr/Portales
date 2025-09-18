using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ControlObjetoTemporal : MonoBehaviour
{    
    enum TipoObjeto{
        luz,
        mesh,
        particula
    }

    [SerializeField]private TipoObjeto tipo;
    public float intensidadEncendido;
    private float intensidadGoal;
    private Light luzComp;
    private LOD[] lods;
    private LODGroup lodGroup;
    private GameObject[] hijos;
    private float distanciaCull;


    void OnEnable()
    {
        EventManager.OnCambiarTiempo += Actualizar;
    }

    private void OnDisable()
    {
        EventManager.OnCambiarTiempo -= Actualizar;
    }
    private void Awake() {
        switch(tipo){
            case TipoObjeto.luz:
                luzComp = GetComponent<Light>();
                if(intensidadEncendido == 0) intensidadEncendido = luzComp.intensity;
            break;
            case TipoObjeto.mesh:
                lodGroup = transform.GetComponent<LODGroup>();
                lods = lodGroup.GetLODs();
            break;
            case TipoObjeto.particula:
                for (int i = 0; i< transform.childCount; i++)
                {
                    hijos[i] = transform.GetChild(i).gameObject;
                }
            break;
        }
        Actualizar();
    }


    void Actualizar(){
        switch(tipo){
            case TipoObjeto.luz:
                if(EventManager.esDia){
                    intensidadGoal = 0;
                }
                else{
                    intensidadGoal = intensidadEncendido;
                }
                StartCoroutine(LerpLuz());
            break;

            // Si es de dia se cambia al lod 0, si no al lod 1

            case TipoObjeto.mesh:

                distanciaCull = lods[1].screenRelativeTransitionHeight;

                if(EventManager.esDia){
                    lods[0].screenRelativeTransitionHeight = distanciaCull+0.001f;
                    lodGroup.SetLODs(lods);
                }
                else{
                    lods[0].screenRelativeTransitionHeight = 1;
                    lodGroup.SetLODs(lods);
                    
                }
            break;
            case TipoObjeto.particula:
                for (int i = 0; i< transform.childCount; i++){
                    hijos[i].SetActive(!EventManager.esDia);
                }
            break;
        }
    }

    private IEnumerator LerpLuz(){

        luzComp.intensity = Mathf.Lerp(luzComp.intensity, intensidadGoal, Time.deltaTime * 2);

        yield return null;
        
    }

}
