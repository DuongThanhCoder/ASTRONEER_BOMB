using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private UnityEngine.UI.Button playonthisdevice;
    [SerializeField] private UnityEngine.UI.Button createHostbtn;
    [SerializeField] private UnityEngine.UI.Button joinHostbtn;
    [SerializeField] private UnityEngine.UI.Button exitbtn;
    [SerializeField] private UnityEngine.UI.InputField ipconnect;
    [SerializeField] private Text report;
    private void Start()
    {
        playonthisdevice.onClick.AddListener(() =>
        {
            playondevice();
        });
        createHostbtn.onClick.AddListener(() =>
        {
            CreateHost();
        });
        joinHostbtn.onClick.AddListener(() =>
        {
            JoinHost();
        });
        exitbtn.onClick.AddListener(() =>
        {
            ExitGame();
        });
    }
    public void ExitGame()
    {
        Debug.Log("ExitGame");
        Application.Quit();
    }
    public void playondevice()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void CreateHost()
    {
        Debug.Log("CreateHost");
        NetworkManager.Singleton.StartHost();
        SceneManager.LoadSceneAsync(1);
    }
    public void JoinHost()
    {
        string ipAddress = ipconnect.text;
        if (HostExists(ipAddress))
        {
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(
            ipAddress,  // The IP address is a string
            (ushort)2345 // The port number is an unsigned short
        );
            NetworkManager.Singleton.StartClient();
            SceneManager.LoadSceneAsync(1);
        }
        else
        {
            Debug.Log("Host không tồn tại");
        }
    }
    public void resetreport()
    {
        report.enabled = false;
    }
    private bool HostExists(string ipAddress)
    {
        try
        {
            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            PingReply reply = ping.Send(ipAddress);

            if (reply != null && reply.Status == IPStatus.Success)
            {
                return true; // Host tồn tại
            }
            else
            {
                report.enabled = true;
                return false; // Host không tồn tại
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error checking host existence: {e.Message}");
            return false;
        }
    }
}
