using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    [SerializeField]
    private EdgeCollider2D swordCollider;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private GameObject keyInBox;

    void Start()
    {
        keyInBox.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "bullet")
        {
            SoundManager.PlaySound("box");
            keyInBox.SetActive(true);
            this.gameObject.SetActive(false);
        }
        
    }
    
    
}
