using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
public float moveSpeed = 1;
    [SerializeField]
    float jumpSpeed = 1.0f;
    bool grounded = false;
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;

    public int health = 10;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnCollisionEnter2D(Collision2D collision)
     {
             string otherTag = collision.gameObject.tag;
        if(otherTag == "Enemy")
        {
            health -= 1;
            slider.value = health;
        }
        if(health == 1)
        {
           float moveX = Input.GetAxis("Horizontal");
        Vector2 velocity = rb.velocity;
        velocity.x = moveX * moveSpeed;
        rb.velocity = velocity;
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector2(0, 100 * jumpSpeed));
            grounded = false;
            animator.SetTrigger("jump");
        }
        if(rb.velocity.y < -0.1f && !grounded)
        {
            animator.SetTrigger("fall");
        }
        animator.SetFloat("xInput", moveX);
        animator.SetBool("grounded", grounded);
        if(moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
        }
        
           if(health <= 0)
            {
                Scene scene = SceneManager.GetActiveScene();
                 SceneManager.LoadScene(scene.name);

            }
            
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
      if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}
