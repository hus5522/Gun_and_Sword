using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyCollider : MonoBehaviour {

    [SerializeField]
    private EdgeCollider2D SwordCollider;

    [SerializeField]
    private BoxCollider2D KeyCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Sword"))
            Physics2D.IgnoreCollision(SwordCollider, KeyCollider, true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
            Physics2D.IgnoreCollision(SwordCollider, KeyCollider, false);

        
    }
}
