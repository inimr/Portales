using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiezasAClicarEnPrueba : MonoBehaviour
{
    public int numPiezaClicada;
    public PruebaManager controlPrueba;
    [Header("Variables del shader")]      
    [SerializeField] private MeshRenderer render;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }




    // TENEMOS QUE HACER QUE AL CLICAR NOS COJA EL NUMERO, LA AÑADA A LA LISTA DE PRUEBA MANAGER, COMPARE CON LA OTRA, SI ESTA BIEN SEGUIR Y SI ESTA MAL SACARNOS DEL JUEGO (BORRANDO LA LISTA ETC)
    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       /* if(controlPrueba.enJuego == true)
        {
            StartCoroutine(IluminarYApagarLoClicado());
            AñadirNumeroALaListaYCompara();

        }*/
    }
    public void SeleccionarPiedra()
    {
        if (controlPrueba.enJuego)
        {
          
            StartCoroutine(IluminarYApagarLoClicado());
            AñadirNumeroALaListaYCompara();

        }
    }
    private void AñadirNumeroALaListaYCompara()
    {
        controlPrueba.numPrueba.Add(numPiezaClicada);

        if(controlPrueba.listaNumEncendidos.IndexOf(numPiezaClicada) == controlPrueba.numPrueba.IndexOf(numPiezaClicada))// && controlPrueba.listaNumEncendidos.IndexOf(-1) )
        {
            // Añadir sonido de exito?

            if(controlPrueba.listaNumEncendidos.Count == controlPrueba.numPrueba.Count)
            {

                Debug.Log("Has Superado la Prueba");
                if(LevelManager.data.state == GameState.SinEmpezar)
                {
                    controlPrueba.UpdateGameState(GameState.Nivel1); // Los cristales encendidos los podemos meter en el evento del UpdateGameState supongo
                    
                }
                else if(LevelManager.data.state == GameState.Nivel1)
                {
                    controlPrueba.UpdateGameState(GameState.Nivel2);
                }
                else
                {
                    controlPrueba.UpdateGameState(GameState.Completado);
                }
                TerminarPrueba();
                
            }
            else
            {
                Debug.Log("Correcto, sigue adelante champion");
            }
        }
        else
        {
            // Añadir sonido de fallo?
            TerminarPrueba();
        }
    }
    private void TerminarPrueba()
    {
        controlPrueba.numPrueba.Clear();
        controlPrueba.listaNumEncendidos.Clear();
        controlPrueba.enJuego = false;
    }
    private void CambiarMaterialPiezasAlClicar() 
    {
        //Animacion 
        render.material.SetFloat("_Iluminando", 1);
    }

    private void VolverAlMaterialOriginal()
    {
        render.material.SetFloat("_Iluminando", 0);
    }

    private IEnumerator IluminarYApagarLoClicado()
    {
        CambiarMaterialPiezasAlClicar();
        animator.SetTrigger("Pulsar");
        AudioManager.instance.Efecto(AudioManager.instance.Botones);

        yield return new WaitForSeconds(1);
        AudioManager.instance.Efecto(AudioManager.instance.Botones);

        VolverAlMaterialOriginal();
    }
}
