using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 8f;
    public float knockback = 10f;
    
    private bool facingRight;
    private GameObject owner;
    private Rigidbody2D rb;

    public void Setup(bool isFacingRight, GameObject fireballOwner)
    {
        facingRight = isFacingRight;
        owner = fireballOwner;
        
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();
        
        rb.gravityScale = 0f; 
        rb.linearVelocity = (facingRight ? Vector2.right : Vector2.left) * speed;
        
        Destroy(gameObject, 2f);
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject == owner) return;

        if (hit.CompareTag("Player"))
        {
            UniversalPlayerMovement enemy = hit.GetComponent<UniversalPlayerMovement>();
            
            if (enemy != null)
            {
                if (enemy.isReflecting)
                {
                    facingRight = !facingRight;
                    owner = hit.gameObject; 
                    
                    rb.linearVelocity = (facingRight ? Vector2.right : Vector2.left) * (speed * 1.5f);
                    damage *= 1.5f; 
                    
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                    
                    return; 
                }
                
              
                if (!enemy.isInvincible && !enemy.isShielding && !enemy.isDodging)
                {
                    enemy.TakeDamage(damage, knockback, facingRight, false); 
                    Destroy(gameObject); 
                }
                else if (enemy.isShielding)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (hit.CompareTag("Ground") || hit.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}