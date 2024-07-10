using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.XR;
using UnityEngine;


public class PunManager : MonoBehaviourPunCallbacks, IManager
{

    RoomOptions _curRoomOptions;

    public RoomOptions CurRoomOptions
    {
        get { return _curRoomOptions; }
        set { _curRoomOptions = value; }
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
        Debug.Log($"Joined Room, CurRoomName = {PhotonNetwork.CurrentRoom.Name}");
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

    }
}
