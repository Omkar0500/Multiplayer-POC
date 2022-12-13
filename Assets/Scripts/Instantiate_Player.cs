using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Instantiate_Player : MonoBehaviourPun
{
    [SerializeField] List<GameObject> playerPrefab;

    [SerializeField] public string playerName;

    [SerializeField] float xMin;
    [SerializeField] float zMin;
    [SerializeField] float xMax;
    [SerializeField] float zMax;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => PhotonNetwork.IsConnectedAndReady);

        playerPrefab = Player_Customizer.playerPrefabNames;

        Debug.Log(Player_Customizer.referenceString);

        playerName = Player_Customizer.referenceString;

        foreach (var prefab in playerPrefab)
        {
            if (prefab.name == playerName)
            {
                Vector3 playerPos = new Vector3(Random.Range(xMin, xMax), 0.0f, Random.Range(zMin, zMax));
                GameObject obj = PhotonNetwork.Instantiate(playerName, playerPos, Quaternion.identity);
            }
        }
    }
}

