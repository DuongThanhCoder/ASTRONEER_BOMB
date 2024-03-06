using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConstructerGameLan : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject blueplayer;
    public GameObject greenplayer;
    public GameObject redplayer;
    public GameObject yellowplayer;
    public GameObject player0;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    int numConnections;
    void Start()
    {
        numConnections = NetworkManager.Singleton.ConnectedClientsList.Count;
        Debug.Log("Number of connected clients: " + numConnections);
        if (numConnections == 1)
        {
                Debug.Log("Day la host");
                player0 = Instantiate(blueplayer, new Vector2(-9.5f, 4.5f), Quaternion.identity);
                player0.GetComponent<NetworkObject>().Spawn();
                

        }
        if (numConnections == 2)
        {
                Debug.Log("Day la host");
                player0 = Instantiate(blueplayer, new Vector2(-9.5f, 4.5f), Quaternion.identity);
                player0.GetComponent<NetworkObject>().Spawn();
                player1 = Instantiate(greenplayer, new Vector2(14.5f, -9.5f), Quaternion.identity);
                player1.GetComponent<NetworkObject>().Spawn();
                player1.GetComponent<NetworkObject>().ChangeOwnership(1);

        }
        if (numConnections == 3)
        {
                Debug.Log("Day la host");
                player0 = Instantiate(blueplayer, new Vector2(-9.5f, 4.5f), Quaternion.identity);
                player0.GetComponent<NetworkObject>().Spawn();
                player1 = Instantiate(greenplayer, new Vector2(14.5f, -9.5f), Quaternion.identity);
                player1.GetComponent<NetworkObject>().Spawn();
                player1.GetComponent<NetworkObject>().ChangeOwnership(1);
                player2 = Instantiate(redplayer, new Vector2(-9.5f, -9.5f), Quaternion.identity);
                player2.GetComponent<NetworkObject>().Spawn();
                player2.GetComponent<NetworkObject>().ChangeOwnership(2);
        }
        if (numConnections == 4)
        {
                Debug.Log("Day la host");
                player0 = Instantiate(blueplayer, new Vector2(-9.5f, 4.5f), Quaternion.identity);
                player0.GetComponent<NetworkObject>().Spawn();
                player1 = Instantiate(greenplayer, new Vector2(14.5f, -9.5f), Quaternion.identity);
                player1.GetComponent<NetworkObject>().Spawn();
                player1.GetComponent<NetworkObject>().ChangeOwnership(1);
                player2 = Instantiate(redplayer, new Vector2(-9.5f, -9.5f), Quaternion.identity);
                player2.GetComponent<NetworkObject>().Spawn();
                player2.GetComponent<NetworkObject>().ChangeOwnership(2);
                player3 = Instantiate(yellowplayer, new Vector2(14.5f, 4.5f), Quaternion.identity);
                player3.GetComponent<NetworkObject>().Spawn();
                player3.GetComponent<NetworkObject>().ChangeOwnership(3);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
