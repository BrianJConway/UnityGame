using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {
    private Rigidbody2D myRigidBody;
    private Collider2D myCollider;
    public Text scoreText;
    public AudioSource JumpSfx;
    public AudioSource DeathSfx;

    public float jumpForce = 500f;
    private Animator myAnim;
    private float bunnyHurtTime = -1;
    private float startTime;
    private int jumpsLeft = 2;
    
	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }

        if( bunnyHurtTime == -1 )
        {
            if (Input.GetButtonUp("Jump") || Input.GetButtonUp("Fire1") && jumpsLeft > 0)
            {
                if (myRigidBody.velocity.y < 0)
                {
                    myRigidBody.velocity = Vector2.zero;
                }

                if (jumpsLeft == 1)
                {
                    myRigidBody.AddForce(transform.up * jumpForce * 0.75f);
                }
                else
                {
                    myRigidBody.AddForce(transform.up * jumpForce);
                }

                JumpSfx.Play();
                jumpsLeft--;
            }

            myAnim.SetFloat("v_velocity", myRigidBody.velocity.y);
        }
        else
        {
            if( Time.time > bunnyHurtTime + 2 )
            {
                SceneManager.LoadScene("Game");
            }
        }

        scoreText.text = (Time.time - startTime).ToString("0.0");
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.transform.tag == "Enemy" )
        {
            foreach (prefabSpawner spawner in FindObjectsOfType<prefabSpawner>())
            {
                spawner.enabled = false;
            }
            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                moveLefter.enabled = false;
            }

            bunnyHurtTime = Time.time;
            myAnim.SetBool( "bunnyHurt", true );
            DeathSfx.Play();

            myRigidBody.velocity = Vector2.zero;
            myRigidBody.AddForce(transform.up * jumpForce );
            myCollider.enabled = false;
        }
        else if( collision.transform.tag == "Ground")
        {
            jumpsLeft = 2;
        }
    }
}
