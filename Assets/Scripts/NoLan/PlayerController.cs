using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool trangthaikhien;
    public int vukhi;
    //prefab
    public GameObject robotprefab;
    //gameObject
    Rigidbody2D rigidbody2d;
    public Animator animator_body;
    public Animator animator_overlay;
    public Animator animator_vukhi;
    public Animator animator_gunfire;
    public GameObject thanhmau;
    public GameObject khien;
    public GameObject weapon;
    public GameObject gunfire;
    public GameObject overlay;
    public AudioClip tiengsung;
    public GameObject nickname;
    public GameObject robot;
    public HammerAttackController hammerAttack;
    public GunAttackController gunAttack;
    public HealthController health;
    public PlayerMovementController movement;
    //other
    public enum role
    {
        p1, p2, p3, p4, bot,
    }
    public role roleplayer;
    public void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthController>();
        trangthaikhien = false;
        animator_body = GetComponent<Animator>();
        setFirtLook();
        vukhi = 0;
        hammerAttack = gameObject.GetComponent<HammerAttackController>();
        gunAttack = gameObject.GetComponent<GunAttackController>();
        movement = gameObject.GetComponent<PlayerMovementController>();
        setPlayerName();
    }

    // Update is called once per frame
    void Update()
    {
        if (MapUI.pause == true) { return; }
        if (health.mauhientai == 0)
        {
            movement.speed = 0;
            return;
        }
        if (movement.vohieuhoamove == false)
        {
            switch (roleplayer)
            {
                case role.p1:
                    activeWeapon(KeyCode.F);
                    break;
                case role.p2:
                    activeWeapon(KeyCode.RightControl);
                    break;
                case role.p3:
                    activeWeapon(KeyCode.K);
                    break;
                case role.p4:
                    activeWeapon(KeyCode.RightShift);
                    break;
            }
        }
        else
        {
            movement.ngang = 0;
            movement.doc = 0;
        }
    }
    void FixedUpdate()
    {
    }
    public void batkhien()
    {
        khien.SetActive(true);
        trangthaikhien = true;
    }
    public void tatkhien()
    {
        khien.SetActive (false);
        trangthaikhien = false;
    }
    public IEnumerator choxoa()
    {
        yield return new WaitForSeconds(2f);
        thanhmau.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    public IEnumerator capnhapvukhi(float time)
    {
        yield return new WaitForSeconds(time);
        animator_body.SetInteger("vukhi", vukhi);
        animator_overlay.SetInteger("vukhi", vukhi);
        animator_vukhi.SetInteger("vukhi", vukhi);
        animator_gunfire.SetInteger("vukhi", vukhi);
    }
    public void capnhapvukhi()
    {
        animator_body.SetInteger("vukhi", vukhi);
        animator_overlay.SetInteger("vukhi", vukhi);
        animator_vukhi.SetInteger("vukhi", vukhi);
        animator_gunfire.SetInteger("vukhi", vukhi);
    }
    public IEnumerator bovohieuhoatime(float time)
    {
        yield return new WaitForSeconds(time);
        movement.vohieuhoamove = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<DanController>()!= null)
        {
            health.thaydoimau(-1);
            Destroy(collision.gameObject);
        }
    }
    void setPlayerName()
    {
        switch (roleplayer)
        {
            case role.p1:
                if (LobbyDeviceManager.lobby.nickname1.Length > 1)
                {
                    nickname.GetComponent<TextMeshProUGUI>().text = LobbyDeviceManager.lobby.nickname1;
                }
                else nickname.GetComponent<TextMeshProUGUI>().text = "P1";
                break;

            case role.p2:
                if (LobbyDeviceManager.lobby.nickname2.Length > 1)
                {
                    nickname.GetComponent<TextMeshProUGUI>().text = LobbyDeviceManager.lobby.nickname2;
                }
                else nickname.GetComponent<TextMeshProUGUI>().text = "P2";
                break;

            case role.p3:
                if (LobbyDeviceManager.lobby.nickname3.Length > 1)
                {
                    nickname.GetComponent<TextMeshProUGUI>().text = LobbyDeviceManager.lobby.nickname3;
                }
                else nickname.GetComponent<TextMeshProUGUI>().text = "P3";
                break;

            case role.p4:
                if (LobbyDeviceManager.lobby.nickname4.Length > 1)
                {
                    nickname.GetComponent<TextMeshProUGUI>().text = LobbyDeviceManager.lobby.nickname4;
                }
                else nickname.GetComponent<TextMeshProUGUI>().text = "P4";
                break;
        }
    }
    public void setHuongNhin(Vector2 huongnhin)
    {
        animator_body.SetFloat("LookX", huongnhin.x);
        animator_body.SetFloat("LookY", huongnhin.y);
        animator_overlay.SetFloat("LookX", huongnhin.x);
        animator_overlay.SetFloat("LookY", huongnhin.y);
        animator_vukhi.SetFloat("LookX", huongnhin.x);
        animator_vukhi.SetFloat("LookY", huongnhin.y);
        animator_gunfire.SetFloat("LookX", huongnhin.x);
        animator_gunfire.SetFloat("LookY", huongnhin.y);
    }
    public void setSpeed(Vector2 vecmove)
    {
        animator_body.SetFloat("Speed", vecmove.magnitude);
        animator_overlay.SetFloat("Speed", vecmove.magnitude);
        animator_vukhi.SetFloat("Speed", vecmove.magnitude);
    }
    void setFirtLook()
    {
        animator_body.SetFloat("LookX", 0f);
        animator_body.SetFloat("LookY", -1f);
        animator_overlay.SetFloat("LookX", 0f);
        animator_overlay.SetFloat("LookY", -1f);
        animator_vukhi.SetFloat("LookX", 0f);
        animator_vukhi.SetFloat("LookY", -1f);
    }
    void changeGunPosition()
    {
        if (movement.doc > 0)
        {
            weapon.transform.localPosition = new Vector2(0, 0.34f);
            gunfire.transform.localPosition = new Vector2(0, 1.5f);
            gunfire.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (movement.doc < 0)
        {
            weapon.transform.localPosition = new Vector2(0, -1f);
            gunfire.transform.localPosition = new Vector2(0, -2.07f);
            gunfire.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
        else if (movement.ngang < 0)
        {
            weapon.transform.localPosition = new Vector2(-0.41f, -0.58f);
            gunfire.transform.localPosition = new Vector2(-1.81f, -0.43f);
            gunfire.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (movement.ngang > 0)
        {
            weapon.transform.localPosition = new Vector2(0.41f, -0.58f);
            gunfire.transform.localPosition = new Vector2(1.81f, -0.43f);
            gunfire.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void activeWeapon(KeyCode key)
    {
        if (Input.GetKeyDown(key) && vukhi == 1)
        {
            robot = Instantiate(robotprefab, rigidbody2d.position + new Vector2(0, -0.5f), Quaternion.identity);
            movement.vohieuhoamove = true;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
        if (vukhi == 2)
        {
            weapon.transform.localPosition = new Vector2(0, -0.3f);
        }
        if (Input.GetKeyDown(key) && vukhi == 2)
        {
            animator_body.SetTrigger("attack");
            animator_overlay.SetTrigger("attack");
            animator_vukhi.SetTrigger("attack");
            hammerAttack.kichhoatbua();
            gameObject.GetComponent<BombController>().setbreaktime();
            vukhi = 0;
            StartCoroutine(capnhapvukhi(1f));
        }
        if (vukhi == 3)
        {
            changeGunPosition();
        }
        if (Input.GetKeyDown(key) && vukhi == 3)
        {
            if (gunAttack.soluongdan > 1)
            {
                gunAttack.ban();
                animator_gunfire.SetTrigger("attack");
                --gunAttack.soluongdan;
            }
            else
            {
                gunAttack.ban();
                animator_gunfire.SetTrigger("attack");
                --gunAttack.soluongdan;
                gameObject.GetComponent<BombController>().setbreaktime();
                vukhi = 0;
                StartCoroutine(capnhapvukhi(0.5f));
            }
        }
    }
}
