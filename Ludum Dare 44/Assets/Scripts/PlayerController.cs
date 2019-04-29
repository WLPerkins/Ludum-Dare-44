using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Vector2 moveVector;

    private Rigidbody2D rb;

    private bool facingRight = true;


    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    public bool isGoal = false;
    public LayerMask whatIsGoal;

    private int extraJumps;
    public int extraJumpValue;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;


    private Animator anim;
    public Animator camAnim;

    public GameObject lEye;
    public GameObject rEye;
    public Sprite halfCircleSprite;

    public GameObject rFoot;
    public GameObject lFoot;
    public GameObject severedFoot;

    private AudioSource[] audios;
    private bool goalAudioPlayed;


    private Vector3 m_Velocity = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private void Start()
    {
        audios = GetComponents<AudioSource>();
        extraJumpValue = GameManager.instance.extraJumpsForPlayer;
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        camAnim = GameObject.Find("Cam").GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isGoal = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGoal);

        if (isGoal)
        {

            if (!goalAudioPlayed)
            {
                audios[2].Play();
                goalAudioPlayed = true;
            }

            Debug.Log("Initiate text and load next scene.");
        }

        if (isGrounded && extraJumps > GameManager.instance.extraJumpsForPlayer - 2)
        {
            moveVector.x = Input.GetAxis("Horizontal");
        }

        else
        {
            if (moveVector.x > .0125f)
            {
                moveVector.x -= .25f * Time.fixedDeltaTime;
            } else if (moveVector.x < -.0125f)
            {
                moveVector.x += .25f * Time.fixedDeltaTime;
            } else if (moveVector.x < .0125f && moveVector.x > -.0125f)
            {
                moveVector.x = 0;
            }
        }
        
        
        Vector3 targetVelocity = new Vector2(moveVector.x * speed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);


        //rb.velocity = new Vector2 (moveVector.x * speed, rb.velocity.y);
        //rb.AddForce(moveVector * speed);

        if(facingRight == false && moveVector.x > 0)
        {
            Flip();
        } else if(facingRight == true && moveVector.x < 0)
        {
            Flip();
        }

        ///stuff that was previously in Update...

        

    }

    private void Update()
    {
        if (isGrounded)
        {
            extraJumps = extraJumpValue;
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0 && GameManager.instance.deathAllowed == true)
        {
            audios[0].Play();
            anim.SetTrigger("takeOff");

            if (extraJumps == GameManager.instance.extraJumpsForPlayer && !isGrounded)
            {
                audios[1].Play();
                GameObject sevFoot = Instantiate(severedFoot, rFoot.transform.transform.position, Quaternion.identity);
                sevFoot.GetComponent<Rigidbody2D>().AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
                rFoot.GetComponent<SpriteRenderer>().enabled = false;
                extraJumpValue--;
                extraJumps--;
                rEye.GetComponent<SpriteRenderer>().sprite = halfCircleSprite;
                lEye.GetComponent<SpriteRenderer>().sprite = halfCircleSprite;
                camAnim.SetTrigger("cameraShake");
            }
            else if (extraJumps == GameManager.instance.extraJumpsForPlayer -1 && !isGrounded)
            {
                audios[1].Play();
                GameObject sevFoot = Instantiate(severedFoot, lFoot.transform.transform.position, Quaternion.identity);
                sevFoot.GetComponent<Rigidbody2D>().AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
                lFoot.GetComponent<SpriteRenderer>().enabled = false;
                extraJumpValue--;
                extraJumps--;
                rEye.GetComponent<SpriteRenderer>().enabled = false;
                lEye.GetComponent<SpriteRenderer>().enabled = false;
                camAnim.SetTrigger("cameraShake");
            }

            isJumping = true;
            moveVector.x = Input.GetAxis("Horizontal");
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        else if (Input.GetButtonDown("Jump") && extraJumps > GameManager.instance.extraJumpsForPlayer - 2 && isGrounded)
        {
            audios[0].Play();
            anim.SetTrigger("takeOff");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.AddForce(Vector2.up * jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }

        if (extraJumps == GameManager.instance.extraJumpsForPlayer - 2)
        {
            anim.enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            rb.freezeRotation = false;
            checkRadius = 1f;
        }

        //AnimationChecks

        if (moveVector.x == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
