using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManager : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject blueplayer;
    public GameObject greenplayer;
    public GameObject redplayer;
    public GameObject yellowplayer;
    public GameObject kiemxoatprefab;
    //int numConnections;
    public GameObject player0;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    private GameObject go0;
    private GameObject go1;
    private GameObject go2;
    private GameObject go3;
    //[SerializeField] private UnityEngine.UI.Button SanSang;
    //[SerializeField] private UnityEngine.UI.Button HuySanSang;
    //private int soluongss = 0;
    //private int songuoichoi = 0;
    private void Awake()
    {


        //SanSang.onClick.AddListener(() =>
        //{
        //    if (NetworkManager.IsClient)
        //    {
        //        Debug.Log("vklss");
        //        SanSangServerRpc();
        //    }
        //});
        //HuySanSang.onClick.AddListener(() =>
        //{
        //    if (NetworkManager.IsClient)
        //    {
        //        Debug.Log("vklhuy");
        //        HuySanSangServerRpc();
        //    }
        //});
    }
    void Start()
    {
        
        if (NetworkManager.IsHost)
        {
            Debug.Log("Day la host");
            player0 = Instantiate(blueplayer, new Vector2(-6f, 0.65f), Quaternion.identity);
            player0.GetComponent<NetworkObject>().Spawn();
            player0.GetComponent<PlayerLanController>().Start();
            player0.GetComponent<PlayerLanController>().enabled = false;

            go0 = Instantiate(kiemxoatprefab);
            go0.GetComponent<NetworkObject>().Spawn();
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        //if (NetworkManager.IsHost)
        //{
        //    numConnections = NetworkManager.Singleton.ConnectedClientsList.Count;
        //    //songuoichoi = numConnections;
        //    Debug.Log("Number of connected clients: " + numConnections);

        //}

    }
    private void OnClientConnected(ulong clientId)
    {
        Debug.Log("Client Id " + clientId);
        if (clientId == 1)
        {
            Debug.Log("Client connected. You can now call ServerRpc methods.");
            // Call your ServerRpc method here or trigger an event to notify other scripts
            player1 = Instantiate(greenplayer, new Vector2(-2f, 0.65f), Quaternion.identity);
            player1.GetComponent<NetworkObject>().Spawn();
            player1.GetComponent<PlayerLanController>().enabled = false;
            player1.GetComponent<NetworkObject>().ChangeOwnership(1);
            go1 = Instantiate(kiemxoatprefab);
            go1.GetComponent<NetworkObject>().Spawn();
            go1.GetComponent <NetworkObject>().ChangeOwnership(1);

        }
        if (clientId == 2)
        {
            Debug.Log("Client connected. You can now call ServerRpc methods.");
            // Call your ServerRpc method here or trigger an event to notify other scripts
            player2 = Instantiate(redplayer, new Vector2(2f, 0.65f), Quaternion.identity);
            player2.GetComponent<NetworkObject>().Spawn();
            player2.GetComponent<PlayerLanController>().enabled = false;
            player2.GetComponent<NetworkObject>().ChangeOwnership(2);
            go2 = Instantiate(kiemxoatprefab);
            go2.GetComponent<NetworkObject>().Spawn();
            go2.GetComponent<NetworkObject>().ChangeOwnership(2);

        }
        if (clientId == 3)
        {
            Debug.Log("Client connected. You can now call ServerRpc methods.");
            // Call your ServerRpc method here or trigger an event to notify other scripts
            player3 = Instantiate(yellowplayer, new Vector2(6f, 0.65f), Quaternion.identity);
            player3.GetComponent<NetworkObject>().Spawn();
            player3.GetComponent<PlayerLanController>().enabled = false;
            player3.GetComponent<NetworkObject>().ChangeOwnership(3);
            go3 = Instantiate(kiemxoatprefab);
            go3.GetComponent<NetworkObject>().Spawn();
            go3.GetComponent<NetworkObject>().ChangeOwnership(3);

        }
    }
    //[ServerRpc]
    //private void SanSangServerRpc()
    //{
    //    soluongss++;
    //    Debug.Log("So luong ss " + soluongss);
    //}
    //[ServerRpc]
    //private void HuySanSangServerRpc()
    //{
    //    soluongss--;
    //    Debug.Log("So luong ss " + soluongss);
    //}

}
