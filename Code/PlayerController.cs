using UnityEngine;


/* This class handles the control of the player, including interacting with objects and movement.
 */

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
 
    
    public float horizontal; // used to represent horizontal movement
    public float speed; // speed of the player
    public float jump; // jumping power


    public GameObject heldObject; // points to the object the player is currently holding
    public bool isHolding; // tells if the player is holding an object

    public CoinManager cm;

/* since the same key is used to pick up and drop the object, the player will instantly drop the object in one frame,
 * so this is a short timer that prevents the item from being dropped instantly*/
    public float holdTimer = 0.1f; 

    public static KeyCode interact = KeyCode.S; // key to be used to interact with objects

    void Update()
    {

        // movement
        horizontal = Input.GetAxisRaw("Horizontal");


        // player can only jump if they're on the ground
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jump);
        }

        // holding the jump button for longer lets the player jump higher
        if(Input.GetKeyUp(KeyCode.Space) && rb.linearVelocityY > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, rb.linearVelocityY * 0.5f);
        }


        // dropping pickup
        
        holdTimer -= Time.deltaTime;
        holdTimer = Mathf.Clamp(holdTimer, 0, 0.1f); // timer is kept between 0 and 0.1

        if (isHolding && Input.GetKeyDown(KeyCode.S) && holdTimer == 0)
        {
            holdTimer = 0.1f;
            heldObject.transform.SetParent(null);
            heldObject.GetComponent<Pickup>().rb.simulated = true;
            isHolding = false;
        }

    }

    // updates movement
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocityY);
    }

    // checks if the player is on the ground
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // coin collection
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
            
        }

        if(other.gameObject.name == "Scene Transition")
        {
            other.gameObject.GetComponent<SceneTransition>().Transition();
        }
    }
  







}

