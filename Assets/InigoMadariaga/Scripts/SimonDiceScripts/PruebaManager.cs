using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PruebaManager : MonoBehaviour
{
    [Header("Variables necesarias para el funcionamiento de la prueba")]
    public List<int> listaNumEncendidos; // Lista donde guardamos los numeros de las piezas que hay que clicar
    public List<int> numPrueba; // Lista donde guardamos los numeros de las piezas que hemos clicado    
    private int maxNumero = 9; // Numero maximo que pueda salir
    private int numAnteriorIluminado = -1; // Variable para guardar el numero anterior que ha salido en la lista de numAClicar para que no salga el mismo
    private int numVueltaActual = 0; // Variable para que la corrutina sepa en que vuelta estamos
    [SerializeField] private GameObject[] objectShown; // Array donde almacenamos todos los GameObjects que pueden brillar
    [Header("Variables para el control de la fase del juego")]
    [SerializeField] private MeshRenderer[] cristalesFasesSuperadas; // Cristales que brillaran para saber en que fase estamos. SEGURAMENTE CAMBIAR CON SHADERS    
    
    //public GameState State = GameState.SinEmpezar;   
    public static event UnityAction<GameState> onGameStateChanged;
    private Animator animator;
    public static PruebaManager instance;
     

    public bool enJuego = false; // Variable que nos indica si estamos jugando o no, para no poder pulsar mas veces. Quiza nos haga falta otro para que el jugador no pueda pulsar los resultados antes de tiempo
    public bool animacionesEnProceso = false;

    private void Awake()
    {
        onGameStateChanged += onOnGameStateChanged;
        animator = GetComponent<Animator>();
        if(instance == null ) { instance = this; }

    }

    private void Start()
    {
        
        
        UpdateGameState(LevelManager.data.state);
    }
    private void OnMouseDown() // Funcion para que el boton pueda leerlo porque no podemos pasarle el otro por el valor del GameState, en el juego seguramente se haga de otra manera
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        CrearListaNivel(LevelManager.data.state);
    }
    public void LanzarLista()
    {
        animator.SetTrigger("Pulsar");
        CrearListaNivel(LevelManager.data.state);
    }

    public void CrearListaNivel(GameState state)
    {
       if(enJuego == false && animacionesEnProceso == false)
       {
            animacionesEnProceso=true;
           
            if (state == GameState.SinEmpezar)
            {
                
                StartCoroutine(DarTiempoAPlayer(numAnteriorIluminado, numVueltaActual, 4));
                numVueltaActual = 0; // Para que las siguientes fases no exploten
               


            }
            else if (state == GameState.Nivel1)
            {
                
                StartCoroutine(DarTiempoAPlayer(numAnteriorIluminado, numVueltaActual, 5));
                numVueltaActual = 0;
                

            }
            else if (state == GameState.Nivel2)
            {
                
                StartCoroutine(DarTiempoAPlayer(numAnteriorIluminado, numVueltaActual, 6));
                numVueltaActual = 0;       

            }
       }  
      
    }
    //Esta variable solo la usamos para retrasar el inicio del otro, para darle tiempo al jugador
    //Si usamos el numerator de AñadirAListaNivel, quitar este
    //JUEVES: EN VEZ DE QUE LLAMEN A AÑADIR LISTA CADA UNO, QUE TODOS LLAMEN A DAR TIEMPO A PLAYER Y
    // BORRAR EL DE ARRIBA, PARA AÑADIR UN DELAY SOLO ENTRE CLICAR Y VER LAS COSAS
    private IEnumerator DarTiempoAPlayer(int numAnterior, int vueltaActual, int vueltaMaxima)
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(AñadirAListaNivel(numAnterior, vueltaActual, vueltaMaxima));
    }

    private  IEnumerator AñadirAListaNivel(int numAnterior, int vueltaActual, int vueltaMaxima)
    {
        AudioManager.instance.TIK_TAK();

        //yield return new WaitForSeconds(2); //Por si queremos darle mas tiempo entre cada iluminacion
        int añadirALista = Random.Range(0, maxNumero);
       
        while (añadirALista == numAnterior)
        {
            añadirALista = Random.Range(0, maxNumero); //Esto no se si funcionara
        }

        listaNumEncendidos.Add(añadirALista);
        IluminarLasPiezas(añadirALista);
        numAnteriorIluminado = añadirALista;      
        yield return new WaitForSeconds(2); //Aqui esperamos al tiempo
        VolverLasPiezasAlMaterialOriginal(añadirALista);
        if(vueltaActual < vueltaMaxima)
        {
            vueltaActual++;
            StartCoroutine(AñadirAListaNivel(numAnteriorIluminado, vueltaActual, vueltaMaxima));
            print(vueltaActual);
        }
        else
        {
            AudioManager.instance.STOP_EFECTO();
            enJuego = true;
            animacionesEnProceso = false;
            
        }
    }
    private void IluminarLasPiezas(int numIluminado) // CAMBIAMOS EL MATERIAL PARA ILUMINAR LA PIEZA
    {
        Material matObjetoSeleccionado = objectShown[numIluminado].GetComponent<PiezasIluminadas>().matPieza;
        matObjetoSeleccionado.SetFloat("_Iluminando", 1);
     
      
       
    }

    private void VolverLasPiezasAlMaterialOriginal(int numIluminado) // Cambiamos de nuevo el material a su material base
    {
        Material matObjetoSeleccionado = objectShown[numIluminado].GetComponent<PiezasIluminadas>().matPieza;
        matObjetoSeleccionado.SetFloat("_Iluminando", 0);
       
    }
    public void UpdateGameState(GameState newState)
    {
        LevelManager.data.state = newState;

        switch (newState)
        {
            case GameState.Nivel1:
                break;
            case GameState.Nivel2:
                break;            
            case GameState.Completado:
                break;
            case GameState.SinEmpezar:
                break;
            default: Debug.Log("La prueba no esta empezada");
                break;
        }

        onGameStateChanged?.Invoke(newState);


    }

    private void OnDestroy()
    {
        onGameStateChanged -= onOnGameStateChanged;

    }


    private void onOnGameStateChanged(GameState state)
    {
       
        //ILUMINACION CRISTALES
        if(state == GameState.Nivel1)
        {
            cristalesFasesSuperadas[0].material.SetFloat("_encendido", 1);
            
        }
        if(state == GameState.Nivel2)
        {
            cristalesFasesSuperadas[0].material.SetFloat("_encendido", 1);
            cristalesFasesSuperadas[1].material.SetFloat("_encendido", 1);
        }
        if(state == GameState.Completado)
        {
            cristalesFasesSuperadas[0].material.SetFloat("_encendido", 1);
            cristalesFasesSuperadas[1].material.SetFloat("_encendido", 1);
            cristalesFasesSuperadas[2].material.SetFloat("_encendido", 1);
            LevelManager.data.puzzleSDTerminado = true;
            //AQUI TAMBIEN SEGURAMENTE TENGAMOS QUE PONER LA CINEMATICA DE LA TORRE, EL RAYO ETC
        }
    }
   
}



public enum GameState
{
    SinEmpezar,
    Nivel1,
    Nivel2,   
    Completado
   
    
}