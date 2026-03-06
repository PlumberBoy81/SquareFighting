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
        
        rb.gravityScale = 0f; // Keeps it flying straight horizontally
        rb.linearVelocity = (facingRight ? Vector2.right : Vector2.left) * speed;
        
        Destroy(gameObject, 2f); // Destroy it after 2 seconds so it doesn't fly forever
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        // Don't hit the person who threw it
        if (hit.gameObject == owner) return;

        // If it hits a player
        if (hit.CompareTag("Player"))
        {
            // FIXED: Variable type matches the GetComponent type
            UniversalPlayerMovement enemy = hit.GetComponent<UniversalPlayerMovement>();
            
            if (enemy != null)
            {
                // --- NEW: REFLECTOR LOGIC ---
                if (enemy.isReflecting)
                {
                    // Reverse the direction and make the reflector the new owner!
                    facingRight = !facingRight;
                    owner = hit.gameObject; 
                    
                    // Give it a 50% boost to speed and damage for reflecting it
                    rb.linearVelocity = (facingRight ? Vector2.right : Vector2.left) * (speed * 1.5f);
                    damage *= 1.5f; 
                    
                    // Flip the fireball sprite so it faces the new direction
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                    
                    return; // Exit early so the fireball isn't destroyed!
                }
                
              
                if (!enemy.isInvincible && !enemy.isShielding && !enemy.isDodging)
                {
                    enemy.TakeDamage(damage, knockback, facingRight, false); 
                    Destroy(gameObject); 
                }
                else if (enemy.isShielding)
                {
                    // If they are shielding, just destroy the fireball without dealing damage
                    Destroy(gameObject);
                }
            }
        }
        // If it hits a wall or the floor
        else if (hit.CompareTag("Ground") || hit.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }
    }
}