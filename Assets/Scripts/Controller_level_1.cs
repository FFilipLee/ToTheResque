using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Controller_level_1 : MonoBehaviour
{
    private bool onGround;
    private float jumpTimer;
    private bool isSprinting;

    [SerializeField]
    private float jumpExpectedTime = 3f;

    [SerializeField]
    private float movementSpeed = 2f;

    [SerializeField]
    private float sprintMultiplier = 2f; // Multiplier for sprinting speed

    [SerializeField]
    private float jumpForce = 5f; // Jump power, adjustable from the editor

    [SerializeField]
    private GameObject spriteObject; // Reference to the child GameObject with the Sprite Renderer

    [SerializeField]
    public TMP_Text textBox; 

    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    [Header("Sprite Transform Properties")]
    [SerializeField]
    private Vector3 spriteScale = new Vector3(2.2f, 1.6f, 2.2f); // Expose scale in Inspector

    [SerializeField]
    private Vector3 spritePosition = Vector3.zero; // Expose position in Inspector

    [SerializeField]
    private Vector3 spriteRotation = Vector3.zero; // Expose rotation in Inspector

    [SerializeField]
    private Vector3 teleportLocation = new Vector3(3f, 2f, 0f); // Where to teleport when character dies


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        float currentSpeed = moveInput * movementSpeed;

        Debug.Log("Current speed: " + currentSpeed);

        // Check if the player is sprinting and on the ground
        if (onGround && Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
            currentSpeed *= sprintMultiplier;
        }
        else if (onGround)
        {
            isSprinting = false;
        }

        // Flip the sprite based on horizontal movement direction
        if (moveInput != 0)
        {
            spriteObject.transform.localScale = new Vector3(spriteScale.x * (moveInput < 0 ? -1 : 1), spriteScale.y, spriteScale.z);
        }

        // Apply the horizontal velocity
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        // Jumping
        if (onGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            onGround = false;
            jumpTimer = Time.time;
        }

        // Maintain sprinting speed in the air if sprinting was initiated on the ground
        if (!onGround && isSprinting)
        {
            rb.velocity = new Vector2(moveInput * movementSpeed * sprintMultiplier, rb.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            transform.position = teleportLocation;
        }
        else if (collision.gameObject.CompareTag("Smash_it"))
        {
            if (isSprinting)
            {
                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Portal_level_2_pre_note"))
        {
            SceneManager.LoadScene("Level_2_pre_note");
        }
        else if (collision.gameObject.CompareTag("Portal_level_2"))
        {
            SceneManager.LoadScene("Level_2");
        }
        else if (collision.gameObject.CompareTag("Portal_level_1"))
        {
            SceneManager.LoadScene("Level_1");
        }
        else if (collision.gameObject.CompareTag("Normal_font_size"))
        {
            textBox.fontSize = 28;
        }
        else if (collision.gameObject.CompareTag("Large_font_size"))
        {
            textBox.fontSize = 36;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            onGround = false;
        }
    }
}
