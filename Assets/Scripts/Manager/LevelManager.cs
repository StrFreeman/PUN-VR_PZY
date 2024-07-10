using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour, IManager
{
    Vector3 XROriginLocalPos = new Vector3 (0, 0, 0.69f);
    Vector3 XROriginLocalEulerAngles = new Vector3(0, -90, 0);

    [SerializeField] GameObject _startLocationGO;
    [SerializeField] GameObject _XROriginGO;
    [SerializeField] GameObject _tc1GO;
    [SerializeField] GameObject _tc2GO;
    [SerializeField] GameObject _startLocation1GO;
    [SerializeField] GameObject _startLocation2GO;


    public GameObject XROriginGO
    {
        get { return _XROriginGO; }
        private set { _XROriginGO = value;}

    }

    public GameObject TC1GO { get { return _tc1GO; } }
    public GameObject TC2GO { get { return _tc2GO; } }
    public GameObject StartLocation1GO { get { return _startLocation1GO; } }
    public GameObject StartLocation2GO { get { return _startLocation2GO; } }
    private void InitPlayerPosition()
    {
        TowerCrane tc1 = TC1GO.GetComponent<TowerCrane>();
        tc1.SetLoadToLoaction(StartLocation1GO);
        TowerCrane tc2 = TC2GO.GetComponent<TowerCrane>();
        tc2.SetLoadToLoaction(StartLocation2GO);

        switch (MonoApplication.Instance.CurPlayerRole)
        {
            case MonoApplication.PlayerRole.Player1:
            {
                Transform cabTf = TC1GO.transform.Find("Jib/Cab");
                XROriginGO.transform.parent = cabTf;

                XROriginGO.transform.localEulerAngles = XROriginLocalEulerAngles;
                XROriginGO.transform.localPosition = XROriginLocalPos;


                break;
            }
            case MonoApplication.PlayerRole.Player2:
            {
                Transform cabTf = TC2GO.transform.Find("Jib/Cab");
                XROriginGO.transform.parent = cabTf;

                XROriginGO.transform.localEulerAngles = XROriginLocalEulerAngles;
                XROriginGO.transform.localPosition = XROriginLocalPos;

                break;
            }
            default:
            {
                Debug.Log($"LevelManager: unknown PlayerRole = {MonoApplication.Instance.CurPlayerRole}");
                break;
            }

        }
    }

    public void Init()
    {
        InitPlayerPosition();
    }

    public void PreInit()
    {

    }
    void Awake()
    {
        PreInit();
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
