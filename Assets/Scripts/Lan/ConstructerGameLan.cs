using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
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
    public Save save;
    int numberPlay;
    private bool isspawn = false;
    void Start()
    {
        //if (IsHost)
        //{
        //    //gameObject.GetComponent<NetworkObject>().Spawn();
        //    numberPlay = save.teamWithId.Count(item => item > -1);
        //    Debug.Log("Number play: " + numberPlay);
        //    if (save.teamWithId[0] > -1)
        //    {
        //        Debug.Log("Instantiate Blue");
        //        player0 = Instantiate(blueplayer, new Vector2(-9.5f, 5.1f), Quaternion.identity);
        //        player0.GetComponent<NetworkObject>().Spawn();
        //    }
        //    if (save.teamWithId[1] > -1)
        //    {
        //        Debug.Log("Instantiate Green");
        //        player1 = Instantiate(greenplayer, new Vector2(14.5f, -8.9f), Quaternion.identity);
        //        player1.GetComponent<NetworkObject>().Spawn();
        //        player1.GetComponent<NetworkObject>().ChangeOwnership((ulong)save.teamWithId[1]);
        //    }
        //    if (save.teamWithId[2] > -1)
        //    {
        //        Debug.Log("Instantiate Red");
        //        player2 = Instantiate(redplayer, new Vector2(-9.5f, -8.9f), Quaternion.identity);
        //        player2.GetComponent<NetworkObject>().Spawn();
        //        player2.GetComponent<NetworkObject>().ChangeOwnership((ulong)save.teamWithId[2]);
        //    }
        //    if (save.teamWithId[3] > -1)
        //    {
        //        Debug.Log("Instantiate Yellow");
        //        player3 = Instantiate(yellowplayer, new Vector2(14.5f, 5.1f), Quaternion.identity);
        //        player3.GetComponent<NetworkObject>().Spawn();
        //        player3.GetComponent<NetworkObject>().ChangeOwnership((ulong)save.teamWithId[3]);
        //    }
        //}

        //if (numConnections == 1)
        //{
        //        Debug.Log("Day la host");
        //        player0 = Instantiate(blueplayer, new Vector2(-9.5f, 5.1f), Quaternion.identity);
        //        player0.GetComponent<NetworkObject>().Spawn();


        //}
        //if (numConnections == 2)
        //{
        //        Debug.Log("Day la host");
        //        player0 = Instantiate(blueplayer, new Vector2(-9.5f, 5.1f), Quaternion.identity);
        //        player0.GetComponent<NetworkObject>().Spawn();
        //        player1 = Instantiate(greenplayer, new Vector2(14.5f, -8.9f), Quaternion.identity);
        //        player1.GetComponent<NetworkObject>().Spawn();
        //        player1.GetComponent<NetworkObject>().ChangeOwnership(1);

        //}
        //if (numConnections == 3)
        //{
        //        Debug.Log("Day la host");
        //        player0 = Instantiate(blueplayer, new Vector2(-9.5f, 5.1f), Quaternion.identity);
        //        player0.GetComponent<NetworkObject>().Spawn();
        //        player1 = Instantiate(greenplayer, new Vector2(14.5f, -8.9f), Quaternion.identity);
        //        player1.GetComponent<NetworkObject>().Spawn();
        //        player1.GetComponent<NetworkObject>().ChangeOwnership(1);
        //        player2 = Instantiate(redplayer, new Vector2(-9.5f, -8.9f), Quaternion.identity);
        //        player2.GetComponent<NetworkObject>().Spawn();
        //        player2.GetComponent<NetworkObject>().ChangeOwnership(2);
        //}
        //if (numConnections == 4)
        //{
        //        Debug.Log("Day la host");
        //        player0 = Instantiate(blueplayer, new Vector2(-9.5f, 5.1f), Quaternion.identity);
        //        player0.GetComponent<NetworkObject>().Spawn();
        //        player1 = Instantiate(greenplayer, new Vector2(14.5f, -8.9f), Quaternion.identity);
        //        player1.GetComponent<NetworkObject>().Spawn();
        //        player1.GetComponent<NetworkObject>().ChangeOwnership(1);
        //        player2 = Instantiate(redplayer, new Vector2(-9.5f, -8.9f), Quaternion.identity);
        //        player2.GetComponent<NetworkObject>().Spawn();
        //        player2.GetComponent<NetworkObject>().ChangeOwnership(2);
        //        player3 = Instantiate(yellowplayer, new Vector2(14.5f, 5.1f), Quaternion.identity);
        //        player3.GetComponent<NetworkObject>().Spawn();
        //        player3.GetComponent<NetworkObject>().ChangeOwnership(3);
        //}
    }
    public void spawn()
    {
        if (IsHost)
        {
            numberPlay = save.teamWithId.Count(item => item > -1);
            Debug.Log("Number play: " + numberPlay);
            if (save.teamWithId[0] > -1)
            {
                Debug.Log("Instantiate Blue");
                player0 = Instantiate(blueplayer, new Vector2(-9.5f, 5.1f), Quaternion.identity);
                player0.GetComponent<NetworkObject>().Spawn();
            }
            if (save.teamWithId[1] > -1)
            {
                Debug.Log("Instantiate Green");
                player1 = Instantiate(greenplayer, new Vector2(14.5f, -8.9f), Quaternion.identity);
                player1.GetComponent<NetworkObject>().Spawn();
                player1.GetComponent<NetworkObject>().ChangeOwnership((ulong)save.teamWithId[1]);
            }
            if (save.teamWithId[2] > -1)
            {
                Debug.Log("Instantiate Red");
                player2 = Instantiate(redplayer, new Vector2(-9.5f, -8.9f), Quaternion.identity);
                player2.GetComponent<NetworkObject>().Spawn();
                player2.GetComponent<NetworkObject>().ChangeOwnership((ulong)save.teamWithId[2]);
            }
            if (save.teamWithId[3] > -1)
            {
                Debug.Log("Instantiate Yellow");
                player3 = Instantiate(yellowplayer, new Vector2(14.5f, 5.1f), Quaternion.identity);
                player3.GetComponent<NetworkObject>().Spawn();
                player3.GetComponent<NetworkObject>().ChangeOwnership((ulong)save.teamWithId[3]);
            }
            setNameClientRpc();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.IsHost)
        {
            if (gameObject.GetComponent<NetworkObject>().IsSpawned&&!isspawn)
            {
                isspawn = true;
                spawn();
                //Destroy(gameObject, 1f);
            }
        }
        
    }
    [ClientRpc]
    public void testClientRpc()
    {
        Debug.Log("test");
    }
    [ClientRpc]
    public void setNameClientRpc()
    {
        Debug.Log("set name");
        if (save.teamWithId[0] >= 0)
        {
            GameObject bluePlayer = GameObject.Find("BlueLanPlayer(Clone)");
            bluePlayer.GetComponentInChildren<TextMeshProUGUI>().text = save.teamWithName[0];
        }
        if (save.teamWithId[1] >= 0)
        {
            GameObject greenPlayer = GameObject.Find("GreenLanPlayer(Clone)");
            greenPlayer.GetComponentInChildren<TextMeshProUGUI>().text = save.teamWithName[1];
        }
        if (save.teamWithId[2] >= 0)
        {
            GameObject redPlayer = GameObject.Find("RedLanPlayer(Clone)");
            redPlayer.GetComponentInChildren<TextMeshProUGUI>().text = save.teamWithName[2];
        }
        if (save.teamWithId[3] >= 0)
        {
            GameObject yellowPlayer = GameObject.Find("YellowLanPlayer(Clone)");
            yellowPlayer.GetComponentInChildren<TextMeshProUGUI>().text = save.teamWithName[3];
        }
    } 
}
