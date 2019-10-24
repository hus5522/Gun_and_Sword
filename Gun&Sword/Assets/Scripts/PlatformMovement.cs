using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    private Vector3 posA; // start position
    private Vector3 posB; // end position
    private Vector3 nextPos; // go to next position

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;
    

	// Use this for initialization
	void Start () {
        posA = childTransform.localPosition; //시작 위치
        posB = transformB.localPosition; // 끝 위치
        nextPos = posB;
	}
	
	// Update is called once per frame
	void Update () {
        Move();

        if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
        {
            ChangeDestination();
        }

        if (Player.instance.buttonOn)
        {
            speed = 9;
        }
        
	}

    private void Move()
    {
        childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != posA ? posA : posB; // 다음 위치가 a가 아니면 a로 바꾸고, 맞으면 b로 바꿈
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.layer = 8;
            //움직이는 발판 위에 있는 동안, other객체(Player)를 childTransform의 자식으로 만듦
            //그래서 내려갈때 부드럽게 내려 갈 수 있음. 이게 없으면 내려가는동안 계속 점프모션나옴
            other.transform.SetParent(childTransform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        //움직이는 발판을 벗어낫을 경우, other객체(Player)객체를 원래대로 되돌림.
        other.transform.SetParent(null);
    }
}
