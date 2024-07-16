using Oculus.Interaction;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.Events;


public class PunManager : MonoBehaviourPunCallbacks, IManager
{
    PhotonView _localManagerPhotonView;

    RoomOptions _curRoomOptions;


    public RoomOptions CurRoomOptions
    {
        get { return _curRoomOptions; }
        set { _curRoomOptions = value; }
    }

    public PhotonView LocalManagerPhotonView
    {
        get { return _localManagerPhotonView; }
        private set { _localManagerPhotonView = value; }
    }

    const string DEFAULT_ROOM_NAME = "Proj: PP, Default Room Name";

    void Awake()
    {
        PreInit();
    }
    // Start is called before the first frame update
    void Start()
    {

        Init();

        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"Connected To Master, UID = {PhotonNetwork.AuthValues.UserId}");

        PhotonNetwork.JoinOrCreateRoom(DEFAULT_ROOM_NAME, CurRoomOptions, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log($"Created Room, RoomName = {DEFAULT_ROOM_NAME}");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Joined Room, CurRoomName = {PhotonNetwork.CurrentRoom.Name}, IsMasterClient = {PhotonNetwork.IsMasterClient}");
    }

    public void EnsureLocalManagerPhotonView()
    {
        LocalManagerPhotonView = PhotonView.Find(3);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        //AuthenticationValues authValues = new AuthenticationValues();
        //authValues.AuthType = CustomAuthenticationType.Custom;

        //authValues.UserId = "0";

        //PhotonNetwork.AuthValues = authValues;
        //PhotonNetwork.ConnectUsingSettings();


        CurRoomOptions = new RoomOptions();
        CurRoomOptions.MaxPlayers = 2;

    }

    public void PreInit()
    {
        switch (MonoApplication.Instance.CurAppState)
        {
            case MonoApplication.AppState.CheckIn:
                {
                    break;
                }
            case MonoApplication.AppState.InLevel:
                {
                    EnsureLocalManagerPhotonView();
                    break;
                }
            case MonoApplication.AppState.CheckOut:
                {
                    break;
                }

        }
    }
}
