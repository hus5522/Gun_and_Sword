using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour {
    private Player player;

    private BoxCollider2D playerCollider;

    [SerializeField]
    private BoxCollider2D platformCollider;

    // Use this for initialization
    void Start()
    {
        playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
            player.myAnimator.SetTrigger("die");
        }
    }
    
}
