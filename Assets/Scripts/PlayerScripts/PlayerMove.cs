using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] float jumpForce = 15f;
    [SerializeField] float yJumpReleaseValue = 2f;
    [SerializeField] PlayerGroundCheck groundCheck;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem rightRunParticles;
    [SerializeField] ParticleSystem leftRunParticles;
    [SerializeField] ParticleSystem jumpParticles;
    [SerializeField] ParticleSystem landRightParticles;
    [SerializeField] ParticleSystem landLeftParticles;

    [SerializeField] private float m_stepTimeModif;
    [SerializeField] AudioSource walkSounds;
    [SerializeField] AudioClip step1Sound;
    [SerializeField] AudioClip step2Sound;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] AudioSource landSound;
    private bool useStepSound1 = true; // Переключатель для звуков шагов
    public float stepInterval = 0.5f; // Интервал между звуками шагов
    private float stepTimer;
    private bool isPlayingParticles = false;
    private bool isFalling = false;
    private float horizontal;

    //public AudioSource jumpSound;

    private Rigidbody2D rb;
    private Vector3 defaultSize;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody2D>();
        groundCheck.gameObject.SetActive(true);
        defaultSize = transform.localScale;
    }

    void LandParticlePlay()
    {
        landSound.Play();
        if(transform.localScale.x == defaultSize.x)
            landRightParticles.Play();
        else if(transform.localScale.x == -defaultSize.x)
            landLeftParticles.Play();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        animator.SetBool("Run", horizontal != 0);
        animator.SetBool("isGrounded", groundCheck.isGroundedReal);
        animator.SetFloat("yVelosity", rb.velocity.y);
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        Flip(horizontal);
        
        if (horizontal != 0 && groundCheck.isGroundedKayot)
        {
            if (!isPlayingParticles)
            {
                if(horizontal > 0)
                {
                    rightRunParticles.Play();
                    isPlayingParticles = true;
                }
                else if(horizontal < 0)
                {
                    leftRunParticles.Play();
                    isPlayingParticles = true;
                }
            }
            
            stepTimer += Time.fixedDeltaTime * m_stepTimeModif;
            if (stepTimer >= stepInterval)
            {
                PlayStepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            if (isPlayingParticles)
            {
                rightRunParticles.Stop();
                leftRunParticles.Stop();
                isPlayingParticles = false;
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isGroundedKayot)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            jumpParticles.Play();
            jumpSound.Play();
            //jumpSound.Play();
        }
        
        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / yJumpReleaseValue);
        
        if (rb.velocity.y < 0 && !groundCheck.isGroundedKayot && isFalling)
        {
            animator.SetTrigger("Fall");
            isFalling = false;
        }
        
        if(groundCheck.isGroundedKayot)
            isFalling = true;
    }
    
    private void PlayStepSound()
    {
        if (walkSounds != null)
        {
            // Переключение между звуками шагов
            walkSounds.PlayOneShot(useStepSound1 ? step1Sound : step2Sound);
            useStepSound1 = !useStepSound1;
        }
    }
    
    private void Flip(float horizontal)
    {
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(defaultSize.x, defaultSize.y, defaultSize.z);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(-defaultSize.x, defaultSize.y, defaultSize.z);
        }
    }
}
