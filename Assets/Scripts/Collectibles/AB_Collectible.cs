using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class AB_Collectible : MonoBehaviour
{
    [SerializeField]
    protected AudioClip pickUpSound;

    void Reset() {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Collect(other.gameObject);
        }
    }

    protected void Collect(GameObject playerObject) {
        AudioSource.PlayClipAtPoint(pickUpSound, transform.position);
        ApplyEffect(playerObject);
        Destroy(gameObject);
    }

    abstract protected void ApplyEffect(GameObject playerObject);

}
