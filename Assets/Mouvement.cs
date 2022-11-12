using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{[Range(5, 45)]
    static int vitesseX = 10;

    [Range(5,45)]
    static int vitesseY = 10;

    private Vector2 speed = new Vector2(vitesseX, vitesseY);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector2 mouvement = new Vector2(inputX * speed.x, speed.y * inputY);

        mouvement *= Time.deltaTime;

        transform.Translate(mouvement);
        
    }
}
