using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TeleportLogic : PiezasPuzzleLuces
{
    [SerializeField] private Transform posFinal;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject tpFX;
    public bool hasTeleported;
    private TeleportLogic recibidorTeleport;
    private float tiempo = 0;
    private GameObject playerCollider;
   

    private void Awake()
    {
        recibidorTeleport = posFinal.GetComponent<TeleportLogic>();
        playerCollider = GameObject.FindWithTag("Player");
    }
/*
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == playerCollider.gameObject) print("Jugador Encontrado");
    }*/
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerCollider.gameObject)
        {
            hasTeleported = false;
            if(recibidorTeleport.esPositivo != true)
            {
                recibidorTeleport.esPositivo = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == playerCollider.gameObject && hasTeleported == false && esPositivo == true)
        {
                       
            tiempo += Time.deltaTime;            
            if(tiempo > 2)
            {
                
                canvasGroup.DOFade(1, 1).OnComplete(() =>
                {
                    AudioManager.instance.Efecto(AudioManager.instance.Telepor_clip);
                    recibidorTeleport.hasTeleported = true;
                    other.gameObject.SetActive(false);
                    other.transform.position = posFinal.position;                    
                    other.gameObject.SetActive(true);
                    canvasGroup.DOFade(0, 1);
                });
                tiempo = 0;
            }

        }
    }

    public void EncenderApagar(bool activo){
        esPositivo = activo;
        tpFX.SetActive(esPositivo);

        
    }

    public void PuzzleTerminado_1(bool activo) { LevelManager.data.puzzle1Terminado = activo; print(activo); }
    public void PuzzleTerminado_2(bool activo) { LevelManager.data.puzzle2Terminado = activo; }
    public void PuzzleTerminado_3(bool activo) { LevelManager.data.puzzle3Terminado = activo; }
    public void PuzzleTerminado_4(bool activo) { LevelManager.data.puzzle4Terminado = activo; }




    /*
        public void CambiarSignoTeleport()
        {
            esPositivo = !esPositivo;
            print("El teleport esta activado = " + esPositivo);
        }
        public void CambiarSignoAPositivo()
        {
            esPositivo = true;
        }

        public void CambiarSignoANegativo()
        {
            esPositivo = false;
        }
        // HABRA QUE HACER UN METODO PARA ACTIVAR EL TELEPORT, O AQUI O EN LA PUERTA LOGICA O EN LA BASE*/
}
