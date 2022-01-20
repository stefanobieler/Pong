using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Netcode.Transports.Enet;
using System;

public class ConnectionManager : MonoBehaviour
{


    [SerializeField] private GameObject playerObject;

    private NetworkSceneManager nwSceneManager;
    private EnetTransport transport;
    private string ipAddress;

    private void OnEnable()
    {

    }

    private void Start()
    {

        NetworkManager.Singleton.OnClientConnectedCallback += OnPlayerConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnPlayerDisconnected;
    }


    private void OnPlayerConnected(ulong clientId)
    {
        Debug.Log($"Player connected {clientId}");
        StartCoroutine(SpawnPlayer(clientId));
    }

    private void OnPlayerDisconnected(ulong clientId)
    {

    }

    private IEnumerator SpawnPlayer(ulong clientId)
    {
        yield return new WaitForSeconds(2);

        List<PaddleSpawnPoint> spawnPoints = PaddleSpawnPoint.spawnPoints;

        int numberOfConnectedClinets = NetworkManager.Singleton.ConnectedClients.Count;
        GameObject player;


        switch (numberOfConnectedClinets)
        {
            case 1:
                player = Instantiate(playerObject, spawnPoints[0].transform.position, Quaternion.identity);
                player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
                break;
            case 2:
                player = Instantiate(playerObject, spawnPoints[1].transform.position, Quaternion.identity);
                player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
                break;
            default:
                //you're just gonna watch.
                Debug.Log("You're just gonna sit there, and watch oky?");
                break;
        }


    }

    public void UpdateIpAddress(string ipAddress)
    {
        this.ipAddress = ipAddress;
    }

    public void HostServer()
    {
        NetworkManager.Singleton.StartHost();
        nwSceneManager = NetworkManager.Singleton.SceneManager;
        nwSceneManager.LoadScene("Level_01", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void JoinServer()
    {
        transport = NetworkManager.Singleton.GetComponent<EnetTransport>();
        transport.Address = ipAddress;
        NetworkManager.Singleton.StartClient();
    }

}
