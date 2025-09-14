using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 2f; // Adjustable speed
    private SpriteRenderer spriteRenderer;

    bool goLeft = true;
    bool goRight = true;
    bool goUp = true;
    bool goDown = true;

    private AudioSource swimSource;
    private AudioSource sfxSource;
    private AudioSource villagerSource;
    private AudioSource pipeSource;

    public AudioClip dingCD;
    public AudioClip swimCD;
    public AudioClip villagerCD;
    public AudioClip pipeCD;

    public GameObject omarDialog; // Omar dialog
    public GameObject tifaDialog; // Tifa dialog

    public GameObject shorkDialog; // Shork dialog


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        swimSource = gameObject.AddComponent<AudioSource>();
        swimSource.loop = true;
        swimSource.clip = swimCD;

        villagerSource = gameObject.AddComponent<AudioSource>();
        villagerSource.loop = true;
        villagerSource.clip = villagerCD;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.loop = false;
        sfxSource.clip = dingCD;

        pipeSource = gameObject.AddComponent<AudioSource>();
        pipeSource.loop = false;
        pipeSource.clip = pipeCD;

        // Hide dialog at the start
        if (omarDialog != null)
            omarDialog.SetActive(false);

        if (tifaDialog != null)
            tifaDialog.SetActive(false); 

        if (shorkDialog != null)
            shorkDialog.SetActive(false); 
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

        // Flip sprite depending on direction
        if (moveX > 0)
            spriteRenderer.flipX = true; // Facing right
        else if (moveX < 0)
            spriteRenderer.flipX = false;  // Facing left

        // Swimming audio
        if (moveDir.magnitude > 0f) // moving
        {
            if (!swimSource.isPlaying)
            {
                swimSource.Play();
            }
        }
        else
        {
            if (swimSource.isPlaying)
            {
                swimSource.Stop();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
            goLeft = false;

        if (collision.CompareTag("Right"))
            goRight = false;

        if (collision.CompareTag("Top"))
            goUp = false;

        if (collision.CompareTag("Bottom"))
            goDown = false;

        if (collision.CompareTag("Glimbo"))
        {
            if (!sfxSource.isPlaying)
            {
                sfxSource.PlayOneShot(dingCD);
            }
        }

        if (collision.CompareTag("Omar"))
        {
            if (omarDialog != null)
                omarDialog.SetActive(true); // Make it appear

            if (!villagerSource.isPlaying)
            {
                villagerSource.PlayOneShot(villagerCD);
            }
        }

        if (collision.CompareTag("Tifa"))
        {
            if (tifaDialog != null)
                tifaDialog.SetActive(true);
        }

        if (collision.CompareTag("Shork"))
        {
            if (shorkDialog != null)
            {
                shorkDialog.SetActive(true);
                shorkDialog.transform.SetParent(collision.transform); // attach to Shork
                shorkDialog.transform.localPosition = new Vector3(5f, 3f, -1.5f);
            }
            
            if (!pipeSource.isPlaying)
            {
                pipeSource.PlayOneShot(pipeCD);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Left"))
            goLeft = true;

        if (collision.CompareTag("Right"))
            goRight = true;

        if (collision.CompareTag("Bottom"))
            goDown = true;

        if (collision.CompareTag("Top"))
            goUp = true;
    }
}
