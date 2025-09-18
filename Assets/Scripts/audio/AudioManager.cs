using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{

    public Transform Player;
    public Transform Torre;
    public float Distancia;

    private float volumen_Oceano;


    [SerializeField] private AudioMixer MIXER;
    [SerializeField] private AnimationCurve mapSonido;

    [Header("AUDIO SPURCES")]
    
    [SerializeField] AudioSource Musica;
    [SerializeField] AudioSource Efectos;
    [SerializeField] AudioSource Ambiente;


    [Space]
    [SerializeField] AudioSource OC_1;
 
    [Header("Clips Audios")]
    public AudioClip musica_clip;
    public AudioClip Telepor_clip;
    public AudioClip Bosque_clip;
    public AudioClip Playa_clip;
    public AudioClip temporizador;
    public AudioClip Puzzle_Completado;
    public AudioClip Botones;

    public AudioClip energia;
    public AudioClip Rayo;


    public static AudioManager instance;
    // Start is called before the first frame update
    void Start()
    {
       if(instance == null) 
        {
            instance = this;        
        }
       
        
        
        Player = GameObject.Find("PlayerCapsule").transform;
        Distancia = Vector3.Distance(Player.position, Torre.position);


        Musica.clip = musica_clip;
        Musica.Play();

        Ambiente.clip = Bosque_clip;
        Ambiente.Play();

        OC_1.clip = Playa_clip;
        
        OC_1.Play();


       
    }

    // Update is called once per frame
    void Update()
    {/*
        if (instance == null)
        {
            //  instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else { Destroy(gameObject); }*/

        Distancia = Vector3.Distance(Player.position, Torre.position);
        volumen_Oceano = Distancia / 250f;

        MIXER.SetFloat("Playa-VOL", (mapSonido.Evaluate(volumen_Oceano) -1) * 40); //ajuste Volumen



    }

    public void CambioMusica(AudioClip Clip)
    {
        Musica.clip = Clip;
        Musica.PlayOneShot(Clip);

    }

    public void Efecto(AudioClip clip) { Efectos.PlayOneShot(clip); }

    public void TIK_TAK() {

        Efectos.clip = temporizador;
        Efectos.Play();
    }

    public void Energia()
    {

        Efectos.clip = energia;
        Efectos.Play();
    }

    public void STOP_EFECTO() { Efectos.Stop(); }




}
