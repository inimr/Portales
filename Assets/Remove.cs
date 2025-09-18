using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour
{
   

    
    private void Start()
    {
        LevelManager.onQuitar += DesaparecerObjeto;
    }

    private void OnDestroy()
    {
        LevelManager.onQuitar -= DesaparecerObjeto;

    }
    public void DesaparecerObjeto()
    {
        gameObject.SetActive(false);
    }
}
