using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonoApplication : MonoBehaviour
{
    public enum PlayerRole { None, Player1, Player2 }
    public enum AppState { CheckIn, InLevel, CheckOut }
    public enum LevelType { rTrain, cTrain, practice, test, debug };

    private const string APP_GAMEOBJECT_NAME = "Application";


    private static MonoApplication _instance;
    [SerializeField] private AppState _curAppState;

    #region Global Managers
    private CanvasManager _canvasManager;
    private PunManager _punManager;
    #endregion

    #region Local Managers
    private TaskManagerV2 _taskManager;
    private LevelManager _levelManager;
    private PerformanceManager _performanceManager;
    #endregion

    #region Global Manager Properties
    public PunManager PunManager
    {
        get { return _punManager; }
        private set { _punManager = value;}
    }
    public CanvasManager CanvasManager
    {
        get { return _canvasManager; }
        private set { _canvasManager = value; }
    }
    #endregion
    #region Local Manager Properties
    public TaskManagerV2 TaskManager
    {
        get { return _taskManager; }
        private set { _taskManager = value; }
    }
    public LevelManager LevelManager
    {
        get { return _levelManager; }
        private set { _levelManager = value; }
    }
    public PerformanceManager PerformanceManager
    {
        get { return _performanceManager; }
        private set { _performanceManager = value; }
    }
    #endregion



    [SerializeField] private PlayerRole _curPlayerRole;
    [SerializeField] private LevelType _curLevelType;

    private static readonly Dictionary<LevelType, string> LEVEL_TYPE_TO_SCENE_NAME_DICT = new Dictionary<LevelType, string>()
    {
        {LevelType.rTrain, "sc1" }
    };

    private bool _isLoadingScene=false;

    public static MonoApplication Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MonoApplication>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("Application");
                    _instance = obj.AddComponent<MonoApplication>();
                }
            }
            return _instance;
        }

        private set { _instance = value; }
    }





    public AppState CurAppState
    {
        get { return _curAppState; }
        private set { _curAppState = value; }
    } 




    public PlayerRole CurPlayerRole
    {
        get { return _curPlayerRole; }
        set { _curPlayerRole = value; }
    }
    public LevelType CurLevelType
    {
        get { return _curLevelType; }
        set { _curLevelType = value; }
    }

    private bool IsLoadingScene 
    {  
        get { return _isLoadingScene; }
        set { _isLoadingScene = value; }
    }
    


    public bool IsReadyForLevel()
    {
        return _instance != null
            && _curPlayerRole != PlayerRole.None;
    }


    public void StartLevel()
    {
        if (IsReadyForLevel() && CurAppState==AppState.CheckIn && !IsLoadingScene)
        {
            CurAppState = AppState.InLevel;
            var async = SceneManager.LoadSceneAsync(LEVEL_TYPE_TO_SCENE_NAME_DICT[CurLevelType]);

            CanvasManager.LockAll();
            async.completed += (operation) =>
            {
                CanvasManager.UnlockAll();
                CurAppState = AppState.InLevel;
                CanvasManager.UpdateLoadingUI(1f);


            };
            while (!async.isDone)
            {
                CanvasManager.UpdateLoadingUI(async.progress);
            }
            //InitLevel
        }
    }

    private void EnsureGlobalManagers()
    {
        PunManager = EnsureGlobalManager<PunManager>();
        CanvasManager = EnsureGlobalManager<CanvasManager>();
    }

    private void EnsureLoaclManagers()
    {
        TaskManager = EnsureLocalManager<TaskManagerV2>();
        LevelManager = EnsureLocalManager<LevelManager>();
        PerformanceManager = EnsureLocalManager<PerformanceManager>();
    }

    public TManager EnsureGlobalManager<TManager>() where TManager : MonoBehaviour, IManager
    {
        TManager manager;
        if (this.gameObject.TryGetComponent(out TManager managerComponent))
        {
            manager = managerComponent;
        }
        else
        {
            manager = this.gameObject.AddComponent<TManager>();
        }

        return manager;
    }

    public TManager EnsureLocalManager<TManager>() where TManager : MonoBehaviour, IManager
    {
        GameObject localManagerGO = GameObject.Find("LocalManager");
        if (localManagerGO == null)
        {
            localManagerGO = new GameObject("LocalManager");
        }

        TManager manager;
        if (localManagerGO.TryGetComponent(out TManager managerComponent))
        {
            manager = managerComponent;
        }
        else
        {
            manager = localManagerGO.AddComponent<TManager>();
        }

        return manager;
    }





    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);


        EnsureGlobalManagers();

        EnsureLoaclManagers();

    }



    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
