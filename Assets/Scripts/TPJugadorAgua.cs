using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPJugadorAgua : MonoBehaviour
{
    [SerializeField] Transform TpPosicion;
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            other.transform.position = TpPosicion.position;
        }
    }
}
