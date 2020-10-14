using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Bool for if the game is over
    public bool gameOver = false;
    public float jumpForce = 15f;
    public float gravityModifier = 3f;

    // Sound Effects
    public AudioClip jumpSound;
    public AudioClip crashSound;

    // Reference to the players RigidBody
    private Rigidbody playerRB;
    // Reference to the players Animator
    private Animator playerAnim;
    // Reverece to the players Explosion
    private ParticleSystem explosionParticle;
    // Reference to the players dirt splatter
    private ParticleSystem dirtParticle;
    // Reference to the players audio source
    private AudioSource playerAudio;

    // Bool for if the player is on the ground or not
    private bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        // Get the reference to the RigidBody
        playerRB = GetComponent<Rigidbody>();
        // Get the reference to the Animator
        playerAnim = GetComponent<Animator>();
        // Get the reference to the explosions Particle system
        explosionParticle = GameObject.Find("Explosion").GetComponent<ParticleSystem>();
        // Get the reference to the dirt splatter Partice system
        dirtParticle = GameObject.Find("FX_DirtSplatter").GetComponent<ParticleSystem>();
        // Get the reference to the players audio source


        // Apply the gravity modifier
        Physics.gravity *= gravityModifier;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Add upwards force to the player when the space key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver) {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            // Start jump animation
            playerAnim.SetTrigger("Jump_trig");
            // Stop dirt particles in the air
            dirtParticle.Stop();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player is touching the ground turn isOnGround to true
        if (collision.gameObject.CompareTag("Ground")) {
            isOnGround = true;
            // Start the dirt particles again
            dirtParticle.Play();
        // If the player is touching a obstacle end the game
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            // Play Death Animation
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            // Stop the dirt when the player dies
            dirtParticle.Stop();
        }
    }
}
