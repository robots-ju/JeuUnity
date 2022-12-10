using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{

    // Paramètres
    [SerializeField]
    private int walkSpeed = 10;
    [SerializeField]
    [Range(0, 1000)]
    private int jumpHeight = 100;
    [SerializeField]
    private float jumpDelay = 0.1f;

    // Variables
    private float groundTime = 0;
    Rigidbody2D rigidbody2DMouton;
    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2DMouton = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()  
    {
        // Déplacement sur l'axe x
        float inputX = Input.GetAxis("Horizontal");
        rigidbody2DMouton.velocity = new Vector2(inputX * walkSpeed,rigidbody2DMouton.velocity.y);

        // Atterissage
        if (jumping && isGrounded())
        {
            groundTime += Time.deltaTime;
            if (groundTime > jumpDelay)
            {
                groundTime = 0;
                jumping = false;
            }
        }

        if (!jumping && Input.GetAxis("Jump") != 0 && isGrounded()) {
            jumping = true;
            rigidbody2DMouton.AddForce(new Vector2(0, jumpForce()));
        }
    }

    private bool isGrounded() {
        Vector2 lowerCenter = transform.position;
        lowerCenter.y -= transform.localScale.y/2f;
        Vector2 lowerLeft = new Vector2(lowerCenter.x - (transform.localScale.x + 0.1f), lowerCenter.y);
        Vector2 lowerRight = new Vector2(lowerCenter.x + (transform.localScale.x + 0.1f), lowerCenter.y);
        return Physics2D.Raycast(lowerLeft, Vector2.down, 0.1f) || Physics2D.Raycast(lowerRight, Vector2.down, 0.1f);
    }

    private float jumpForce() {
        float adaptedJumpHeight = jumpHeight * 100;
        return Mathf.Sqrt(-2 * Physics2D.gravity.y * adaptedJumpHeight * rigidbody2DMouton.gravityScale);
    }
}