using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimations : MonoBehaviour
{
    [SerializeField]
    [Range(0, 360)]
    [Tooltip("Angle maximum de rotation du mouton lors d'un saut ou d'une chute")]
    private float maxRotation = 360f;

    private Rigidbody2D rb;
    private Mouvement playerMouvement;
    private Animator animator;
    private Transform body;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMouvement = GetComponent<Mouvement>();
        animator = GetComponentInChildren<Animator>();
        body = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        freeFallAnim();
        
        walkWaitAnimation();
    }

    private void freeFallAnim() {
        /* Falling + jumping animation */
        float newRotation = 0;
        if (!playerMouvement.isGrounded()) { // Si le joueur est en l'air
            animator.SetBool("isJumping", true);
            float yVel = rb.velocity.y; // Vitesse verticale actuelle
            float jumpVelocity = playerMouvement.getJumpVelocity(); // Vitesse verticale par saut

            // Calculer la nouvelle rotation
            newRotation = yVel / jumpVelocity * maxRotation;
    
        } else {
            animator.SetBool("isJumping", false);
        }

        // Appliquer la rotation
        if (rb.velocity.x < 0) { // Si le joueur se déplace vers la gauche
            body.rotation = Quaternion.Euler(0, 0, -newRotation);
        } else { // Si le joueur se déplace vers la droite
            body.rotation = Quaternion.Euler(0, 0, newRotation);
        }        
    }

    private void walkWaitAnimation(){
        if (playerMouvement.getVelocity().x != 0) { // Si le joueur se déplace
            animator.SetBool("isWalking", true);
        } else { // Si le joueur ne se déplace pas
            animator.SetBool("isWalking", false);
        }
    }
}
