using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

using PhotonHashtable = ExitGames.Client.Photon.Hashtable;

public class Player_Customizer : MonoBehaviourPun
{
    [SerializeField] public TMP_Dropdown selectDropdown;
    [SerializeField] public List<GameObject> playerPrefabs;
    [SerializeField] public static List<GameObject> playerPrefabNames;
    [SerializeField] public string currentName;
    [SerializeField] public static string referenceString;

    [HideInInspector] public int playerIndex;
    private PhotonHashtable hash = new PhotonHashtable();
    private List<string> playerNames = new List<string>();

    private void Start()
    {
        playerPrefabNames = playerPrefabs;

        selectDropdown.ClearOptions();

        foreach (var playerPrefab in playerPrefabs)
        {
            playerNames.Add(playerPrefab.name);
        }

        selectDropdown.AddOptions(playerNames);
 
        selectDropdown.RefreshShownValue();

        SetPlayer(playerIndex);

    }

    public void SetPlayer(int index)
    {
        playerIndex = index;
        currentName = playerNames[index];

        if (PhotonNetwork.IsConnectedAndReady)
        {
            SetHash();
        }
    }
    public void SetHash()
    {
        hash["pName"] = currentName;
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        referenceString = (string)PhotonNetwork.LocalPlayer.CustomProperties["pName"];
        Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties.Count);
    }
}
