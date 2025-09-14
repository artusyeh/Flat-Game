using UnityEngine;

public class LeggoMover : MonoBehaviour
{
    public float leftX = -25f;   //left boundary
    public float rightX = 25f;   //right boundary
    public float speed = 0.2f;    //movement speed

    private bool movingRight = true;

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;


            if (transform.position.x >= rightX)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

  
            if (transform.position.x <= leftX)
            {
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
