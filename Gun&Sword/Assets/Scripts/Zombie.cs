using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {
    
    public Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool attack;

    public bool isDied;

    private bool contactPlayer;

    public static Zombie instance = null;

    private Vector3 posA;
    private Vector3 posB;
    private Vector3 nextPos;

    [SerializeField]
    private Transform childTransform;

    [SerializeField]
    private Transform transformB;


    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        isDied = false;
        myAnimator = GetComponent<Animator>();
        posA = childTransform.localPosition;
        posB = transformB.localPosition;
        nextPos = posB;
        contactPlayer = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDied)
        {
            //float horizontal = Input.GetAxis("Horizontal");

            HandleMovement();
            
            if (Vector3.Distance(childTransform.localPosition, nextPos) <= 0.1)
            {
                ChangeDestination();

                Vector3 theScale = transform.localScale;

                theScale.x *= -1; //scale이 -1이면 좌우반전 됨. 

                transform.localScale = theScale; // 변경된 scale을 적용시킴
            }
            //HandleAttacks();
            ResetValues();
        }
        else
            return;
    }

    private void HandleMovement()
    {
        // 좀비 x축 이동범위 : -7.87 ~ 20.35
        //공격중이 아니면 움직이게 만들어라. 즉, 공격하고있을땐 움직이지 않게 해라
        //Animator 의 Attack의 Tag명을 Attack으로 만들어 줘야 함.
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !contactPlayer)
        {
            childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, movementSpeed * Time.deltaTime);
            myAnimator.SetTrigger("walk");
        }

        //myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void ChangeDestination()
    {
        nextPos = nextPos != posA ? posA : posB; // 다음 위치가 a가 아니면 a로 바꾸고, 맞으면 b로 바꿈
    }

    private void HandleAttacks()
    {
        //공격중일때 다시 공격 못하게 함
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && !isDied)
        {
            myAnimator.SetTrigger("attack");
        }

    }

    //값 초기화 해주는 함수 ex) attack을 false로 바꿔서 작동 안하게 하는 등
    private void ResetValues()
    {
        attack = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword")
        {
            SoundManager.PlaySound("zombieDead");
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            isDied = true;
            myAnimator.SetTrigger("die");
            Invoke("deleteZombie", 1.0f);
        }
    }

    private void deleteZombie()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !isDied) // 죽엇을때 공격하는 모션 안나오게 바꿈
        {
            contactPlayer = true;
            myAnimator.SetTrigger("attack");
            myAnimator.Play("Zombie_Attack");
            movementSpeed = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        contactPlayer = false;
        movementSpeed = 8;
    }


}
