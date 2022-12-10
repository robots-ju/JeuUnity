using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    [SerializeField]
    private int walkSpeed = 10;

    [SerializeField]
    [Range(0, 1000)]
    private int jumpForce = 10;
    Rigidbody2D rigidbody2DMouton;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2DMouton = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()  
    {
        float inputX = Input.GetAxis("Horizontal");
        
        rigidbody2DMouton.velocity = new Vector2(inputX * walkSpeed,rigidbody2DMouton.velocity.y);

        Vector2 lowerCenter = transform.position;
        lowerCenter.y -= transform.localScale.y/2f;
        Vector2 lowerLeft = new Vector2(lowerCenter.x - (transform.localScale.x + 0.1f), lowerCenter.y);
        Vector2 lowerRight = new Vector2(lowerCenter.x + (transform.localScale.x + 0.1f), lowerCenter.y);
        
        float jump = Input.GetAxis("Jump");
    
        Debug.DrawLine(lowerLeft, new Vector2(lowerLeft.x, lowerLeft.y-0.1f), Color.red, 1f);
        Debug.DrawLine(lowerRight, new Vector2(lowerRight.x, lowerRight.y-0.1f), Color.red, 1f);
        if (jump != 0) {
            if (Physics2D.Raycast(lowerLeft, Vector2.down, 0.05f) || Physics2D.Raycast(lowerRight, Vector2.down, 0.05f)) {
                rigidbody2DMouton.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * jumpForce)));
            }
        }
    }
}