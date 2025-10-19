using UnityEngine;



public class PlayerRun2 : MonoBehaviour
{
    public float speed = 5f;
    public float acceleration = 1.2f;
    public float jumpHeight = 7f;

    // dash params
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;

    Rigidbody2D rb;
    bool canDash = true;
    bool isDashing = false;
    bool isGround = true;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Update()
    {
        if (isDashing) return;

        // accelerate run speed
        speed += acceleration * Time.deltaTime;

        // jump 
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
        }

        // start dash
        if (Input.GetKeyDown(KeyCode.S) && canDash)
            StartCoroutine(Dash());
    }

    void FixedUpdate()
    {
        if (isDashing) return;

        
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    void Jump()
    {
        var v = rb.velocity;
        v.y = jumpHeight;
        rb.velocity = v;
    }

    private System.Collections.IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        // dash in facing direction 
        rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * dashingPower, 0f);
        if (tr) tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        // end dash: restore gravity and clamp X speed back to run speed
        rb.gravityScale = originalGravity;
        rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * speed, rb.velocity.y);
        isDashing = false;
        if (tr) tr.emitting = false;

        // cooldown
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
            isGround = true;
    }
}


