using UnityEngine;

public class PressButton : Button
{
    public float pressRange = 1f;
    public PlayerController player; // pointer to the player

    public SpriteRenderer spriteRenderer;
    public Color offColor;
    public Color onColor;

    public bool holdToggle;


    void Start()
    {
        // finds the player in the scene, assumes there is only one player object to work correctly
        player = FindFirstObjectByType<PlayerController>();
    }

    void Update()
    {
        // checks if player is in distance of button
        if (Vector2.Distance(player.transform.position, transform.position) <= pressRange)
        {
            // option that requires the player to hold the button down to keep active
            if (holdToggle)
            {
                if (Input.GetKey(KeyCode.S))
                {
                    toggle = true;
                }
                else
                {
                    toggle = false;
                }
            } else // button will stay on when pressed once
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    toggle = !toggle;
                }
            }
        } else if (holdToggle)
        {
            toggle = false;
        }

        // changing color when active
        if (toggle)
        {
            spriteRenderer.color = onColor;
        } else
        {
            spriteRenderer.color = offColor;
        }
    }
}
