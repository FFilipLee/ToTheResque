using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_level_2 : MonoBehaviour
{
    private bool floating = true;

    [SerializeField]
    private float movementSpeed = 2f;

    [SerializeField]
    private float floating_gravity_multiplier = 2f;

    [SerializeField]
    private float maxVerticalSpeed = -2;

    [SerializeField]
    private GameObject spriteObject; // Reference to the child GameObject with the Sprite Renderer
    
    private Rigidbody2D rb;

    private SpriteRenderer spriteRenderer;

    [Header("Sprite Transform Properties")]
    [SerializeField]
    private Vector3 spriteScale = new Vector3(1, 1, 1); // Expose scale in Inspector

    [SerializeField]
    private Vector3 spritePosition = Vector3.zero; // Expose position in Inspector

    [SerializeField]
    private Vector3 spriteRotation = Vector3.zero; // Expose rotation in Inspector

    [SerializeField]
    private Vector3 teleportLocation = new Vector3(0, 2.8f, 0f); // Where to teleport when character dies

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rb.velocity.y);
    
        float moveInput = Input.GetAxis("Horizontal");
        float currentSpeed = moveInput * movementSpeed;

        if (moveInput != 0)
        {
            spriteObject.transform.localScale = new Vector3(spriteScale.x * (moveInput < 0 ? -1 : 1), spriteScale.y, spriteScale.z);
        }

        // Apply the horizontal velocity
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (floating)
            {
                rb.gravityScale *= floating_gravity_multiplier;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 1.5f);
            }
            else
            {
                rb.gravityScale /= floating_gravity_multiplier;
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 1.5f);
            }
            floating = !floating;

            Debug.Log("New Gravity Scale: " + rb.gravityScale);
        }

        if (rb.velocity.y < maxVerticalSpeed)
        {
          rb.velocity = new Vector2(rb.velocity.x, maxVerticalSpeed);
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            transform.position = teleportLocation;
        }
        else if (collision.gameObject.CompareTag("Smash_it"))
        {
            if (floating)
            {
                transform.position = teleportLocation;
                transform.rotation = new Quaternion(0,0,0,0);
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
            else
            {
                Destroy(collision.gameObject);
                transform.rotation = new Quaternion(0,0,0,0);
                rb.angularVelocity = 0;
            }
        }
        else if (collision.gameObject.CompareTag("Portal_end"))
        {
            SceneManager.LoadScene("Level_end");
        }
    }
}
