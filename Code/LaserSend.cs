using Unity.VisualScripting;
using UnityEngine;

public class LaserSend : Button
{
    public LineRenderer lineRenderer;
    public GameObject emissionPoint;

    public SpriteRenderer spriteRenderer;
    public Color offColor;
    public Color onColor;

    public bool alwaysOn;
    public bool laserToggle;
    public bool inverted;

    public float timerMax;
    public float timer;

    public bool rotation;
    public float rotationSpeed;
    public Button rotateRightInput;
    public Button rotateLeftInput;
    public Button toggleButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void FixedUpdate()
    {
        // checks if the laser has a button to toggle it on and off
        if (toggleButton != null) {
            if (toggleButton.toggle)
            {
                alwaysOn = true;
            } else
            {
                alwaysOn = false;
            }
        }

        // handling button rotation
        if (rotation)
        {
            if (rotateRightInput.toggle)
            {
                transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
            }

            if (rotateLeftInput.toggle)
            {
                transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
            }
        }

        // handling beam inversion
        if (!alwaysOn)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (!inverted)
                {
                    timer = 0;
                    laserToggle = false;
                    toggle = false;
                    spriteRenderer.color = offColor;
                    lineRenderer.enabled = false;
                } else
                {
                    laserToggle = true;
                    toggle = true;
                    spriteRenderer.color = onColor;
                    timer = timerMax;
                }
            }
        } else if (!inverted)
        {
            laserToggle = true;
            toggle = true;
        }

        // sending out beam
        if (laserToggle)
        {
            spriteRenderer.color = onColor;

            RaycastHit2D hit = Physics2D.Raycast(emissionPoint.transform.position, emissionPoint.transform.up);

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            if (hit)
            {
                // hit is a laser receiver
                if (hit.collider.gameObject.GetComponent<LaserReceiver>() != null)
                {
                    hit.collider.gameObject.GetComponent<LaserReceiver>().keepActive();
                }

                // his is another laser
                if (hit.collider.gameObject.GetComponent<LaserSend>() != null)
                {
                    hit.collider.gameObject.GetComponent<LaserSend>().keepActive();
                }
            }
        }
        else
        {
            spriteRenderer.color = offColor;
        }
    }

    // handling the active state of the laser
    public void keepActive()
    {
        if (!inverted)
        {
            laserToggle = true;
            toggle = true;
            spriteRenderer.color = onColor;
            timer = timerMax;
        } else
        {
            laserToggle = false;
            toggle = false;
            spriteRenderer.color = offColor;
            lineRenderer.enabled = false;
            timer = timerMax;
        }
    }
}
