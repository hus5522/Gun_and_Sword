using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerStair : MonoBehaviour {

    [SerializeField]
    private GameObject stair;

    [SerializeField]
    private GameObject stair2;

    // Use this for initialization
    void Start () {
        StartCoroutine(Flicking());
    }

    IEnumerator Flicking()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            stair.SetActive(false);
            stair2.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            stair.SetActive(true);
            stair2.SetActive(false);
        }
    }

}
