using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerRun : MonoBehaviour
{
    public float speed = 5f;
    public float acceleration = 1.2f;
    public float jumpHeight = 7f;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    
    [SerializeField] private TrailRenderer tr;
    
    private Rigidbody2D rb;
    private bool isGround = true;
    
   
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>(); 
    }

   
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        speed += acceleration * Time.deltaTime;
        transform.Translate(new Vector2(1f, 0f) * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            Jump();
            isGround = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
    }

    void Jump()
    {
        Debug.Log("Player Jump");
        Vector2 velocity = rb.velocity;
        velocity.y = jumpHeight;
        rb.velocity = velocity;
    }

    void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            isGround = true;
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
