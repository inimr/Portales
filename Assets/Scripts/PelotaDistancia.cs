using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaDistancia : MonoBehaviour
{
    public Transform Objetivo;
    public float Distancia = 100.0f;

    private Vector3 POS_inicial;
    private Vector3 POS_actual;

    public PickableItem YOLO;

    // Start is called before the first frame update
    void Start()
    {
        POS_inicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        print("DISTANCIA= "+ Vector3.Distance(transform.position, Objetivo.position));
        print("POS_INICIAL= " + POS_inicial);

        POS_actual = transform.position;
        print("POS ACTUAL= " + POS_actual);

        if (Vector3.Distance(transform.position, Objetivo.position) > Distancia)
        {
            // print("DEMASIADO");
            YOLO.Release(false);
           Retornar();
            print("RETORNAR");

        }

       // else { transform.position = transform.position; }





    }

    private void Retornar() {transform.position = Objetivo.position; }






}
