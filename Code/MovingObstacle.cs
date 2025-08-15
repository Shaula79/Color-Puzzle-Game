using UnityEngine;
//using static UnityEditor.Searcher.SearcherWindow.Alignment;

/*
 * TODO: player currently doesn't stay with the platform if sitting on top of it, this might need to be fixed
 * but also if it isn't used in any puzzles then it doesn't matter
 */

public class MovingObstacle : MonoBehaviour
{

    public Vector2 startPos;
    public Vector2 endPos;
    public float speed;

    public Button input;

    Vector2 moveDirection;
    float fixedDistance;
    float moveDistance;

    private void Start()
    {
        moveDirection = (endPos - startPos).normalized;
        fixedDistance = Vector2.Distance(startPos, endPos);
        moveDistance = 0;
    }

    void Update()
    {
        if (input.toggle && moveDistance < fixedDistance)
        {
            transform.Translate(moveDirection * speed * Time.deltaTime);
            moveDistance += speed * Time.deltaTime;
        } 
        else if (!input.toggle && moveDistance > 0)
        {
            transform.Translate(-moveDirection * speed * Time.deltaTime);
            moveDistance -= speed * Time.deltaTime;
        }

        if(moveDistance > fixedDistance)
        {
            moveDistance = fixedDistance;
            transform.position = endPos;
        }
        else if (moveDistance < 0)
        {
            moveDistance = 0;
            transform.position = startPos;
        }
    }
}
