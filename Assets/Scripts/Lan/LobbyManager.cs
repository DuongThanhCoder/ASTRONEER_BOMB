using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Linq;
using TMPro;
using Unity.Netcode;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LobbyManager : NetworkBehaviour
{

    // Start is called before the first frame update
    public GameObject blueplayer;
    public GameObject greenplayer;
    public GameObject redplayer;
    public GameObject yellowplayer;
    public GameObject kiemxoatprefab;
    public Save save;
    //int numConnections;
    private GameObject player0;
    private GameObject player1;
    private GameObject player2;
    private GameObject player3;
    private UnityEngine.UI.Button chooseblue;
    private UnityEngine.UI.Button choosegreen;
    private UnityEngine.UI.Button choosered;
    private UnityEngine.UI.Button chooseyellow;
    private TextMeshProUGUI blueName;
    private TextMeshProUGUI greenName;
    private TextMeshProUGUI redName;
    private TextMeshProUGUI yellowName;
    private UnityEngine.UI.Button buttonss;
    private UnityEngine.UI.Button buttonhss;
    private UnityEngine.UI.Button quit;
    private InputField inputName;
    private List<int> teamWithId;
    private int teamIndex = -1;
    private string teamName = "";
    private GameObject readyManager;
    private void Awake()
    {
        save.teamWithId[0] = -1;
        save.teamWithId[1] = -1;
        save.teamWithId[2] = -1;
        save.teamWithId[3] = -1;
        GameObject canva = GameObject.Find("Canvas");
        chooseblue = canva.transform.Find("ChooseBlue").GetComponent<UnityEngine.UI.Button>();
        choosegreen = canva.transform.Find("ChooseGreen").GetComponent<UnityEngine.UI.Button>();
        choosered = canva.transform.Find("ChooseRed").GetComponent<UnityEngine.UI.Button>();
        chooseyellow = canva.transform.Find("ChooseYellow").GetComponent<UnityEngine.UI.Button>();
        teamWithId = new List<int> { -1, -1, -1, -1 };
        buttonss = canva.transform.Find("Ready").GetComponent<UnityEngine.UI.Button>();
        buttonhss = canva.transform.Find("Cancel").GetComponent<UnityEngine.UI.Button>();
        quit = canva.transform.Find("Quit").GetComponent<UnityEngine.UI.Button>();
        inputName = canva.transform.Find("inputName").GetComponent<InputField>();
        blueName = canva.transform.Find("blueName").GetComponent<TextMeshProUGUI>();
        greenName = canva.transform.Find("greenName").GetComponent<TextMeshProUGUI>();
        redName = canva.transform.Find("redName").GetComponent<TextMeshProUGUI>();
        yellowName = canva.transform.Find("yellowName").GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {

        if (NetworkManager.IsHost)
        {
            Debug.Log("Day la host");
            readyManager = Instantiate(kiemxoatprefab);
            readyManager.GetComponent<NetworkObject>().Spawn();
            //gameObject.GetComponent<NetworkObject>().Spawn();
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
            //a.GetComponent<NetworkObject>().Spawn();
        }
        quit.onClick.AddListener(() =>
        {
            
            NetworkManager networkManager = FindObjectOfType<NetworkManager>();
            if (networkManager != null)
            {
                Destroy(networkManager.gameObject);
            }
            SceneManager.LoadScene(0);
        });

        chooseblue.onClick.AddListener(() =>
        {
            checkAndSetDefautState();
            if (NetworkManager.IsHost)
            {
                //int index = teamWithId.FindIndex(x => x == 0);
                if (teamIndex >= 0)
                {
                    findAndDestroy(teamIndex);
                    teamWithId[teamIndex] = -1;
                    SyncDataClientRpc(teamIndex, -1,"");
                }
                spawner(0);
                teamWithId[0] = 0;
                teamIndex = 0;
                SyncDataClientRpc(0, 0, teamName);
                chooseblue.gameObject.SetActive(false);
            }
            else
            {
                //int index = teamWithId.FindIndex(x => x == (int)NetworkManager.LocalClientId);
                if (teamIndex >= 0)
                {
                    findAndDestroyServerRpc(teamIndex);
                    SyncDataServerRpc(teamIndex, -1, "");
                    
                }
                SyncDataServerRpc(0, (int)NetworkManager.LocalClientId,teamName);
                teamIndex = 0;
                //chooseblue.gameObject.SetActive(false);

            }
        });
        choosegreen.onClick.AddListener(() =>
        {
            checkAndSetDefautState();
            if (NetworkManager.IsHost)
            {
                //int index = teamWithId.FindIndex(x => x == 0);
                if (teamIndex >= 0)
                {
                    findAndDestroy(teamIndex);
                    teamWithId[teamIndex] = -1;
                    SyncDataClientRpc(teamIndex, -1, "");
                }
                spawner(1);
                teamWithId[1] = 0;
                teamIndex = 1;
                SyncDataClientRpc(1, 0, teamName);
                choosegreen.gameObject.SetActive(false);
            }
            else
            {
                //int index = teamWithId.FindIndex(x => x == (int)NetworkManager.LocalClientId);
                if (teamIndex >= 0)
                {
                    findAndDestroyServerRpc(teamIndex);
                    SyncDataServerRpc(teamIndex, -1, "");
                }
                SyncDataServerRpc(1, (int)NetworkManager.LocalClientId, teamName);
                teamIndex = 1;
                //chooseblue.gameObject.SetActive(false);

            }

        });
        choosered.onClick.AddListener(() =>
        {
            checkAndSetDefautState();
            if (NetworkManager.IsHost)
            {
                //int index = teamWithId.FindIndex(x => x == 0);
                if (teamIndex >= 0)
                {
                    findAndDestroy(teamIndex);
                    teamWithId[teamIndex] = -1;
                    SyncDataClientRpc(teamIndex, -1, "");
                }
                spawner(2);
                teamWithId[2] = 0;
                teamIndex = 2;
                SyncDataClientRpc(2, 0, teamName);
                choosered.gameObject.SetActive(false);
            }
            else
            {
                //int index = teamWithId.FindIndex(x => x == (int)NetworkManager.LocalClientId);
                if (teamIndex >= 0)
                {
                    findAndDestroyServerRpc(teamIndex);
                    SyncDataServerRpc(teamIndex, -1, "");

                }
                SyncDataServerRpc(2, (int)NetworkManager.LocalClientId, teamName);
                teamIndex = 2;
            }

        });
        chooseyellow.onClick.AddListener(() =>
        {
            checkAndSetDefautState();
            if (NetworkManager.IsHost)
            {
                //int index = teamWithId.FindIndex(x => x == 0);
                if (teamIndex >= 0)
                {
                    findAndDestroy(teamIndex);
                    teamWithId[teamIndex] = -1;
                    SyncDataClientRpc(teamIndex, -1, "");
                }
                spawner(3);
                teamWithId[3] = 0;
                teamIndex = 3;
                SyncDataClientRpc(3, 0, teamName);
                chooseyellow.gameObject.SetActive(false);
            }
            else
            {
                //int index = teamWithId.FindIndex(x => x == (int)NetworkManager.LocalClientId);
                if (teamIndex >= 0)
                {
                    findAndDestroyServerRpc(teamIndex);
                    SyncDataServerRpc(teamIndex, -1, "");

                }
                SyncDataServerRpc(3, (int)NetworkManager.LocalClientId, teamName);
                teamIndex = 3;
            }

        });
        inputName.GetComponent<InputField>().onEndEdit.AddListener((string name) =>
        {
            //Debug.Log("Name: "+name);
            if (NetworkManager.IsHost)
            {
                save.teamWithName[teamIndex] = name;
                teamName = name;
                SyncDataClientRpc(teamIndex, 0, name);
            }
            else
            {
                save.teamWithName[teamIndex] = name;
                teamName = name;
                SyncDataServerRpc(teamIndex, (int)NetworkManager.LocalClientId, name);
            }
            
            
        });

    }
    public void checkAndSetDefautState()
    {
        inputName.gameObject.SetActive(true);
        if(IsHost)
        {
            if (buttonhss.IsActive())
            {
                readyManager.GetComponent<ReadyManager>().soluongss -= 1;
            }
        }
        else
        {
            if (buttonhss.IsActive())
            {
                GameObject.Find("ReadyManager(Clone)").GetComponent<ReadyManager>().HuySanSangServerRpc(); 
            }

        }
        buttonss.gameObject.SetActive(true);
        buttonhss.gameObject.SetActive(false);
    }
    private void OnClientDisconnected(ulong clientId)
    {
        int index = teamWithId.FindIndex(x => x == (int)clientId);
        if(index >= 0)
        {
            findAndDestroy(index);
            teamWithId[index] = -1;
            SyncDataClientRpc(index, -1, "");
        }
    }

    public void findAndDestroy(int index)
    {
        if(IsHost)
        {
            Debug.Log("Destroy");
        }
        if(index == 0)
        {
            //Destroy(gameObject.transform.Find("BlueFigure(Clone)"));
            Destroy(player0);
            chooseblue.gameObject.SetActive(true);
            Debug.Log("Destroy Blue");
        }
        if (index == 1)
        {
            //Destroy(gameObject.transform.Find("GreenFigure(Clone)"));
            Destroy(player1);
            choosegreen.gameObject.SetActive(true);
            Debug.Log("Destroy Green");
        }
        if (index == 2)
        {
            //Destroy(gameObject.transform.Find("RedFigure(Clone)"));
            Destroy(player2);
            choosered.gameObject.SetActive(true);
            Debug.Log("Destroy Red");
        }
        if (index == 3)
        {
            //Destroy(gameObject.transform.Find("YellowFigure(Clone)"));
            Destroy(player3);
            chooseyellow.gameObject.SetActive(true);
            Debug.Log("Destroy Yellow");
        }
    }
    public void spawner(int index)
    {
        if(index == 0)
        {
            player0 = Instantiate(blueplayer, new Vector2(-6f, 0.65f), Quaternion.identity);
            player0.GetComponent<NetworkObject>().Spawn();
        }
        if (index == 1)
        {
            player1 = Instantiate(greenplayer, new Vector2(-2f, 0.65f), Quaternion.identity);
            player1.GetComponent<NetworkObject>().Spawn();
        }
        if (index == 2)
        {
            player2 = Instantiate(redplayer, new Vector2(2f, 0.65f), Quaternion.identity);
            player2.GetComponent<NetworkObject>().Spawn();
        }
        if (index == 3)
        {
            player3 = Instantiate(yellowplayer, new Vector2(6f, 0.65f), Quaternion.identity);
            player3.GetComponent<NetworkObject>().Spawn();
        }
    }
    public void checkAndUpdateButton()
    {
        if (teamWithId[0] >= 0)
        {
            chooseblue.gameObject.SetActive(false);
            if(teamWithId[0]!= (int)NetworkManager.LocalClientId)
            {
                blueName.gameObject.SetActive(true);
                blueName.text = save.teamWithName[0];
            }
        }
        else
        {
            chooseblue.gameObject.SetActive(true);
            blueName.gameObject.SetActive(false);
        }

        if (teamWithId[1] >= 0)
        {
            choosegreen.gameObject.SetActive(false);
            if (teamWithId[1] != (int)NetworkManager.LocalClientId)
            {
                greenName.gameObject.SetActive(true);
                greenName.text = save.teamWithName[1];
            }
        }
        else
        {
            choosegreen.gameObject.SetActive(true);
            greenName.gameObject.SetActive(false);
        }

        if (teamWithId[2] >= 0)
        {
            choosered.gameObject.SetActive(false);
            if (teamWithId[2] != (int)NetworkManager.LocalClientId)
            {
                redName.gameObject.SetActive(true);
                redName.text = save.teamWithName[2];
            }
        }
        else
        {
            choosered.gameObject.SetActive(true);
            redName.gameObject.SetActive(false);
        }

        if (teamWithId[3] >= 0)
        {
            chooseyellow.gameObject.SetActive(false);
            if (teamWithId[3] != (int)NetworkManager.LocalClientId)
            {
                yellowName.gameObject.SetActive(true);
                yellowName.text = save.teamWithName[3];
            }
        }
        else
        {
            chooseyellow.gameObject.SetActive(true);
            yellowName.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (NetworkManager.IsHost)
        {
            //numConnections = NetworkManager.Singleton.ConnectedClientsList.Count;
            ////songuoichoi = numConnections;
            //Debug.Log("Number of connected clients: " + numConnections);
            Debug.Log(teamWithId[0] + " " + teamWithId[1] + " " + teamWithId[2] + " " + teamWithId[3]);
            //Debug.Log(x + " owner: " + OwnerClientId);

        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SceneManager.LoadScene(0);
        //}
    }
    private void OnClientConnected(ulong clientId)
    {
        if (IsHost)
        {
            SyncListClientRpc(teamWithId[0], teamWithId[1], teamWithId[2], teamWithId[3], save.teamWithName[0], save.teamWithName[1], save.teamWithName[2], save.teamWithName[3]);
        }
    }
    public void savegiatri()
    {
        save.teamWithId[0] = this.teamWithId[0];
        save.teamWithId[1] = this.teamWithId[1];
        save.teamWithId[2] = this.teamWithId[2];
        save.teamWithId[3] = this.teamWithId[3];

    }
    [ServerRpc(RequireOwnership = false)]
    private void SyncDataServerRpc(int index,int value,string name)
    {
        bool spawn = true;
        //++x;
        if (teamWithId[index] == value)
        {
            spawn = false;
        }
        teamWithId[index] = value;
        save.teamWithName[index] = name;
        savegiatri();
        if (value >= 0&&spawn)
        {
            spawner(index);
        }
        SyncDataClientRpc(index, value, name);
        Debug.Log("Server Request " + OwnerClientId);
    }
    [ClientRpc]
    private void SyncDataClientRpc(int index, int value, string name)
    {
        teamWithId[index] = value;
        save.teamWithName[index] = name;
        savegiatri();
        checkAndUpdateButton();
    }
    [ClientRpc]
    private void SyncListClientRpc(int a,int b,int c,int d,string name1,string name2, string name3,string name4)
    {
        teamWithId[0] = a;
        teamWithId[1] = b;
        teamWithId[2] = c;
        teamWithId[3] = d;
        save.teamWithName[0] = name1;
        save.teamWithName[1] = name2;
        save.teamWithName[2] = name3;
        save.teamWithName[3] = name4;
        checkAndUpdateButton();
    }
    [ServerRpc(RequireOwnership = false)]
    private void findAndDestroyServerRpc(int index)
    {
        findAndDestroy(index);
    }
}
