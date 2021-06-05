using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private float jumpForce = 300f;

    [SerializeField]
    private GameObject bulletObj = null;

    [SerializeField]
    private GameObject bulletPos = null;

    private bool isJump = false;

    private bool move = false;
    private float moveHor = 1;

    // Update is called once per frame
    void Update()
    {
        if (move == true)
        {
            PlayerMove();
        }

        if (Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void PlayerMove()
    {
        //float h = Input.GetAxis("Horizontal");
        float h = moveHor;
        float playerSpeed = h * moveSpeed * Time.deltaTime;
        Vector3 vector3 = new Vector3();
        vector3.x = playerSpeed;
        transform.Translate(vector3);

        if (h < 0)
        {
            GetComponent<Animator>().SetBool("IsMove", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h == 0)
        {
            GetComponent<Animator>().SetBool("IsMove", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsMove", true);
            transform.localScale = new Vector3(1, 1, 1);
        }

    }

    private void PlayerJump()
    {
        if (isJump == false)
        {
            GetComponent<Animator>().SetBool("IsMove", false);
            GetComponent<Animator>().SetBool("IsJump", true);

            Vector2 vector2 = new Vector2(0, jumpForce);
            GetComponent<Rigidbody2D>().AddForce(vector2);
            isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform")
        {
            GetComponent<Animator>().SetBool("IsJump", false);
            isJump = false;
        }
    }

    private void Fire()
    {
        AudioClip audioClip = Resources.Load<AudioClip>("RangedAttack") as AudioClip;
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().Play();
        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        Instantiate(bulletObj, bulletPos.transform.position, quaternion).GetComponent<Bullet>().InstantiateBullet(direction);
    }

    public void OnMove(bool _right)
    {
        if (_right)
        {
            moveHor = 1;
        }
        else
        {
            moveHor = -1;
        }

        move = true;
    }

    public void OffMove()
    {
        move = false;
    }
}
