using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector_Playa : MonoBehaviour
{
    public bool dentro;




    // Start is called before the first frame update
    void Start()
    {
        dentro = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player"  )
        {
            dentro =true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            dentro=false;
        }
    }


}
