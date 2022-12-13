using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Photon_Manager : MonoBehaviourPunCallbacks
{
    public static Photon_Manager instance;

    [SerializeField] TMP_InputField nameField;
    [SerializeField] TMP_InputField roomNameField;
    [SerializeField] TMP_InputField joinRoomField;

    [SerializeField] GameObject createJoinCanvas;
    [SerializeField] GameObject logInCanvas;
    [SerializeField] GameObject createCanvas;
    [SerializeField] GameObject joinCanvas;

    [SerializeField] string sceneToLoad;

    [SerializeField] public bool createRoomStatus = false;
    [SerializeField] public bool joinRoomStatus = false;
    [SerializeField] public bool logInStatus = false;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            gameObject.SetActive(false);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room: " + PhotonNetwork.CurrentRoom.Name);
    }


    void Start()
    {
        Debug.Log(" started");
    }

    public void LogIn()
    {
        string temp = nameField.text;
        PhotonNetwork.LocalPlayer.NickName = temp;
        PhotonNetwork.ConnectUsingSettings();
        logInCanvas.SetActive(false);
        createJoinCanvas.SetActive(true);
        logInStatus = true;
        Debug.Log("LogIn Done");
    }

    public void CreateRoom()
    {
        string roomName = roomNameField.text;
        PhotonNetwork.CreateRoom(roomName);
        createCanvas.SetActive(false);
        ChangeScene(sceneToLoad);
        createRoomStatus = true;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined " + roomName);
    }

    public void JoinRoom()
    {
        string tempRoomName = joinRoomField.text;
        PhotonNetwork.JoinRoom(tempRoomName);
        joinCanvas.SetActive(false);
        ChangeScene(sceneToLoad);
        joinRoomStatus = true;
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " joined " + tempRoomName);
    }

    public void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
