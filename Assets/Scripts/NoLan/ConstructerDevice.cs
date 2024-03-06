using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructerDevice : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject blue;
    public GameObject green;
    public GameObject red;
    public GameObject yellow;
    void Start()
    {
        //if (PlayerPrefs.HasKey("typeplayer1"))
        //{
        //    if(PlayerPrefs.GetInt("typeplayer1") == 2)
        //    {
        //        Instantiate(blue, new Vector2(-9.5f, 5.1f), Quaternion.identity);
        //    }
        //    if (PlayerPrefs.GetInt("typeplayer2") == 2)
        //    {
        //        Instantiate(green, new Vector2(14.5f, -8.9f), Quaternion.identity);
        //    }
        //    if (PlayerPrefs.GetInt("typeplayer3") == 2)
        //    {
        //        Instantiate(red, new Vector2(-9.5f, -8.9f), Quaternion.identity);
        //    }
        //    if (PlayerPrefs.GetInt("typeplayer4") == 2)
        //    {
        //        Instantiate(yellow, new Vector2(14.5f, 5.1f), Quaternion.identity);
        //    }
        //}
            if (LobbyDeviceManager.lobby.typeplayer1 == 2)
            {
                Instantiate(blue, new Vector2(-9.5f, 5.1f), Quaternion.identity);
            }
            if (LobbyDeviceManager.lobby.typeplayer2 == 2)
            {
                Instantiate(green, new Vector2(14.5f, -8.9f), Quaternion.identity);
            }
            if (LobbyDeviceManager.lobby.typeplayer3 == 2)
            {
                Instantiate(red, new Vector2(-9.5f, -8.9f), Quaternion.identity);
            }
            if (LobbyDeviceManager.lobby.typeplayer4 == 2)
            {
                Instantiate(yellow, new Vector2(14.5f, 5.1f), Quaternion.identity);
            }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
