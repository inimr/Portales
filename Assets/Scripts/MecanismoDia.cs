using System.Collections;
using StarterAssets;
using UnityEngine;

public class MecanismoDia : MonoBehaviour
{
    private  bool cambiando = false;
    public  float rotSpeed = 20;
    public  float tiempoRot = 2;
    public Transform rotador;
    public Transform rueda;
    public Transform ruedaLoD;
    private Coroutine coroutine;
    private GameObject player;
    private FirstPersonController playerController;
    public MecanismoDia[] mecanismosDia;

    void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<FirstPersonController>();
        EventManager.OnActualizarDiales += GirarDiales;
        cambiando = false;
    }
    void OnDisable()
    {
        EventManager.OnActualizarDiales -= GirarDiales;
    }
    /*
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player"){
            player = other.gameObject;
            playerController = player.GetComponent<FirstPersonController>();
        }
    }
    
    /*
   private void OnTriggerStay(Collider other) {
        if(other.gameObject == player){

            ControlCambiarTiempo();
        }
    }*/
    public void ControlCambiarTiempo(){
        EventManager.diaAnterior = EventManager.esDia;
        cambiando = true;
        playerController.enabled = false;
        if(coroutine == null){
            coroutine = StartCoroutine(Rotar());
        }
        
        
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Q) && cambiando){
            cambiando = false;
            EventManager.ActualizarEsferas(); 
        } 
    }

    private IEnumerator Rotar(){
        EventManager.Rotando();
        while(cambiando || Mathf.Abs(rueda.eulerAngles.y - rotador.eulerAngles.y) > 0.5f){
            //print(rueda.rotation.y != rotador.rotation.y);
            if(cambiando){

                rotador.Rotate(Vector3.up * Input.GetAxisRaw("Horizontal") * rotSpeed, Space.Self);
                EventManager.Rotando();

                //rotador.transform.eulerAngles = new Vector3 (0, Input.GetAxisRaw("Horizontal") * rotSpeed * Mathf.Deg2Rad + rotador.transform.eulerAngles.y, 0);
                //rueda.transform.eulerAngles = Quaternion.RotateTowards(rueda.transform.rotation, rotador.transform.rotation, tiempoRot * Time.deltaTime); 
            }  
            else{
                playerController.enabled = true;
            
            }                
            // slerp nunca termina, entonces no sale del while y termina la corrutina
            rueda.rotation = Quaternion.Slerp(rueda.rotation, rotador.rotation, tiempoRot * Time.deltaTime);
            EventManager.rotacionDia = rueda.localEulerAngles.y;
            EventManager.dotRotacion = Vector3.Dot(rueda.forward, transform.forward);
            print("Dot " + EventManager.dotRotacion);
            EventManager.ActualizarRotacion(); 
            EventManager.ActualizarDia(); 
            yield return null;
        }

        coroutine = null;
        
        EventManager.ActualizarRotacion(); 
        EventManager.ActualizarDiales();
        EventManager.ActualizarDia(); 
        EventManager.ActualizarEsferas(); 
        
        yield return null;  
    }
    /*
    private void OnTriggerExit(Collider other) {
        StopCoroutine(Rotar());
    }*/
    void GirarDiales(){
        rueda.localRotation = Quaternion.Euler(0,EventManager.rotacionDia,0);
        rotador.localRotation = Quaternion.Euler(0,EventManager.rotacionDia,0);
        ruedaLoD.localRotation = Quaternion.Euler(0,EventManager.rotacionDia,0);
    }
    
    /*
    void ActualizarSalirDial(){
        for(int i = mecanismosDia.Length; i >= 0; i--){
            mecanismosDia[i].ActualizarAmbos();
        }
    }
    */
}
