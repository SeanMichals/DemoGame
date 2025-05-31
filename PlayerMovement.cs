using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float halfWidth;
    private float halfHeight;


    void Start()
{
    rb = GetComponent<Rigidbody2D>();

    // Get camera bounds
    Camera cam = Camera.main;
    Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
    Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

    minBounds = new Vector2(bottomLeft.x, bottomLeft.y);
    maxBounds = new Vector2(topRight.x, topRight.y);

    // Get player sprite size for offset
    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    if (sr != null)
    {
        Vector3 size = sr.bounds.size;
        halfWidth = size.x / 2;
        halfHeight = size.y / 2;
    }
}


    void Update()
    {
        // Get input
        movement.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        movement.y = Input.GetAxisRaw("Vertical");   // W/S or Up/Down
    }

    void FixedUpdate()
    {
        Vector2 newPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        // Clamp position to stay within camera view
        float clampedX = Mathf.Clamp(newPos.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(newPos.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        rb.MovePosition(new Vector2(clampedX, clampedY));

    }
}
