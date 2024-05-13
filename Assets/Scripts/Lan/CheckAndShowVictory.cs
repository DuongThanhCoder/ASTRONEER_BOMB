using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CheckAndShowVictory : NetworkBehaviour
{
    // Start is called before the first frame update
    public Save save;
    public int sllive = 0;
    public Image blueVictory;
    public Image greenVictory;
    public Image redVictory;
    public Image yellowVictory;
    public Image panel;
    public Button menu;
    public Button playagain;

    private void Awake()
    {
        GameObject canva = GameObject.Find("Canvas");
        blueVictory = canva.transform.Find("BlueWin").GetComponent<Image>();
        greenVictory = canva.transform.Find("GreenWin").GetComponent<Image>();
        redVictory = canva.transform.Find("RedWin").GetComponent<Image>();
        yellowVictory = canva.transform.Find("YellowWin").GetComponent<Image>();
        panel = canva.transform.Find("Panel").GetComponent<Image>();
        playagain = canva.transform.Find("PlayAgain").GetComponent<Button>();
        menu = canva.transform.Find("Menu").GetComponent<Button>();
    }
    void Start()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
        for (int i = 0; i < 4; ++i)
        {
            if (save.teamWithId[i] < 0)
            {
                ++sllive;
            }
        }
    }

    private void Singleton_OnClientDisconnectCallback(ulong obj)
    {
        for (int i = 0; i < 4; ++i)
        {
            if (NetworkManager.IsHost && save.teamWithId[i] == (int)obj)
            {
                --sllive;
                CheckandShow();
            }
        }
    }
    public void CheckandShow()
    {
        if (NetworkManager.IsHost && sllive == 1)
        {
            if (GameObject.Find("BlueLanPlayer(Clone)") != null)
            {
                callWinImageClientRpc(0);
                return;
            }
            if (GameObject.Find("GreenLanPlayer(Clone)") != null)
            {
                callWinImageClientRpc(1);
                return;
            }
            if (GameObject.Find("RedLanPlayer(Clone)") != null)
            {
                callWinImageClientRpc(2);
                return;
            }
            if (GameObject.Find("YellowLanPlayer(Clone)") != null)
            {
                callWinImageClientRpc(3);
                return;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    [ServerRpc(RequireOwnership = false)]
    public void reqestgiamslServerRpc()
    {
        --sllive;
        CheckandShow();
    }
    [ClientRpc]
    public void callWinImageClientRpc(int n)
    {
        if (n == 0)
        {
            blueVictory.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            playagain.gameObject.SetActive(true);
            menu.gameObject.SetActive(true);
            return;
        }
        if (n == 1)
        {
            greenVictory.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            playagain.gameObject.SetActive(true);
            menu.gameObject.SetActive(true);
            return;
        }
        if (n == 2)
        {
            redVictory.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            playagain.gameObject.SetActive(true);
            menu.gameObject.SetActive(true);
            return;
        }
        if (n == 3)
        {
            yellowVictory.gameObject.SetActive(true);
            panel.gameObject.SetActive(true);
            playagain.gameObject.SetActive(true);
            menu.gameObject.SetActive(true);
            return;
        }
    }
}
