using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttackController : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMovementController movement;
    public GameObject dan;
    public int soluongdan;
    private void Awake()
    {
        soluongdan = 0;
    }
    void Start()
    {
        movement = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ban()
    {
        //gameObject.GetComponent<AudioSource>().PlayOneShot(tiengsung);
        Vector2 vitri = gameObject.transform.position;
        if (Mathf.Abs(movement.huongnhin.x) == Mathf.Abs(movement.huongnhin.y))
        {
            vitri.y -= 0.5f;
            GameObject Dan = Instantiate(dan, new Vector2(vitri.x, (vitri.y + movement.huongnhin.y * 1.5f)), Quaternion.Euler(0, 0, 90f));
            Dan.GetComponent<DanController>().themluc(new Vector2(0, movement.huongnhin.y), 500f);
        }
        else
        {
            if (Mathf.Abs(movement.huongnhin.y) > 0)
            {
                if (movement.huongnhin.y > 0)
                {
                    GameObject Dan = Instantiate(dan, vitri + movement.huongnhin + new Vector2(0f, -0.8f), Quaternion.Euler(0, 0, 90f));
                    Dan.GetComponent<DanController>().themluc(movement.huongnhin, 500f);
                }
                if (movement.huongnhin.y < 0)
                {
                    GameObject Dan = Instantiate(dan, vitri + movement.huongnhin + new Vector2(0f, -0.3f), Quaternion.Euler(0, 0, 90f));
                    Dan.GetComponent<DanController>().themluc(movement.huongnhin, 500f);
                }

            }
            else
            {
                vitri.y -= 0.5f;
                GameObject Dan = Instantiate(dan, vitri + movement.huongnhin * 1, Quaternion.Euler(0, 0, 0));
                Dan.GetComponent<DanController>().themluc(movement.huongnhin, 500f);
            }
        }
    }
}
