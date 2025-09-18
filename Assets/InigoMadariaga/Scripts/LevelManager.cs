using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System;


public class LevelManager : MonoBehaviour
{
    private string _gameScene;
    public static SaveData data;
    [System.Serializable]
    public class SaveData
    {
        public bool puzzle1Terminado;
        public bool puzzle2Terminado;
        public bool puzzle3Terminado;
        public bool puzzle4Terminado;
        public bool puzzleSDTerminado;
        public bool animacionSecundariaTerminada;
        public bool animacionSDTerminada;
       
       
        public GameState state;

       
    }

    public static LevelManager instance;
    public delegate void QuitarBloqueo();
    public static event QuitarBloqueo onQuitar;

    //[Header("Variables que controlan el juego")]
    //[Space]
    //public bool puzzle1Terminado;
    //public bool puzzle2Terminado;
    //public bool puzzle3Terminado;
    //public bool puzzle4Terminado;
    //public bool puzzleSDTerminado;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _gameScene = SceneManager.GetActiveScene().name;
        SceneManager.sceneUnloaded += OnSceneUnload;
        LoadThings();
        
        
    }
    public void OnApplicationQuit()
    {
       if (!Application.isEditor) SaveThings();
    }

    private void OnSceneUnload(UnityEngine.SceneManagement.Scene arg0)
    {
        if (arg0.name == _gameScene) SaveThings();
    }
    private void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnload;
    }

    public void Quitar()
    {
        onQuitar.Invoke();
    }
    public void SaveThings()
    {
        /* data.puzzle1Terminado = puzzle1Terminado;
         data.puzzle2Terminado = puzzle2Terminado;
         data.puzzle3Terminado = puzzle3Terminado;
         data.puzzle4Terminado = puzzle4Terminado;
         data.puzzleSDTerminado = puzzleSDTerminado;
         data.animacionSecundariaTerminada = CinematicManager.Instance.animacionSecundariaTerminada;
         data.animacionSDTerminada = CinematicManager.Instance.animacionSDTerminada;
         data.state = PruebaManager.instance.State;*/
       
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadThings()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            data = JsonUtility.FromJson<SaveData>(json);

            /*puzzle1Terminado = data.puzzle1Terminado;
            puzzle2Terminado = data.puzzle2Terminado;
            puzzle3Terminado = data.puzzle3Terminado;
            puzzle4Terminado = data.puzzle4Terminado;
            puzzleSDTerminado = data.puzzleSDTerminado;
            CinematicManager.Instance.animacionSecundariaTerminada = data.animacionSecundariaTerminada;
            CinematicManager.Instance.animacionSDTerminada = data.animacionSDTerminada;
            PruebaManager.instance.State = data.state;*/
        }
        else
        {
            data = new SaveData();
            data.puzzle1Terminado = false;
            data.puzzle2Terminado = false;
            data.puzzle3Terminado = false;
            data.puzzle4Terminado = false;
            data.puzzleSDTerminado = false;
            data.animacionSecundariaTerminada = false;
            data.animacionSDTerminada = false;
            data.state = GameState.SinEmpezar;
        }

    }
}


