using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{

    float time;
    Vector3 startPullPosition;
    Vector3 target;
    float timeToReachPortal;
    bool pullingObject = false;
    GameObject gameObjectToMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pullingObject) // Si un objet est en train d'être tiré vers le portail
        {
            time += Time.deltaTime;
            gameObjectToMove.gameObject.transform.position = Vector2.Lerp(startPullPosition, transform.position, time / timeToReachPortal);

            if (time >= timeToReachPortal) // Si le temps est écoulé
            {
                pullingObject = false;
                time = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Lancer l'animation de disparition
            Animator animator = collision.GetComponentInChildren<Animator>();
            animator.SetBool("disappear", true);

            // Désactiver les contrôles du joueur
            collision.GetComponent<Mouvement>().enabled = false;

            // Mettre le joueur à l'arrêt
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            // Arrêter le timer
            collision.GetComponent<Timer>().stopTimer();

            // Déplacer le joueur vers le centre du portail
            PullObjectIntoPortal(collision.gameObject, animator.GetCurrentAnimatorStateInfo(0).length * 2);
        }
    }

    // Méthode pour déplacer le joueur vers la destination en un temps donné
    public void PullObjectIntoPortal(GameObject gameObject, float time)
     {
        gameObjectToMove = gameObject;
        timeToReachPortal = time;
        pullingObject = true;
        time = 0;
        startPullPosition = gameObject.transform.position;
     }
}
