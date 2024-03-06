using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MapUI : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEngine.UI.Button btnpause;
    public UnityEngine.UI.Button btncontinue;
    public UnityEngine.UI.Button btnmenu;
    public GameObject victoryText;
    public GameObject panel;
    public UnityEngine.UI.Button PlayAgain;
    public GameObject map;
    public UnityEngine.UI.Image bluewin;
    public UnityEngine.UI.Image greenwin;
    public UnityEngine.UI.Image redwin;
    public UnityEngine.UI.Image yellowwin;
    public AudioSource audioSource;
    public AudioClip chienthang;


    public static bool pause;
    string playerwin;
    int dem = 0;
    void Start()
    {
        pause = false;
        btnpause.onClick.AddListener(() =>
        {
            pause = true;
        });
        btncontinue.onClick.AddListener(() =>
        {
            pause = false;
        });
        btnmenu.onClick.AddListener(() =>
        {
            Debug.Log("Chuyen sang scene 0");
            SceneManager.LoadSceneAsync(0);

        });
        PlayAgain.onClick.AddListener(() =>
        {
            Debug.Log("Chuyen sang scene 4");
            SceneManager.LoadSceneAsync(4);

            //Time.timeScale = 1.0f;
        });
    }

    // Update is called once per frame
    void Update()
    {
        kiemtrasoluong();
        if (dem == 1)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(chienthang);
            playerwin = kiemtratencuoi();
            panel.gameObject.SetActive(true);
            //victoryText.GetComponent<TextMeshProUGUI>().text = playerwin + "Victorty";
            //victoryText.gameObject.SetActive(true);
            if (playerwin[0]=='B'){
                bluewin.gameObject.SetActive(true);
            }
            if (playerwin[0]=='G'){
                greenwin.gameObject.SetActive(true);
            }
            if (playerwin[0]=='R'){
                redwin.gameObject.SetActive(true);
            }
            if (playerwin[0]=='Y'){
                yellowwin.gameObject.SetActive(true);
            }
            btnmenu.gameObject.SetActive(true);
            PlayAgain.gameObject.SetActive(true);
            Destroy(map);
            Destroy(GameObject.Find(playerwin+ "(Clone)"));
        }
        
    }
    public void kiemtrasoluong()
    {
        dem = 0;
        if(GameObject.Find("BluePlayer(Clone)") != null)
        {
            ++dem;
        }
        if (GameObject.Find("GreenPlayer(Clone)") != null)
        {
            ++dem;
        }
        if (GameObject.Find("RedPlayer(Clone)") != null)
        {
            ++dem;
        }
        if (GameObject.Find("YellowPlayer(Clone)") != null)
        {
            ++dem;
        }
        Debug.Log("SoLuongConLai: "+ dem);
    }
    public string kiemtratencuoi()
    {
        if (GameObject.Find("BluePlayer(Clone)") != null)
        {
            return "BluePlayer";
        }
        if (GameObject.Find("GreenPlayer(Clone)") != null)
        {
            return "GreenPlayer";
        }
        if (GameObject.Find("RedPlayer(Clone)") != null)
        {
            return "RedPlayer";
        }
        if (GameObject.Find("YellowPlayer(Clone)") != null)
        {
            return "YellowPlayer";
        }
        return "";
    }
}
