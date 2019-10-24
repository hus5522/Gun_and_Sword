using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour {
    

    private Animator myDoorAnimator;
    //private BoxCollider2D myBox;

    [SerializeField]
    private string whatDoor;

    private bool isOpenY;
    private bool isOpenB;
    private bool isOpenG;
    private bool isOpenS;


    [SerializeField]
    private BoxCollider2D playerCollider;

    [SerializeField]
    private BoxCollider2D doorCollider;


    // Use this for initialization
    void Start () {
        isOpenY = false;
        isOpenB = false;
        isOpenG = false;
        isOpenS = false;
        myDoorAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (Player.instance.yKey && !isOpenY)
        {
            myDoorAnimator.SetTrigger("YKey");
            Invoke("eraseDoor", 1.0f);
        }

        if (Player.instance.bKey && !isOpenB)
        {
            myDoorAnimator.SetTrigger("BKey");
            Invoke("eraseDoor", 1.0f);
        }

        if (Player.instance.gKey && !isOpenG)
        {
            myDoorAnimator.SetTrigger("GKey");
            Invoke("eraseDoor", 1.0f);
        }

        if (Player.instance.sKey && !isOpenS)
        {
            myDoorAnimator.SetTrigger("SKey");
            Invoke("eraseDoor", 1.0f);
        }
    }

    private void eraseDoor()
    {
        if (Player.instance.yKey && !isOpenY && whatDoor == "YellowDoor")
        {
            SoundManager.PlaySound("openDoor");
            doorCollider.isTrigger = true;
            this.gameObject.SetActive(false);
            isOpenY = true;
        }

        if (Player.instance.bKey && !isOpenB && whatDoor == "BlueDoor")
        {
            SoundManager.PlaySound("openDoor");
            doorCollider.isTrigger = true;
            this.gameObject.SetActive(false);
            isOpenB = true;
        }

        if (Player.instance.gKey && !isOpenG && whatDoor == "GreenDoor")
        {
            SoundManager.PlaySound("openDoor");
            doorCollider.isTrigger = true;
            this.gameObject.SetActive(false);
            isOpenG = true;
        }

        if (Player.instance.sKey && !isOpenS && whatDoor == "SilverDoor")
        {
            SoundManager.PlaySound("openDoor");
            doorCollider.isTrigger = true;
            this.gameObject.SetActive(false);
            isOpenS = true;
        }
    }
    
    
}
