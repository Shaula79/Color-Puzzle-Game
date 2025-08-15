using UnityEngine;

public class StandButton : Button
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public SpriteRenderer spriteRenderer;
    public Color offColor;
    public Color onColor;

    private int objectCounter;
    void Start()
    {
        objectCounter = 0;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //active when object enters collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectCounter++;
        if (objectCounter > 0)
        {
            toggle = true;
            spriteRenderer.color = onColor;
        }
    }

    // deactives when all objects exit
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectCounter--;
        if (objectCounter <= 0)
        {
            objectCounter = 0;
            toggle = false;
            spriteRenderer.color = offColor;
        }
    }


}
