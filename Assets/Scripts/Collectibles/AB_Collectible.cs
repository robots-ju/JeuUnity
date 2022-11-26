using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class AB_Collectible : MonoBehaviour
{
    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Bonjour");
        if (other.gameObject.tag == "Player") {
            Debug.Log("HIT");
            Collect(other.gameObject);
        }
    }

    protected void Collect(GameObject playerObject) {
        audioSource.Play();
        Destroy(gameObject);
        ApplyEffect(playerObject);
    }

    abstract protected void ApplyEffect(GameObject playerObject);

}
