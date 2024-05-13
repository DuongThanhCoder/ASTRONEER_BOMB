using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ReadyManager : NetworkBehaviour
{
    // Start is called before the first frame update
    
    int numConnections;
    private UnityEngine.UI.Button buttonss;
    private UnityEngine.UI.Button buttonhss;
    public Save save;
    public int soluongss = 0;
    private NetworkVariable<int> slss = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
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
                //SanSangClientRpc();
                ++soluongss;
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
                //HuySanSangClientRpc();
                --soluongss;
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
        //if (IsHost)
        //{
        //    //Dongbo(soluongss);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("So luong ss netvar " + slss.Value);
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
            if (soluongss == kiemtrasoluong() && kiemtrasoluong() >= 2)
            {
                chuyensceneClientRpc();
                //SceneManager.LoadScene(2);

            }
        }
    }
    public int kiemtrasoluong()
    {
        return save.teamWithId.Count(item => item > -1);
    }
    [ClientRpc]
    private void chuyensceneClientRpc()
    {
        SceneManager.LoadScene(2);
    }

    [ServerRpc(RequireOwnership = false)]
    public void SanSangServerRpc()
    {
        //if(OwnerClientId == 0)
        //{
        //    soluongss++;
        //}
        //congsoluong();
        soluongss++;
        Debug.Log("Server Request " + OwnerClientId);
    }
    [ServerRpc(RequireOwnership = false)]
    public void HuySanSangServerRpc()
    {
        //if (OwnerClientId == 0)
        //{
        //    soluongss--;
        //}
        //trusoluong();
        soluongss--;
        Debug.Log("Server Request " + OwnerClientId);
    }
}
