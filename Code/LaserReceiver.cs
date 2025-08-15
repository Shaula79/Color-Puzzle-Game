using UnityEngine;

public class LaserReceiver : Button
{
    public SpriteRenderer spriteRenderer;
    public Color offColor;
    public Color onColor;

    public float timerMax;
    public float timer;

    void Start()
    {
    }

    void Update()
    {
        // decrements timer, turn off when timer hits 0
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 0;
            spriteRenderer.color = offColor;
            toggle = false;
        }
    }

    public void keepActive()
    {
        toggle = true;
        spriteRenderer.color = onColor;
        timer = timerMax;
    }
}
