using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class PiezaNegativoOG : PiezasPuzzleLuces
{
    [SerializeField] List<DecalProjector> listaDecals;
    [Header("Arrastrar las acciones del Trigger")]
    [SerializeField] private UnityEvent myTrigger;
    [SerializeField] private UnityEvent onEnterPositive;
    [SerializeField] private UnityEvent onEnterNegative;

    // Metodo para cambiar el sentido del booleano.
    public void CambiarSigno()
    {
        esPositivo = !esPositivo;
        CambiarSignoADecals();
        myTrigger.Invoke();

    }

    public void LeLlegaPositivo()
    {
        esPositivo = false;
        CambiarSignoADecals();
        onEnterPositive.Invoke();
    }

    public void LeLlegaNegativo()
    {
        esPositivo = true;
        CambiarSignoADecals();
        onEnterNegative.Invoke();
    }

    public void CambiarSignoADecals()
    {
        if (listaDecals.Count > 0)
        {
            for (int i = 0; i < listaDecals.Count; i++)
            {
                Material matDecal = listaDecals[i].GetComponent<DecalProjector>().material;
                if (esPositivo)
                {
                    matDecal.SetFloat("_Encendido", 1);
                }
                else
                {
                    matDecal.SetFloat("_Encendido", 0);
                }
                
            }
        }

    }
}
