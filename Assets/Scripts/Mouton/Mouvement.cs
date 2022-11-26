using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    [SerializeField]
    private int walkSpeed = 10;

    [SerializeField]
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
        
        rigidbody2DMouton.velocity = new Vector2(inputX * walkSpeed,0);

        Vector2 castStartPosi = transform.position;
        castStartPosi.y -= transform.localScale.y;

        float jump = Input.GetAxis("Jump");
        if (jump != 0) {
            if (Physics2D.Raycast(castStartPosi, Vector2.down, 0.001f)) {
                rigidbody2DMouton.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * jumpForce)));
            }
        }
    }

}
