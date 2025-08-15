using UnityEngine;

/* This script will be put on any item that the player can pick up and move around.
 * 
 * TODO: Items should come to rest on the ground, either with physics, or just instantly teleporting to the ground
 * the teleporting option is probably less janky but having some physics would be nice if we want to do puzzles with that.
*/
public class Pickup : MonoBehaviour
{
    public float pickupRange = 1f; // distance from which the player can pick up the object

    public PlayerController player; // pointer to the player

    public Rigidbody2D rb;

    private void Start()
    {
        // finds the player in the scene, assumes there is only one player object to work correctly
        player = FindFirstObjectByType<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // detecting conditions for being picked up
        if(Vector2.Distance(player.transform.position, transform.position) <= pickupRange)
        {
            if (!player.isHolding && Input.GetKeyDown(KeyCode.S) && player.holdTimer == 0)
            {

                player.isHolding = true;
                player.heldObject = gameObject;
                player.holdTimer = 0.1f;

                rb.linearVelocity = Vector2.zero;
                rb.simulated = false;

                transform.parent = player.transform;
                transform.localPosition = new Vector2(0, 1);
            }
        }
    }
}
