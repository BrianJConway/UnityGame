using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunnyController : MonoBehaviour {
    private Rigidbody2D myRigidBody;
    public float jumpForce = 500f;
    private Animator myAnim;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetButtonUp("Jump") )
        {
            myRigidBody.AddForce( transform.up * jumpForce );
        }

        myAnim.SetFloat( "v_velocity", myRigidBody.velocity.y );
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.transform.tag == "Enemy" )
        {
            SceneManager.LoadScene("Game");
        }
    }
}
