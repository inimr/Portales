using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPortalSalida : MonoBehaviour
{
    [SerializeField] GameObject portal;
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            portal.SetActive(true);
        }
    }
}
