using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private Rigidbody2D myRigidBody;
    public Animator myAnimator;

    [SerializeField]
    private Transform bulletPos;

    [SerializeField]
    private float movementSpeed;

    private bool attack;
    private bool shoot;
    private bool facingRight;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool isGrounded;
    private bool jump;
    private bool jumpShoot;

    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float hitForce; // 피격시 밀리는 힘

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private EdgeCollider2D SwordCollider;
    

    public bool isDied;
    public bool isCleared;

    public static Player instance = null;

    public bool yKey;
    public bool bKey;
    public bool gKey;
    public bool sKey;
    public bool buttonOn;

    public bool IsFalling
    {
        get
        {
            return myRigidBody.velocity.y < 0;
        }
    }

    [SerializeField]
    private GameObject GameOverUI;

    [SerializeField]
    private GameObject StageClearMsg;

    [SerializeField]
    private SpriteRenderer mySpriteRenderer;

    private int playerHP;

    [SerializeField]
    private GameObject[] heart;

    [SerializeField]
    private GameObject[] deleted_Heart;

    public static int currentStage = 0;

    public int CurrentStage
    {
        get { return currentStage; }
        set { currentStage = value; }
    }

    

    // Use this for initialization
    void Start() {
        
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        isDied = false;
        isCleared = false;
        facingRight = true;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        GameOverUI.SetActive(false);
        StageClearMsg.SetActive(false);
        //SoundManager.PlaySound("background");
        playerHP = 3;

        //하트 UI 초기화
        for (int i = 0; i < 3; i++)
        {
            heart[i].SetActive(true);
            deleted_Heart[i].SetActive(false);
        }
        
    }

    //handleInput 함수를 사용하려면 매 프레임마다 눌렀는지 체크해야하는데, fixedUpdate는 그게 안댐
    private void Update()
    {
        if (!isDied)
            HandleInput();
        else
        {
            myAnimator.Play("Dead");
            return;
        }
         
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!isDied)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");

            isGrounded = IsGrounded();

            HandleMovement(horizontal);
            Flip(horizontal);
            HandleAttacks();
            HandleShoot();
            HandleLayers();
            ResetValues();
        }
        else
            return;
    }

    private void HandleMovement(float horizontal)
    {
        //캐릭터가 떨어지고 있는 중이라면 실행
        if (IsFalling)
        {
            gameObject.layer = 9; //layer9 = Falling layer
            myAnimator.SetBool("land", true);
        }

        //공격중이 아니면 움직이게 만들어라. 즉, 공격하고있을땐 움직이지 않게 해라
        //Animator 의 Attack의 Tag명을 Attack으로 만들어 줘야 함.
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))// && (isGrounded || airControl))
        {
            myRigidBody.velocity = new Vector2(horizontal * movementSpeed, myRigidBody.velocity.y);
        }

        if (isGrounded && jump) //지상에 있다가 점프신호를 받으면 실행됨
        {
            isGrounded = false; //공중에 있음을 알림
            myRigidBody.AddForce(new Vector2(0, jumpForce)); //점프하는 힘 추가
            myAnimator.SetTrigger("jump"); //점프 상태임을 알리는 trigger 발생
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void HandleAttacks()
    {
        //공격중일때 다시 공격 못하게 함
        if (attack && isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            SoundManager.PlaySound("attack");
            myAnimator.SetTrigger("attack");
            myRigidBody.velocity = Vector2.zero;
        }

        //점프 공격을 눌럿고, 공중에 있고, 점프공격중이 아니라면 실행
        if (jumpShoot && !isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsTag("JumpShoot"))
        {
            SoundManager.PlaySound("shoot");
            myAnimator.SetBool("jumpShoot", true);
        }

        if (!jumpShoot && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsTag("JumpShoot"))
        {
            myAnimator.SetBool("jumpShoot", false);
        }

    }

    private void HandleShoot()
    {
        //공격중일때 다시 공격 못하게 함
        if (shoot && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            SoundManager.PlaySound("shoot");
            myAnimator.SetTrigger("shoot");
            if (isGrounded)
            {
                myRigidBody.velocity = Vector2.zero; // 이걸 없애야 점프중에 총쏴도 바로 안 떨어짐
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && !IsFalling)
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            attack = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            shoot = true;
            jumpShoot = true;
        }
        /*
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (currentStage == 0)
            {
                currentStage++;
                SceneManager.LoadScene("Stage2");
            }
            else if (currentStage == 1)
            {
                currentStage++;
                SceneManager.LoadScene("Stage3");
            }
            else if (currentStage == 2)
            {
                currentStage = 0;
                SceneManager.LoadScene("AllClear");
            }
        }
        */
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight; //true -> false or false -> true
            
            Vector3 theScale = transform.localScale;

            theScale.x *= -1; //scale이 -1이면 좌우반전 됨. 

            transform.localScale = theScale; // 변경된 scale을 적용시킴
        }
    }

    //값 초기화 해주는 함수 ex) attack을 false로 바꿔서 작동 안하게 하는 등
    private void ResetValues()
    {
        attack = false;
        shoot = false;
        jump = false;
        jumpShoot = false;
    }

    private bool IsGrounded()
    {
        if (myRigidBody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                //다른 collider랑 겹치는 collider 만들기 -> 땅 위에 있는지의 유뮤를 알기 위함
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("land", false);
                        return true; // 땅과 닿으면 true 반환
                    }
                }//for
            }//foreach
        }//if
        return false;
    }//IsGrounded

    private void HandleLayers()
    {
        //공중에 있으면 airLayer의 Weight를 1로 만들어서 공중에서 해야할 애니매이션이 나오게 함
        if (!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    public void ShootBullet(int value)
    {
        //공중이거나 땅 위거나
        if (!isGrounded && value == 1 || isGrounded && value == 0)
        {
            if(facingRight)
            {
                //player의 포지션에서 생성되고, 회전값은 없음(Quaternion.identity)
                GameObject tmp = (GameObject)Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
                tmp.GetComponent<Bullet>().Initialize(Vector2.right);
            }
           else
            {
                GameObject tmp = (GameObject)Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
                tmp.GetComponent<Bullet>().Initialize(Vector2.left);
            }
        }
    }//shootBullet

    public void MeleeAttack()
    {
        SwordCollider.enabled = !SwordCollider.enabled;
    }

    //피격당했을때, Sprite Renderer의 color를 RGB(255,255,255 -> 92,92,92)로 깜빡이게 해보기
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "DeadLine" && !isCleared)
        {
            
            if (playerHP == 3) // 풀피였을때.
            {
                heart[2].SetActive(false);
                deleted_Heart[2].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());
            }
            else if (playerHP == 2) // 하트가 2칸있을때 맞았을때.
            {
                heart[1].SetActive(false);
                deleted_Heart[1].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());
            }
            else if (playerHP == 1) // 막피에 맞았을때.
            {
                heart[0].SetActive(false);
                deleted_Heart[0].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());

                if (!isDied && playerHP <= 0)
                { //소리 한번만 나게
                    SoundManager.PlaySound("playerDead");
                    myAnimator.SetTrigger("die");
                    isDied = true;
                    GameOverUI.SetActive(true);
                }
            }
        }

        if (other.gameObject.tag == "Spike" && !isCleared)
        {
            if (playerHP == 3) // 풀피였을때.
            {
                heart[2].SetActive(false);
                deleted_Heart[2].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());
            }
            else if (playerHP == 2) // 하트가 2칸있을때 맞았을때.
            {
                heart[1].SetActive(false);
                deleted_Heart[1].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());
            }
            else if (playerHP == 1) // 막피에 맞았을때.
            {
                heart[0].SetActive(false);
                deleted_Heart[0].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());

                if (!isDied && playerHP <= 0)
                { //소리 한번만 나게
                    SoundManager.PlaySound("playerDead");
                    myAnimator.SetTrigger("die");
                    isDied = true;
                    GameOverUI.SetActive(true);
                }
            }
        }

        if (other.gameObject.tag == "bullet" && !isCleared)
        {
            if (playerHP == 3) // 풀피였을때.
            {
                heart[2].SetActive(false);
                deleted_Heart[2].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());
            }
            else if (playerHP == 2) // 하트가 2칸있을때 맞았을때.
            {
                heart[1].SetActive(false);
                deleted_Heart[1].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());
            }
            else if (playerHP == 1) // 막피에 맞았을때.
            {
                heart[0].SetActive(false);
                deleted_Heart[0].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());

                if (!isDied && playerHP <= 0)
                { //소리 한번만 나게
                    SoundManager.PlaySound("playerDead");
                    myAnimator.SetTrigger("die");
                    isDied = true;
                    GameOverUI.SetActive(true);
                }
            }
        }

        if (other.gameObject.CompareTag("Enemy") && !isCleared)
        {
            if (playerHP == 3) // 풀피였을때.
            {
                heart[2].SetActive(false);
                deleted_Heart[2].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());
            }
            else if (playerHP == 2) // 하트가 2칸있을때 맞았을때.
            {
                heart[1].SetActive(false);
                deleted_Heart[1].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());
            }
            else if (playerHP == 1) // 막피에 맞았을때.
            {
                heart[0].SetActive(false);
                deleted_Heart[0].SetActive(true);
                playerHP--;
                StartCoroutine(HitPlayer());

                if (!isDied && playerHP <= 0)
                { //소리 한번만 나게
                    SoundManager.PlaySound("playerDead");
                    myAnimator.SetTrigger("die");
                    isDied = true;
                    GameOverUI.SetActive(true);
                }
            }
        }
    }

    IEnumerator HitPlayer()
    {   //캐릭터가 피격될때 깜빡임
        if (!isCleared)
        {
            mySpriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(0.33f);
            mySpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.33f);
            mySpriteRenderer.color = Color.gray;
            yield return new WaitForSeconds(0.33f);
            mySpriteRenderer.color = Color.white;
            yield return null;
        }
        else
        {
            yield break;
        }
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "YellowKey")
        {
            SoundManager.PlaySound("getKey");
            yKey = true;
            other.gameObject.SetActive(false);
        }
        
        if (other.tag == "BlueKey")
        {
            SoundManager.PlaySound("getKey");
            bKey = true;
            other.gameObject.SetActive(false);
        }

        if (other.tag == "GreenKey")
        {
            SoundManager.PlaySound("getKey");
            gKey = true;
            other.gameObject.SetActive(false);
        }

        if (other.tag == "SilverKey")
        {
            SoundManager.PlaySound("getKey");
            sKey = true;
            other.gameObject.SetActive(false);
        }

        if (other.tag == "Portal")
        {
            StartCoroutine(StageClear());
        }
    }

    IEnumerator StageClear()
    {
        StageClearMsg.SetActive(true);

        if(!isCleared)  //소리 한번만 나게
            SoundManager.PlaySound("StageClear");

        isCleared = true;
        yield return new WaitForSeconds(6.0f);

        StageClearMsg.SetActive(true);
        /*
        if (GameManager.instance.stage == 1)
        {
            GameManager.instance.stage++;
            isCleared = false;
            SceneManager.LoadScene("Stage2");
        }
        else if (GameManager.instance.stage == 2)
        {
            GameManager.instance.stage++;
            isCleared = false;
            SceneManager.LoadScene("Stage3");
        }
        else
        {
            //엔딩씬 보는 기능
            isCleared = false;
            SceneManager.LoadScene("AllClear");
        }
        */
        if (CurrentStage == 0)
        {
            CurrentStage++;
            isCleared = false;
            SceneManager.LoadScene("Stage2");
        }
        else if (CurrentStage == 1)
        {
            CurrentStage++;
            isCleared = false;
            SceneManager.LoadScene("Stage3");
        }
        else if (CurrentStage == 2)
        {
            //엔딩씬 보는 기능
            CurrentStage = 0;
            isCleared = false;
            SceneManager.LoadScene("AllClear");
        }
        yield return null;
    }
}

