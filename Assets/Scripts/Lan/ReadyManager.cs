using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadyManager : NetworkBehaviour
{
    // Start is called before the first frame update
    
    int numConnections;
    private UnityEngine.UI.Button buttonss;
    private UnityEngine.UI.Button buttonhss;
    public int soluongss = 0;

    private void Awake()
    {
        GameObject canva = GameObject.Find("Canvas");
        buttonss = canva.transform.Find("Ready").GetComponent<UnityEngine.UI.Button>();
        buttonhss = canva.transform.Find("Cancel").GetComponent<UnityEngine.UI.Button>();

        buttonss.onClick.AddListener(() =>
        {
            if (NetworkManager.LocalClientId == 0)
            {
                Debug.Log("HostSS");
                SanSangClientRpc();
            }
            else
            {
                //xinsoluongServerRpc();
                Debug.Log("ClientSS");
                SanSangServerRpc();
            }
        });
        buttonhss.onClick.AddListener(() =>
        {
            if (NetworkManager.LocalClientId == 0)
            {
                Debug.Log("HostSS");
                HuySanSangClientRpc();
            }
            else
            {
                Debug.Log("ClientSS");
                HuySanSangServerRpc();
            }
        });
    }
    void Start()
    {
        //if(IsHost)
        //{
        //    Dongbo(soluongss);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.IsHost && IsOwner)
        {
            Debug.Log("so luong ss tren host " + soluongss + " id " + OwnerClientId + " Client id " + NetworkManager.LocalClientId);
        }
        //if (NetworkManager.IsHost && !IsOwner)
        //{
        //    Debug.Log("so luong ss tren host " + soluongss + " id " + OwnerClientId + " Client id " + NetworkManager.LocalClientId);
        //}
        if (NetworkManager.IsHost)
        {
            numConnections = NetworkManager.Singleton.ConnectedClientsList.Count;
            //songuoichoi = numConnections;
            Debug.Log("Number of connected clients: " + numConnections);

        }
        if (OwnerClientId == 0 && NetworkManager.IsHost)
        {
            if(soluongss == NetworkManager.Singleton.ConnectedClientsList.Count && NetworkManager.Singleton.ConnectedClientsList.Count>=1)
            {
                chuyensceneClientRpc();
                //SceneManager.LoadScene(2);
                
            }
        }
    }
    [ClientRpc]
    private void chuyensceneClientRpc()
    {
        SceneManager.LoadScene(2);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SanSangServerRpc()
    {
        //if(OwnerClientId == 0)
        //{
        //    soluongss++;
        //}
        //congsoluong();
        soluongss++;
        Debug.Log("Client Request " + OwnerClientId);
    }
    [ServerRpc(RequireOwnership = false)]
    private void HuySanSangServerRpc()
    {
        //if (OwnerClientId == 0)
        //{
        //    soluongss--;
        //}
        //trusoluong();
        soluongss--;
        Debug.Log("Client Request " + OwnerClientId);
    }
    [ClientRpc ]
    private void SanSangClientRpc()
    {
        //if (OwnerClientId >0)
        //{
        //    soluongss++;
        //}
        //congsoluong();
        soluongss++;
        Debug.Log("Client Request " + OwnerClientId);
    }
    [ClientRpc]
    private void HuySanSangClientRpc()
    {

        soluongss--;
        Debug.Log("Client Request " + OwnerClientId);
    }
}
