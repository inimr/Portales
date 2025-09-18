using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ControlDestruccion : MonoBehaviour
{
    float frames;
    bool arrancar = false;
    [SerializeField] Material material;
    [SerializeField] GameObject[] puentesRotos;
    [SerializeField] GameObject[] puentesArreglados;

    // Update is called once per frame

    private void Start() {
        frames = 5f;    
        material.SetFloat("_FramesCodigo", frames);
    }
    void Update()
    {
        if(arrancar){
            frames += Time.deltaTime;
            material.SetFloat("_FramesCodigo", frames);
            if(frames >= 11.2){
                for(int i = 0; i <= 2; i++){
                    puentesArreglados[i].SetActive(true);
                    puentesRotos[i].SetActive(false);
                }

                arrancar = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void Arreglar()
    {
        arrancar =true;

    }
}
