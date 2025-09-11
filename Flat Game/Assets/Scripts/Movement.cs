using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjustable speed in units per second
    private SpriteRenderer spriteRenderer;

    bool goLeft = true;
    bool goRight = true;
    bool goUp = true;
    bool goDown = true;

    AudioSource myCDPlayer;
    public AudioClip dingCD;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        myCDPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.A) && goLeft)
            moveX = -1f;
        if (Input.GetKey(KeyCode.D) && goRight)
            moveX = 1f;
        if (Input.GetKey(KeyCode.S) && goDown)
            moveY = -1f;
        if (Input.GetKey(KeyCode.W) && goUp)
            moveY = 1f;

        Vector3 moveDir = new Vector3(moveX, moveY, 0f).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if (moveX > 0)
            spriteRenderer.flipX = true; // Facing right
        else if (moveX < 0)
            spriteRenderer.flipX = false;  // Facing left
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("trigger hit");
        if (collision.CompareTag("Left"))
        {
            // Debug.Log("Hit left border");
            goLeft = false;
        }

        if (collision.CompareTag("Right"))
        {
            goRight = false;
        }

        if (collision.CompareTag("Top"))
        {
            goUp = false;
        }

        if (collision.CompareTag("Bottom"))
        {
            goDown = false;
        }

        if (collision.CompareTag("DeadFish")) // plays audio on collsion with fish
        {
            myCDPlayer.PlayOneShot(dingCD);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
        {
            goLeft = true;
        }

        if (collision.CompareTag("Right"))
        {
            goRight = true;
        }

        if (collision.CompareTag("Bottom"))
        {
            goDown = true;
        }

        if (collision.CompareTag("Top"))
        {
            goUp = true;
        }
    }

    
}
