using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System.Linq;
using System;
using StarterAssets;
using System.IO;

public class CinematicManager : MonoBehaviour
{
    public static CinematicManager Instance;    
    private CinemachineVirtualCamera cameraPrincipal;
    private FirstPersonController playerController;
    private CinemachineBrain brain;

    [Header("Variables de la cinematica de Simon Dice")]
    [Space]    
    [SerializeField] private CinemachineVirtualCamera cameraSimonDice;
    [SerializeField] private CinemachineVirtualCamera cameraTransicionSD;
    [SerializeField] private CinemachineVirtualCamera cameraFinalSD;
    [SerializeField] private Transform lookAtSD;
    [SerializeField] private Material matCristalSD;
    [SerializeField] private GameObject RayoTorreSimon;
    private CinemachineTrackedDolly dollySimon;
    private CinemachineTrackedDolly dollyTransicion;
    private float t;
    private float tt;
    private float ttt;   
    private float nuevaPosY;
    private bool musicaEnergiaSDEnProceso;
    
    [Space]
    [Header("Variables de la cinematica de la otra torre")]
    [Space]
    [SerializeField] private CinemachineVirtualCamera cameraTransicionSecundaria;
    [SerializeField] private CinemachineVirtualCamera cameraRotacionSecundaria;
    [SerializeField] private CinemachineVirtualCamera cameraFinalSecundaria;
    [SerializeField] private Transform lookAtSecundaria;
    [SerializeField] private Material matCristalSecundaria;
    [SerializeField] private GameObject RayoTorreEsferas;
    private CinemachineTrackedDolly dollyTranSecundaria;
    private CinemachineTrackedDolly dollyRotSecundaria;
    private float t1secundaria;
    private float t2secundaria;
    private float t3secundaria;   
    private float nuevaPosYSecundaria;
    private bool musicaEnergiaSecundariaEnProceso;
   
    
    [Space]
    [Header("Variables de la cinematica del puente")]
    [Space]
    [SerializeField] private CinemachineVirtualCamera cameraPuente;
    [SerializeField] private ControlDestruccion control;
   


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        dollySimon = cameraSimonDice.GetCinemachineComponent<CinemachineTrackedDolly>();
        dollyTransicion = cameraTransicionSD.GetCinemachineComponent<CinemachineTrackedDolly>();
        dollyTranSecundaria = cameraTransicionSecundaria.GetCinemachineComponent<CinemachineTrackedDolly>();
        dollyRotSecundaria = cameraRotacionSecundaria.GetCinemachineComponent<CinemachineTrackedDolly>();
        cameraPrincipal = GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();
        playerController = FindObjectOfType<FirstPersonController>();
        brain = FindObjectOfType<CinemachineBrain>();

        
       
        
        //  Resources.FindObjectsOfTypeAll<CinemachineBrain>(); PARA BUSCAR EL CINEMACHINE BRAIN SI NO DETECTA LA OTRA MANERA
        //  ������ HEMOS CAMBIADO EL FADE IN OUT DEL CINEMACHINE BRAIN A 0.5, POR SI SE PIERDE EN EL SOURCETREE !!!!!!!
        //  ������ HEMOS CAMBIADO EL FADE IN OUT DEL CINEMACHINE BRAIN A 0.5, POR SI SE PIERDE EN EL SOURCETREE !!!!!!!

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeScreenshot();
        }
    

  


        if (LevelManager.data.puzzle1Terminado && !LevelManager.data.animacionSecundariaTerminada) 
        {
            CinematicaSecundaria();  
        }
        if (LevelManager.data.puzzleSDTerminado && !LevelManager.data.animacionSDTerminada)
        {
            ComenzarSimonDice();
        }
       
            
    }

    void TakeScreenshot()
    {
        // Obtiene la ruta del escritorio del usuario
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // Nombre del archivo con fecha y hora
        string fileName = "UnityScreenshot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

        // Ruta completa
        string fullPath = Path.Combine(desktopPath, fileName);

        // Captura la pantalla
        ScreenCapture.CaptureScreenshot(fullPath);

        Debug.Log("Pantallazo guardado en: " + fullPath);
    }
    private void OnDestroy()
    {
        matCristalSecundaria.SetFloat("_encendido", 0);
        matCristalSD.SetFloat("_encendido", 0);
    }

   
    #region CinematicaSD
    public void ComenzarSimonDice()
    {
        playerController.enabled = false;
        cameraTransicionSD.gameObject.SetActive(true);
        brain.m_DefaultBlend.m_Time = 0.5f; //PARA MODIFICAR LA VELOCIDAD DE LA QUE PASA DE UNA CAMARA A OTRA

         tt += Time.deltaTime * 0.5f;

        dollyTransicion.m_PathPosition = Mathf.Lerp(0, 1, tt);
        if(dollyTransicion.gameObject.activeSelf && dollyTransicion.m_PathPosition > 0.4f) 
        {
            cameraTransicionSD.transform.rotation = Quaternion.Lerp(cameraTransicionSD.transform.rotation, Quaternion.Euler(0, 105, 0), Time.deltaTime * 2);
            /*CinemachineSmoothPath path = dollyTransicion.m_Path as CinemachineSmoothPath;
            Vector3 localPoint = path.m_Waypoints[path.m_Waypoints.Length - 1].position;
            Vector3 worldPoint = path.transform.TransformPoint(localPoint);*/
           
            if (tt >= 1f)
            {
               EmpezarGiroTorreSD();
            }
        }          
        
    }

    private void EmpezarGiroTorreSD()
    {
        playerController.enabled = false;
        brain.m_DefaultBlend.m_Time = 1f; //PARA MODIFICAR LA VELOCIDAD DE A QUE VELOCIDAD PASA DE UNA CAMARA A OTRA
        cameraTransicionSD.gameObject.SetActive(false);
        cameraSimonDice.gameObject.SetActive(true);
        t += Time.deltaTime * 0.1f;

        dollySimon.m_PathPosition = Mathf.Lerp(0, 1, t);
        //Vector3 nuevaPos = Vector3.Lerp(lookAtSD.localPosition, new Vector3(lookAtSD.localPosition.x, 10.5f, lookAtSD.localPosition.z), t*0.2f);
        //lookAtSD.localPosition = nuevaPos;

        nuevaPosY = Mathf.Lerp(0, 10.5f, t);
        lookAtSD.localPosition = new Vector3(lookAtSD.localPosition.x, nuevaPosY, lookAtSD.localPosition.z);
        if(dollySimon.m_PathPosition >= 1)
        {
            CambiarMaterialCristalSD();
        }
    }

    private void CambiarMaterialCristalSD()
    {
       
        ttt += Time.deltaTime * 0.3f;
        matCristalSD.SetFloat("_encendido", Mathf.Lerp(0,1, ttt));

        if (!musicaEnergiaSDEnProceso)
        {
            AudioManager.instance.Energia();
            musicaEnergiaSDEnProceso = true;

        }
        if (matCristalSD.GetFloat("_encendido") >= 1f && !LevelManager.data.animacionSDTerminada)
        {
            LevelManager.data.animacionSDTerminada = true;
            StartCoroutine(UltimaFaseCinematicaSD());
        }

    }

    private IEnumerator UltimaFaseCinematicaSD()
    {
        cameraFinalSD.gameObject.SetActive(true);
        cameraSimonDice.gameObject.SetActive(false);
        RayoTorreSimon.SetActive(true);
        AudioManager.instance.STOP_EFECTO();
        yield return null;
        AudioManager.instance.Efecto(AudioManager.instance.Rayo);


        yield return new WaitForSeconds(2);
        if(LevelManager.data.animacionSecundariaTerminada) 
        {
            brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut; //NO TENGO NI IDEA SI FUNCIONA
            
            yield return null;
            cameraPuente.gameObject.SetActive(true);
            AudioManager.instance.Efecto(AudioManager.instance.Puzzle_Completado);
            control.Arreglar();
            yield return new WaitForSeconds(8);
            cameraPuente.gameObject.SetActive(false);
        }
        brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut; //NO TENGO NI IDEA SI FUNCIONA
        cameraFinalSD.gameObject.SetActive(false);
        yield return null;
        brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        brain.m_DefaultBlend.m_Time = 1f;
        
        //Reactivamos el control del jugador
        playerController.enabled = true;
        Debug.Log("Cinematica terminada");
    }
    #endregion

   
    
    #region CinematicaSecundaria

    private void CinematicaSecundaria()
    {
       cameraTransicionSecundaria.gameObject.SetActive(true);
        t1secundaria += Time.deltaTime * 0.5f;
        dollyTranSecundaria.m_PathPosition = Mathf.Lerp(0, 1, t1secundaria);
        if(dollyTranSecundaria.m_PathPosition >= 1)
        {
            EmpezarGiroSecundaria();
        }
    }

    private void EmpezarGiroSecundaria()
    {
        playerController.enabled = false;
        cameraTransicionSecundaria.gameObject.SetActive(false);
        cameraRotacionSecundaria.gameObject.SetActive(true);
        t2secundaria += Time.deltaTime * 0.1f;
        dollyRotSecundaria.m_PathPosition = Mathf.Lerp(0, 1, t2secundaria);
        nuevaPosYSecundaria = Mathf.Lerp(0, 21, t2secundaria);
        lookAtSecundaria.localPosition = new Vector3(lookAtSecundaria.localPosition.x, nuevaPosYSecundaria, lookAtSecundaria.localPosition.z);

        if(dollyRotSecundaria.m_PathPosition >= 1)
        {
            CambiarMaterialCristalSecundaria();
        }
    }

    private void CambiarMaterialCristalSecundaria()
    {
        t3secundaria += Time.deltaTime * 0.3f;
        matCristalSecundaria.SetFloat("_encendido", Mathf.Lerp(0, 1, t3secundaria));

        if (!musicaEnergiaSecundariaEnProceso)
        {
            AudioManager.instance.Energia();
            musicaEnergiaSecundariaEnProceso = true;

        }
        if (t3secundaria >= 1 && !LevelManager.data.animacionSecundariaTerminada)
        {
            LevelManager.data.animacionSecundariaTerminada = true;
            StartCoroutine(UltimaFaseCinematicaSecundaria());        
           
        }
    }

    private IEnumerator UltimaFaseCinematicaSecundaria()
    {
        cameraFinalSecundaria.gameObject.SetActive(true);
        cameraRotacionSecundaria.gameObject.SetActive(false);
        RayoTorreEsferas.SetActive(true);
        AudioManager.instance.STOP_EFECTO();
        yield return null;

        AudioManager.instance.Efecto(AudioManager.instance.Rayo);

        yield return new WaitForSeconds(2);
        if(LevelManager.data.animacionSDTerminada) 
        {
            brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut; //NO TENGO NI IDEA SI FUNCIONA
           
            yield return null;            
            cameraPuente.gameObject.SetActive(true);
            AudioManager.instance.Efecto(AudioManager.instance.Puzzle_Completado);
            control.Arreglar();
            yield return new WaitForSeconds(8);
            cameraPuente.gameObject.SetActive(false);

        }
        //Volvemos a la camara principal
        
        brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
        
        
        //NO TENGO NI IDEA SI FUNCIONA
        cameraFinalSecundaria.gameObject.SetActive(false);
        yield return null;

        //Reactivamos el control del jugador
        playerController.enabled = true;
        brain.m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
        brain.m_DefaultBlend.m_Time = 1f;
        Debug.Log("Cinematica terminada");
    }
   
    #endregion
}
