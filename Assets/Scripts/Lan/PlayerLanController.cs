using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static Cinemachine.CinemachineFreeLook;

public class PlayerLanController : NetworkBehaviour
{
    // Start is called before the first frame update
    public float speed=4;
    public int mauhientai;
    public int maumax = 5;
    public bool trangthaikhien;
    float ngang;
    float doc;
    public Vector2 huongnhin = new Vector2(0,-1);
    private Vector2 vecmove;
    public float timebtconlai;
    float timebattu = 1.5f;
    public bool trangthaibt;
    int nhapnhay = 0;
    public int vukhi;
    public bool vohieuhoamove;
    public int soluongdan;
    //prefab
    public GameObject robotprefab;
    public GameObject dan;
    
    //gameobject
    Rigidbody2D rigidbody2d;
    Animator animator_body;
    public Animator animator_overlay;
    public Animator animator_vukhi;
    public Animator animator_gunfire;
    public HealthBar healthBar;
    public GameObject thanhmau;
    public GameObject khien;
    public GameObject weapon;
    public GameObject gunfire;
    public GameObject overlay;
    public GameObject nickname;
    HammerAttackLanController hammerAttack;
    GameObject robot;
    CheckAndShowVictory checkwin;

    public void Awake(){
        if (SceneManager.GetActiveScene().name == "LobbyLan")
        {
            thanhmau.SetActive(false);

        }
    }
    public void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator_body = GetComponent<Animator>();
        trangthaikhien = false;
        animator_body.SetFloat("LookX", huongnhin.x);
        animator_body.SetFloat("LookY", huongnhin.y);
        animator_overlay.SetFloat("LookX", huongnhin.x);
        animator_overlay.SetFloat("LookY", huongnhin.y);
        animator_vukhi.SetFloat("LookX", huongnhin.x);
        animator_vukhi.SetFloat("LookY", huongnhin.y);
        trangthaibt = false;
        timebtconlai = 0;
        mauhientai = 3;
        healthBar.setValue(mauhientai / (float)maumax);
        vukhi = 0;
        soluongdan = 0;
        vohieuhoamove = false;
        hammerAttack = gameObject.GetComponent<HammerAttackLanController>();
        if (SceneManager.GetActiveScene().name == "LobbyLan")
        {
            thanhmau.SetActive(false);
            gameObject.GetComponent<PlayerLanController>().enabled = false;

        }
        checkwin = GameObject.Find("CheckVictoryLan(Clone)").GetComponent<CheckAndShowVictory>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (SceneManager.GetActiveScene().name == "LobbyLan")
        {
            thanhmau.SetActive(false);
            return;
        }
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
        if (IsOwner)
        {
            //ngang = Input.GetAxis("Horizontal");
            //doc = Input.GetAxis("Vertical");
            if (vohieuhoamove == false)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    ngang = -0.7f;
                }
                else
                {
                    if (Input.GetKey(KeyCode.D))
                    {
                        ngang = 0.7f;
                    }
                    else
                    {
                        ngang = 0;
                    }
                }
                if (Input.GetKey(KeyCode.S))
                {
                    doc = -0.7f;
                }
                else
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        doc = 0.7f;
                    }
                    else
                    {
                        doc = 0;
                    }
                }
                if (Input.GetKeyDown(KeyCode.F) && vukhi == 1)
                {
                    if (IsHost)
                    {
                        spawnrobot(0);
                    }
                    if (IsClient && !IsHost)
                    {
                        goispawnrobotServerRpc((int)OwnerClientId);
                    }
                    vohieuhoamove = true;
                    gameObject.GetComponent<CircleCollider2D>().isTrigger = true;

                }
                if (vukhi == 2)
                {
                    weapon.transform.localPosition = new Vector2(0, -0.3f);
                }
                if (Input.GetKeyDown(KeyCode.F) && vukhi == 2)
                {
                    animator_body.SetTrigger("attack");
                    animator_overlay.SetTrigger("attack");
                    animator_vukhi.SetTrigger("attack");
                    hammerAttack.requestHammerAttackServerRpc();
                    gameObject.GetComponent<BombLanController>().setbreaktime();
                    vukhi = 0;
                    StartCoroutine(capnhapvukhi(1f));
                }
                if (vukhi == 3)
                {
                    if (doc > 0)
                    {
                        weapon.transform.localPosition = new Vector2(0, 0.34f);
                        gunfire.transform.localPosition = new Vector2(0, 1.5f);
                        gunfire.transform.localRotation = Quaternion.Euler(0, 0, -90);
                    }
                    else if (doc < 0)
                    {
                        weapon.transform.localPosition = new Vector2(0, -1f);
                        gunfire.transform.localPosition = new Vector2(0, -2.07f);
                        gunfire.transform.localRotation = Quaternion.Euler(0, 0, 90);
                    }
                    else if (ngang < 0)
                    {
                        weapon.transform.localPosition = new Vector2(-0.41f, -0.58f);
                        gunfire.transform.localPosition = new Vector2(-1.81f, -0.43f);
                        gunfire.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (ngang > 0)
                    {
                        weapon.transform.localPosition = new Vector2(0.41f, -0.58f);
                        gunfire.transform.localPosition = new Vector2(1.81f, -0.43f);
                        gunfire.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                if (Input.GetKeyDown(KeyCode.F) && vukhi == 3)
                {
                    if (soluongdan > 1)
                    {
                        reqestBanServerRpc();
                        
                        animator_gunfire.SetTrigger("attack");
                        --soluongdan;
                    }
                    else
                    {
                        reqestBanServerRpc();
                        //animator_body.SetTrigger("attack");
                        //animator_overlay.SetTrigger("attack");
                        //animator_vukhi.SetTrigger("attack");
                        animator_gunfire.SetTrigger("attack");
                        --soluongdan;
                        gameObject.GetComponent<BombLanController>().setbreaktime();
                        vukhi = 0;
                        StartCoroutine(capnhapvukhi(0.5f));
                    }
                    
                }
            }
            else
            {
                ngang = 0;
                doc = 0;
            }

        }

        vecmove = new Vector2(ngang, doc);
        Vector2 vectest = vecmove;
        vectest.Normalize();
        if (IsOwner)
        {
            if (ngang != 0 || doc != 0)
            {
                huongnhin = vecmove;
                huongnhin.Normalize();
                syncHuongNhinServerRpc(huongnhin);
                animator_body.SetFloat("LookX", huongnhin.x);
                animator_body.SetFloat("LookY", huongnhin.y);
                animator_overlay.SetFloat("LookX", huongnhin.x);
                animator_overlay.SetFloat("LookY", huongnhin.y);
                animator_vukhi.SetFloat("LookX", huongnhin.x);
                animator_vukhi.SetFloat("LookY", huongnhin.y);
                animator_gunfire.SetFloat("LookX", huongnhin.x);
                animator_gunfire.SetFloat("LookY", huongnhin.y);
            }
            animator_body.SetFloat("Speed", vecmove.magnitude);
            animator_overlay.SetFloat("Speed", vecmove.magnitude);
            animator_vukhi.SetFloat("Speed", vecmove.magnitude);
        }
        

    }
    void FixedUpdate()
    {
        if(ngang !=0 && doc != 0)
        {
            ngang = ngang * 0.707106f;
            doc = doc * 0.707106f;
        }
        Vector2 vitri = rigidbody2d.position;
        vitri.x += speed * ngang * Time.deltaTime;
        vitri.y += speed * doc * Time.deltaTime;
        rigidbody2d.MovePosition(vitri);
    }
    public void thaydoimau(int luongmau)
    {
        if (trangthaikhien == true && luongmau < 0)
        {
            tatkhien();
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
            animator_body.SetBool("die", true);
            gunfire.SetActive(false);
            weapon.SetActive(false);
            thanhmau.SetActive(false);
            overlay.SetActive(false);
            //Destroy(gameObject, 2f);
            StartCoroutine(choxoa());
            Destroy(gameObject, 3f);
            Destroy(robot);
            
        }
    }
    private void OnDestroy()
    {
        checkwin.reqestgiamslServerRpc();
    }
    public void batkhien()
    {
        khien.SetActive(true);
        trangthaikhien = true;
    }
    public void tatkhien()
    {
        khien.SetActive(false);
        trangthaikhien = false;
    }
    private IEnumerator choxoa()
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
    public void ban()
    {
        Vector2 vitri = gameObject.transform.position;
        if (Mathf.Abs(huongnhin.x) == Mathf.Abs(huongnhin.y))
        {
            vitri.y -= 0.5f;
            GameObject Dan = Instantiate(dan, new Vector2(vitri.x, (vitri.y + huongnhin.y * 1.5f)), Quaternion.Euler(0, 0, 90f));
            Dan.GetComponent<NetworkObject>().Spawn();
            Dan.GetComponent<DanLanController>().themluc(new Vector2(0, huongnhin.y), 500f);
        }
        else
        {
            if (Mathf.Abs(huongnhin.y) > 0)
            {
                if (huongnhin.y > 0)
                {
                    GameObject Dan = Instantiate(dan, vitri + huongnhin + new Vector2(0f, -0.8f), Quaternion.Euler(0, 0, 90f));
                    //Dan.GetComponent<NetworkObject>().Spawn();
                    Dan.GetComponent<DanLanController>().themluc(huongnhin, 500f);
                }
                if (huongnhin.y < 0)
                {
                    GameObject Dan = Instantiate(dan, vitri + huongnhin + new Vector2(0f, -0.5f), Quaternion.Euler(0, 0, 90f));
                    //Dan.GetComponent<NetworkObject>().Spawn();
                    Dan.GetComponent<DanLanController>().themluc(huongnhin, 500f);
                }
            }
            else
            {
                vitri.y -= 0.5f;
                GameObject Dan = Instantiate(dan, vitri + huongnhin * 1, Quaternion.Euler(0, 0, 0));
                //Dan.GetComponent<NetworkObject>().Spawn();
                Dan.GetComponent<DanLanController>().themluc(huongnhin, 500f);
            }
        }
    }
    public IEnumerator bovohieuhoatime(float time)
    {
        yield return new WaitForSeconds(time);
        vohieuhoamove = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DanController>() != null)
        {
            thaydoimau(-1);
            Destroy(collision.gameObject);
        }
    }
    public void spawnrobot(int id)
    {
        robot = Instantiate(robotprefab, rigidbody2d.position + new Vector2(0, -0.5f), Quaternion.identity);
        //robot.GetComponent<RobotLanController>().player = gameObject.GetComponent<PlayerLanController>();
        robot.GetComponent<NetworkObject>().Spawn();
        robot.GetComponent<NetworkObject>().ChangeOwnership((ulong)id);
    }

    [ServerRpc]
    public void goispawnrobotServerRpc(int id)
    {
        spawnrobot(id);
    }
    [ServerRpc(RequireOwnership = false)]
    public void reqestBanServerRpc()
    {
        goibanClientRpc();
    }
    [ClientRpc]
    public void goibanClientRpc()
    {
        ban();
    }
    [ClientRpc]
    public void syncHuongNhinClientRpc(Vector2 huongnhinx)
    {
        huongnhin = huongnhinx;
    }
    [ServerRpc(RequireOwnership = false)]
    public void syncHuongNhinServerRpc(Vector2 huongnhinx)
    {
        syncHuongNhinClientRpc(huongnhinx);
        huongnhin = huongnhinx;
    }
    [ServerRpc(RequireOwnership = false)]
    public void requestChangeHealthServerRpc(int luongmau)
    {
        callChangeHealthClientRpc(luongmau);
    }
    [ClientRpc]
    public void callChangeHealthClientRpc(int luongmau)
    {
        thaydoimau(luongmau);
    }
}
