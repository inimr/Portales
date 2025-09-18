using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiezasIluminadas : MonoBehaviour
{
    public int numeroPieza;
    public Material matPieza;

    private void OnDestroy()
    {
        matPieza.SetFloat("_Iluminando", 0);
    }
}
