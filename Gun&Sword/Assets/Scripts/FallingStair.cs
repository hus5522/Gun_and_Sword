using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStair : MonoBehaviour {

    private Rigidbody2D rb2d;

    public float fallDelay;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb2d.isKinematic = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
        yield return 0; //IEnumerator off
    }

}
