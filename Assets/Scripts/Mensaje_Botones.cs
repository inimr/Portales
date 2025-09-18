using UnityEngine;
using TMPro;
using DG.Tweening;


public class Mensaje_Botones : MonoBehaviour
{
    [SerializeField] private GameObject Caja_texto;
    [SerializeField] private TextMeshProUGUI Texto;
    [SerializeField] private CanvasGroup AlFA;

    [Header("Teclas")]

    [SerializeField] private GameObject A;
    [SerializeField] private GameObject D;
    [SerializeField] private GameObject E;


    [Header("Objetos no canvas")]

    [SerializeField] private Transform Objetivo_INTERACTUAR;
    [SerializeField] private Transform objetivo_ROTAR;
    [SerializeField] private Transform objetivo_PELOTA;

    private Transform Player;
    private float distancia = 5.0f;
    private float tiempo;
    private bool Activo;
    private Transform _objetivo;

    private bool _objetivoInteractuarVisto;
    private bool _objetivoRotarVisto;
    private bool _objetivoPELOTAVisto;


    // Start is called before the first frame update
    void Start()
    {
       // Caja_texto.SetActive(false);
        E.SetActive(false);

        A.SetActive(false);
        D.SetActive(false);

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        AlFA.alpha = 0;

        Activo = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_objetivo) tiempo += Time.deltaTime;

        /////////////////////////////////////////////////////////////////////    Determinamos cual es el objetivo al que nos hemos acercado

        if (Vector3.Distance(Player.position, Objetivo_INTERACTUAR.position) < distancia && !_objetivo && !_objetivoInteractuarVisto)
        {
            _objetivo = Objetivo_INTERACTUAR;
        }
        else if (Vector3.Distance(Player.position, objetivo_ROTAR.position) < distancia && !_objetivo && !_objetivoRotarVisto)
        {
            _objetivo = objetivo_ROTAR;
        }

        else if (Vector3.Distance(Player.position, objetivo_PELOTA.position) < distancia && !_objetivo && !_objetivoPELOTAVisto) 
        {
            _objetivo = objetivo_PELOTA;
        }


        ///////////////////////////////////////////////////////////////////////////////////////////////-------------------------------INTERACTUAR

        if (_objetivo == Objetivo_INTERACTUAR && tiempo == 0)
        {
            AlFA.DOFade(1,0.5f);
            //Caja_texto.SetActive (true);

            print("mensaje");
                E.SetActive(true);
                Texto.text = "Pulsa   " + "   para interactuar";

          //  tiempo += Time.deltaTime;

        }


      else  if (_objetivo == Objetivo_INTERACTUAR && (tiempo > 10 || Vector3.Distance(Player.position, Objetivo_INTERACTUAR.position) > distancia))
        {
            tiempo = 0;

            AlFA.DOFade(0, 0.5f);

            E.SetActive(false);

            _objetivo = null;

        }

        else if (_objetivo == Objetivo_INTERACTUAR && tiempo > 3)
        {
            _objetivoInteractuarVisto = true;
        }




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////----------ROTAR





        if (_objetivo == objetivo_ROTAR && tiempo == 0)
        {
            AlFA.DOFade(1, 0.5f);
            //Caja_texto.SetActive (true);

            print("mensaje");
            A.SetActive(true);
            D.SetActive(true);
            Texto.text = "Pulsa     " + "     para rotar";

            //  tiempo += Time.deltaTime;

        }


        else if (_objetivo == objetivo_ROTAR && (tiempo > 10 || Vector3.Distance(Player.position, objetivo_ROTAR.position) > distancia))
        {
            tiempo = 0;

            AlFA.DOFade(0, 0.5f);

            A.SetActive(false);
            D.SetActive(false);
            _objetivo = null;

        }

        else if (_objetivo == objetivo_ROTAR && tiempo > 3)
        {
            _objetivoRotarVisto = true;
        }


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////---------- PELOTA



        if (_objetivo == objetivo_PELOTA && tiempo == 0)
        {
            AlFA.DOFade(1, 0.5f);
            //Caja_texto.SetActive (true);

            print("mensaje");
            E.SetActive(true);
            Texto.text = "Pulsa   " + "   para interactuar";

            //  tiempo += Time.deltaTime;

        }


        else if (_objetivo == objetivo_PELOTA && (tiempo > 10 || Vector3.Distance(Player.position, objetivo_PELOTA.position) > distancia))
        {
            tiempo = 0;

            AlFA.DOFade(0, 0.5f);

            E.SetActive(false);

            _objetivo = null;

        }

        else if (_objetivo == objetivo_PELOTA && tiempo > 3)
        {
            _objetivoPELOTAVisto = true;
        }





    }






}
