using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalAnimation : MonoBehaviour
{
    private Material material;
    private float tiempo = 0;
    private float maximo;
    private float encendidoONo;

    private void OnDestroy()
    {
        material.SetFloat("_ColumnaX", 0);
        material.SetFloat("_Encendido", encendidoONo);
    }

    private void Start()
    {
        material = GetComponent<DecalProjector>().material;
        maximo = material.GetFloat("_FilaX");
        encendidoONo = material.GetFloat("_Encendido");
    }

    private void Update()
    {
        tiempo += Time.deltaTime;

        if(tiempo  < 1f) 
        {
            material.SetFloat("_ColumnaX", Mathf.Lerp(0, maximo, tiempo));
        }
        else
        {
            tiempo = 0;
            material.SetFloat("_ColumnaX", 0);
        }
       
    }
}
