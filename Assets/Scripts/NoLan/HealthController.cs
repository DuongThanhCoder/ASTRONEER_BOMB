using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineFreeLook;

public class HealthController : MonoBehaviour
{
    // Start is called before the first frame update
    public int mauhientai;
    public int maumax = 5;
    float timebtconlai;
    float timebattu = 1.5f;
    bool trangthaibt;
    int nhapnhay = 0;
    PlayerController player;
    public HealthBar healthBar;
    private void Awake()
    {
        mauhientai = 3;
    }
    void Start()
    {
        player = GetComponent<PlayerController>();
        healthBar.setValue(mauhientai / (float)maumax);
        trangthaibt = false;
        timebtconlai = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (trangthaibt == true)
        {
            if (gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                if (nhapnhay == 20)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    nhapnhay = 0;
                }
            }
            else
            {
                if (nhapnhay == 20)
                {
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    nhapnhay = 0;
                }
            }
            timebtconlai -= Time.deltaTime;
            if (timebtconlai <= 0)
            {
                trangthaibt = false;
                timebtconlai = 0;
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                nhapnhay = 0;
            }
            nhapnhay += 1;
        }
    }
    public void thaydoimau(int luongmau)
    {
        if (player.trangthaikhien == true && luongmau < 0)
        {
            player.tatkhien();
            trangthaibt = true;
            timebtconlai = timebattu;
            return;
        }
        if (luongmau < 0)
        {
            if (trangthaibt == true)
            {
                return;
            }
            else
            {
                trangthaibt = true;
                timebtconlai = timebattu;
            }
        }
        mauhientai = Mathf.Clamp(mauhientai + luongmau, 0, maumax);
        healthBar.setValue(mauhientai / (float)maumax);
        if (mauhientai == 0)
        {
            player.animator_body.SetBool("die", true);
            player.gunfire.SetActive(false);
            player.weapon.SetActive(false);
            player.thanhmau.SetActive(false);
            player.overlay.SetActive(false);
            //Destroy(gameObject, 2f);
            StartCoroutine(player.choxoa());
            Destroy(gameObject, 3f);
            Destroy(player.robot);
        }
    }
}
