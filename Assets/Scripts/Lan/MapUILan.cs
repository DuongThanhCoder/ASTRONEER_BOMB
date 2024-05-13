using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapUILan : MonoBehaviour
{
    // Start is called before the first frame update
    public Button menu;
    public Button playagain;
    void Start()
    {
        menu.onClick.AddListener(() =>
        {
            NetworkManager networkManager = FindObjectOfType<NetworkManager>();
            if (networkManager != null)
            {
                Destroy(networkManager.gameObject);
            }
            SceneManager.LoadScene(0);
        });
        playagain.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
