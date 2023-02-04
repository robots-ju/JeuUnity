using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimations : MonoBehaviour
{
    [SerializeField]
    [Range(0, 90)]
    [Tooltip("Angle maximum de rotation du mouton lors d'un saut ou d'une chute")]
    private float maxRotation = 25f;

    private Rigidbody2D rb;
    private Mouvement playerMouvement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMouvement = GetComponent<Mouvement>();
    }

    // Update is called once per frame
    void Update()
    {
        freeFallAnim();
        
    }

    private void freeFallAnim() {
        /* Falling + jumping animation */
        float newRotation = 0;
        if (!playerMouvement.isGrounded()) { // Si le joueur est en l'air
            float yVel = rb.velocity.y; // Vitesse verticale actuelle
            float jumpVelocity = playerMouvement.getJumpVelocity(); // Vitesse verticale par saut

            // Calculer la nouvelle rotation
            newRotation = yVel / jumpVelocity * maxRotation;
    
        }

        // Appliquer la rotation
        if (rb.velocity.x < 0) { // Si le joueur se déplace vers la gauche
            transform.rotation = Quaternion.Euler(0, 0, -newRotation);
        } else { // Si le joueur se déplace vers la droite
            transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }

        
    }
}
