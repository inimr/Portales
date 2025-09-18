using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarLuces : MonoBehaviour
{
    private Light diaLuz;
    private Light nocheLuz;
    
    void Start(){
        diaLuz = gameObject.transform.GetChild(0).GetComponent<Light>();
        nocheLuz = gameObject.transform.GetChild(1).GetComponent<Light>();
    }
    void Update()
    {
        //if(Vector3.Dot(transform.forward, Vector3.up) < 0){
        if(Input.GetKeyDown(KeyCode.E)){
            transform.RotateAround (transform.position, transform.up, 180f);
        }
    }
}
