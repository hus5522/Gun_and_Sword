﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Turret2 : MonoBehaviour
{
    [SerializeField]
    private Transform bulletPos;

    [SerializeField]
    private GameObject bulletPrefab;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("shootBulletOfTurret");
    }

    IEnumerator shootBulletOfTurret()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            GameObject tmp = (GameObject)Instantiate(bulletPrefab, bulletPos.position, Quaternion.Euler(0,0,90));
            tmp.GetComponent<TurretBullet>().Initialize(Vector2.left);
        }
    }

}
