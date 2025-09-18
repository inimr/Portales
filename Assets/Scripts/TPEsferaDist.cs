using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPEsferaDist : MonoBehaviour
{
    [SerializeField] private Transform puzle;
    private Vector3 posicionTP;
    public float distanciaMaxima = 20;
    private PickableItem pickable;
    private Material matBola;
    private bool enTransicion;
    private Coroutine coroutine;

    void Start(){
        pickable = GetComponent<PickableItem>();
        if(puzle == null){
            posicionTP = transform.position;
            puzle = transform;
        }
        else{
            posicionTP = puzle.position;
        }
        matBola = transform.GetChild(0).GetComponent<MeshRenderer>().material;
        enTransicion = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(enTransicion) return;
        if(Vector3.Distance(puzle.position, transform.position) > distanciaMaxima){
            
            enTransicion = true;
            coroutine = StartCoroutine(LerpTransparencia());
            
        }
    }

    private IEnumerator LerpTransparencia(){
        while(enTransicion){
            print("ola");
            
            matBola.SetFloat("_Transparencia", Mathf.Lerp(matBola.GetFloat("_Transparencia"), 0, Time.deltaTime * 2));

            if(matBola.GetFloat("_Transparencia") <= 0.1){
                pickable.Release(true);
                transform.position = posicionTP;    
                enTransicion = false;
                yield return new WaitForSeconds(1);
                matBola.SetFloat("_Transparencia", 2);
                StopCoroutine(coroutine);
                yield return null;
            }
            yield return null;
        }
    }
}
