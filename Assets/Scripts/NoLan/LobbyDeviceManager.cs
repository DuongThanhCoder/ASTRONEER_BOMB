using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyDeviceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static LobbyDeviceManager lobby { get; private set; }
    public GameObject blue;
    public GameObject green;
    public GameObject red;
    public GameObject yellow;
    public UnityEngine.UI.Button bot1;
    public UnityEngine.UI.Button player1;
    public UnityEngine.UI.Button bot2;
    public UnityEngine.UI.Button player2;
    public UnityEngine.UI.Button bot3;
    public UnityEngine.UI.Button player3;
    public UnityEngine.UI.Button bot4;
    public UnityEngine.UI.Button player4;
    public UnityEngine.UI.Button start;
    public UnityEngine.UI.Button quit;
    public UnityEngine.UI.Button remove1;
    public UnityEngine.UI.Button remove2;
    public UnityEngine.UI.Button remove3;
    public UnityEngine.UI.Button remove4;
    public InputField name1;
    public InputField name2;
    public InputField name3;
    public InputField name4;
    public int typeplayer1 = 0;
    public int typeplayer2 = 0;
    public int typeplayer3 = 0;
    public int typeplayer4 = 0;
    public string nickname1, nickname2, nickname3, nickname4;
    GameObject p1,p2,p3,p4;
    GameObject b1, b2, b3, b4;
    public int mapnumber = 4;
    void Start()
    {
        bot1.onClick.AddListener(() =>
        {
            b1 = Instantiate(blue, new Vector2(-6f, 2.2f), Quaternion.identity);
            typeplayer1 = 1;
        });
        player1.onClick.AddListener(() =>
        {
            p1 = Instantiate(blue, new Vector2(-6f, 2.2f), Quaternion.identity);
            typeplayer1 = 2;
        });
        bot2.onClick.AddListener(() =>
        {
            b2 = Instantiate(green, new Vector2(-2f, 2.2f), Quaternion.identity);
            typeplayer2 = 1;
        });
        player2.onClick.AddListener(() =>
        {
            p2 = Instantiate(green, new Vector2(-2f, 2.2f), Quaternion.identity);
            typeplayer2 = 2;
        });
        bot3.onClick.AddListener(() =>
        {
            b3 = Instantiate(red, new Vector2(2f, 2.2f), Quaternion.identity);
            typeplayer3 = 1;
        });
        player3.onClick.AddListener(() =>
        {
            p3 = Instantiate(red, new Vector2(2f, 2.2f), Quaternion.identity);
            typeplayer3 = 2;
        });
        bot4.onClick.AddListener(() =>
        {
            b4 = Instantiate(yellow, new Vector2(6f, 2.2f), Quaternion.identity);
            typeplayer4 = 1;
        });
        player4.onClick.AddListener(() =>
        {
            p4 = Instantiate(yellow, new Vector2(6f, 2.2f), Quaternion.identity);
            typeplayer4 = 2;
        });
        remove1.onClick.AddListener(() =>
        {
            Destroy(p1);
            typeplayer1 = 0;
        });
        remove2.onClick.AddListener(() =>
        {
            Destroy(p2);
            typeplayer2 = 0;
        });
        remove3.onClick.AddListener(() =>
        {
            Destroy(p3);
            typeplayer3 = 0;
        });
        remove4.onClick.AddListener(() =>
        {
            Destroy(p4);
            typeplayer4 = 0;
        });
        lobby = this;
        start.onClick.AddListener(() =>
        {
            nickname1 = name1.text;
            nickname2 = name2.text;
            nickname3 = name3.text;
            nickname4 = name4.text;
            Debug.Log(name1.text);
            SceneManager.LoadSceneAsync(mapnumber);
           
        });
        quit.onClick.AddListener(() =>
        {
            SceneManager.LoadSceneAsync(0);
        });
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void setMap1()
    {
        mapnumber = 4;
    }
    public void setMap2()
    {
        mapnumber = 5;
    }
    public void setMap3()
    {
        mapnumber = 6;
    }
}
