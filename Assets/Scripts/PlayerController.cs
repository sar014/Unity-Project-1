using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float jumpForce = 10;
    public float doubleJumpForce = 3;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;

    public bool jumpUsed = false;
    private Animator playerAnim;

    private AudioSource playerAudio;

    //Particle System
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem powerUpParticle;
    //SFX
    public AudioClip jumpSound;
    public AudioClip crashSound;

    //PowerUps
    public bool hasPowerUp = false;
    private BoxCollider disableBoxCollider;
    public GameObject powerUpIndicator;
    // Start is called before the first frame update
    void Start()
    {
        //Getting the rigidbody component from the player
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        //Getting the animator component 
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        disableBoxCollider = GetComponent<BoxCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            //ForceMode.impulse is part of 4 different types of force. In this case the force will be applied immediately
            playerRB.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            //helps in preventing double jump
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound,1.0f);
            jumpUsed = false;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && !isOnGround && !jumpUsed)
        {
            //ForceMode.impulse is part of 4 different types of force. In this case the force will be applied immediately
            playerRB.AddForce(Vector3.up*doubleJumpForce,ForceMode.Impulse);
            //helps in preventing double jump
            playerAnim.Play("Running_Jump",3,0f);
            playerAudio.PlayOneShot(jumpSound,1.0f);
            jumpUsed = true;
        }
    }

    //when the player comes back to the ground,it will enter the collsion area. Therefore, we change the var to true
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }

        else if (collision.gameObject.CompareTag("Obstacle") && !hasPowerUp)
        {
            gameOver = true;
            Debug.Log("Game Over");

            //Death Animation
            playerAnim.SetBool("Death_b",true);
            playerAnim.SetInteger("DeathType_int",1);

            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound,1.0f);
        }

        else if (collision.gameObject.CompareTag("Obstacle") && hasPowerUp)
        {
            //disableBoxCollider.enabled = false;
            Destroy(collision.gameObject);
            //StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerUpParticle.Play();
            powerUpIndicator.SetActive(true);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        disableBoxCollider.enabled = true;
        playerRB.useGravity = true;
        powerUpIndicator.SetActive(false);
    }
}
