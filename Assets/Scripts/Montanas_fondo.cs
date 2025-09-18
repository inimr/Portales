using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Montanas_fondo : MonoBehaviour
{

    private GameObject montana;
    private Transform target;
    


    // Start is called before the first frame update
    void Start()
    {
        montana = this.gameObject;
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

     montana.transform.LookAt(target.position);
     montana.transform.rotation = Quaternion.LookRotation(target.forward);


    }
}
