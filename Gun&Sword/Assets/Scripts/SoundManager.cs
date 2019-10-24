using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static AudioClip playerDeadSound, playerShootSound, playerAttackSound, enemyDeathSound, getKeySound, openDoorSound, stageClearSound, backgroundSound, boxSound;
    static AudioSource audioSrc;
    //public static SoundManager instance = null;

	// Use this for initialization
	void Start () {
       /*
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        */
        playerDeadSound = Resources.Load<AudioClip>("character_dead");
        playerShootSound = Resources.Load<AudioClip>("gun");
        playerAttackSound = Resources.Load<AudioClip>("sword");
        enemyDeathSound = Resources.Load<AudioClip>("zombie_dead");
        getKeySound = Resources.Load<AudioClip>("getKey");
        openDoorSound = Resources.Load<AudioClip>("OpenDoor");
        stageClearSound = Resources.Load<AudioClip>("StageClear");
        backgroundSound = Resources.Load<AudioClip>("background");
        boxSound = Resources.Load<AudioClip>("box");

        audioSrc = GetComponent<AudioSource>();
        SoundManager.PlaySound("background");
    }


    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "playerDead" :
                audioSrc.PlayOneShot(playerDeadSound);
                break;
            case "shoot":
                audioSrc.PlayOneShot(playerShootSound);
                break;
            case "attack":
                audioSrc.PlayOneShot(playerAttackSound);
                break;
            case "zombieDead":
                audioSrc.PlayOneShot(enemyDeathSound);
                break;
            case "getKey":
                audioSrc.PlayOneShot(getKeySound);
                break;
            case "openDoor":
                audioSrc.PlayOneShot(openDoorSound);
                break;
            case "StageClear":
                audioSrc.PlayOneShot(stageClearSound);
                break;
            case "background":
                audioSrc.PlayOneShot(backgroundSound);
                break;
            case "box":
                audioSrc.PlayOneShot(boxSound);
                break;
        }
    }
}
