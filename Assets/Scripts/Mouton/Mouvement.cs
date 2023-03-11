using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{

    private const int groundLayerMask = 1 << 8; // Layer 8 = Ground
    private const int groundWallLayerMask = groundLayerMask | (1 << 9);

    // Paramètres
    [SerializeField]
    [Tooltip("Vitesse de déplacement du mouton")]
    private int walkSpeed = 10;
    [SerializeField]
    [Range(0, 1000)]
    [Tooltip("Hauteur du saut du mouton")]
    private int jumpHeight = 100;
    [SerializeField]
    [Tooltip("Délai minimum entre deux sauts")]
    private float jumpDelay = 0.1f;

    // Variables
    private float groundTime = 0;
    Rigidbody2D rigidbody2DMouton;
    BoxCollider2D boxCollider2DMouton;
    SpriteRenderer sr;
    private bool jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2DMouton = GetComponent<Rigidbody2D>();
        boxCollider2DMouton = GetComponent<BoxCollider2D>();
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()  
    {
        // Déplacement sur l'axe x
        float inputX = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(inputX * walkSpeed,rigidbody2DMouton.velocity.y);
        
        rigidbody2DMouton.velocity = new Vector2(inputX * walkSpeed,rigidbody2DMouton.velocity.y);
        sr.flipX = rigidbody2DMouton.velocity.x < 0;

        // Lors du contact avec le sol après un saut
        if (jumping && isGrounded())
        {
            // Compter le temps passé depuis l'atterissage
            groundTime += Time.deltaTime;
            if (groundTime > jumpDelay) // Si le temps est supérieur au délai de saut
            {
                // Reset les valeurs de saut
                groundTime = 0;
                jumping = false;
            }
        }

        // Saut
        if (!jumping && Input.GetAxis("Jump") != 0 && isGrounded()) { // Si le mouton peut sauter
            jumping = true;
            rigidbody2DMouton.AddForce(new Vector2(0, getJumpForce()));
        }
    }

    /**
    * Vérifie si le mouton est au sol
    */
    public bool isGrounded() {
        // Trouver les coins inférieurs du mouton
        Bounds bounds = boxCollider2DMouton.bounds;
        Vector2 lowerCenter = bounds.center;
        lowerCenter.y -= bounds.extents.y;
        Vector2 lowerLeftRayOrigin = new Vector2(lowerCenter.x + (bounds.extents.x / 2), lowerCenter.y);
        Vector2 lowerRightRayOrigin = new Vector2(lowerCenter.x - (bounds.extents.x / 2), lowerCenter.y);

        // Vérifier si le mouton touche le sol
        return Physics2D.Raycast(lowerLeftRayOrigin, Vector2.down, 0.1f, groundLayerMask) || Physics2D.Raycast(lowerRightRayOrigin, Vector2.down, 0.1f, groundLayerMask);;
    }

    /**
    * Calcul la force nécessaire pour atteindre la hauteur de saut souhaité
    */
    private float getJumpForce() {
        float adaptedJumpHeight = jumpHeight * 100;
        return Mathf.Sqrt(-2 * Physics2D.gravity.y * adaptedJumpHeight * rigidbody2DMouton.gravityScale);
    }

    /**
    * Calcul la vitesse nécessaire pour atteindre la hauteur de saut souhaité
    */
    public float getJumpVelocity() {
        return (getJumpForce()/rigidbody2DMouton.mass)*Time.fixedDeltaTime;
    }

    public Vector2 getVelocity() {
        return rigidbody2DMouton.velocity;
    }
}