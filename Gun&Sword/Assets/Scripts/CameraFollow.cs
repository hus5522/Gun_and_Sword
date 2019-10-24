using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private float xMax;

    [SerializeField]
    private float yMax;

    [SerializeField]
    private float xMin;

    [SerializeField]
    private float yMin;

    private Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (Player.instance.yKey)
        {
            moveCamToYellowDoor();
            Invoke("resetValue", 1.5f);
        }
        else if (Player.instance.bKey)
        {
            moveCamToBlueDoor();
            Invoke("resetValue", 1.5f);
        }
        else if (Player.instance.gKey)
        {
            moveCamToGreenDoor();
            Invoke("resetValue", 1.5f);
        }
        else if (Player.instance.sKey)
        {
            moveCamToSilverDoor();
            Invoke("resetValue", 1.5f);
        }
        else
        {
            transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
        }
    }

    private void moveCamToYellowDoor()
    {
        /*
        if(GameManager.instance.stage == 1)
            transform.position = new Vector3(23.8f, 4.8f, -10f);
            */

        if (Player.instance.CurrentStage== 0)
            transform.position = new Vector3(23.8f, 4.8f, -10f);
    }

    private void moveCamToBlueDoor()
    {
        /*
        if (GameManager.instance.stage == 2)
            transform.position = new Vector3(34.4f, -6.3f, -10f);
        if (GameManager.instance.stage == 3)
            transform.position = new Vector3(23.7f, 5.3f, -10f);
            */
        if (Player.instance.CurrentStage == 1)
            transform.position = new Vector3(34.4f, -6.3f, -10f);
        if (Player.instance.CurrentStage == 2)
            transform.position = new Vector3(23.7f, 5.3f, -10f);
    }

    private void moveCamToGreenDoor()
    {
        /*
        if (GameManager.instance.stage == 2)
            transform.position = new Vector3(11.4f, -5.8f, -10f);
        if (GameManager.instance.stage == 3)
            transform.position = new Vector3(23.7f, -6.3f, -10f);
            */
        if (Player.instance.CurrentStage == 1)
            transform.position = new Vector3(11.4f, -5.8f, -10f);
        if (Player.instance.CurrentStage == 2)
            transform.position = new Vector3(23.7f, -6.3f, -10f);
    }

    private void moveCamToSilverDoor()
    {
        /*
        if (GameManager.instance.stage == 3)
            transform.position = new Vector3(23.7f, 20.5f, -10f);
            */
        if (Player.instance.CurrentStage == 2)
            transform.position = new Vector3(23.7f, 20.5f, -10f);
    }

    private void resetValue()
    {
        Player.instance.yKey = false;
        Player.instance.bKey = false;
        Player.instance.gKey = false;
        Player.instance.sKey = false;
    }

}
