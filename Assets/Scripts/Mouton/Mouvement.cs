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
        float inputY = Input.GetAxis("Vertical");

        float mouvement = 0;

        if (inputX != 0) {
            mouvement = inputX * walkSpeed * Time.deltaTime;
        }

        if (Physics2D.Raycast(transform.position, Vector2.down, 0.1F)) {
            if (inputY == 1) {
                rigidbody2DMouton.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * jumpForce)));
            }
        }

        rigidbody2DMouton.MovePosition(new Vector2(mouvement, 0));
        
    }

}
