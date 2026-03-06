using UnityEngine;

public class ProjectileReflector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure your Fireball prefab has the tag "Projectile"
        if (other.CompareTag("Projectile"))
        {
            Rigidbody2D projRb = other.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                // Reverse the projectile's velocity
                projRb.linearVelocity = new Vector2(-projRb.linearVelocity.x, projRb.linearVelocity.y);
                
                // Flip the projectile's sprite to face the new direction
                SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
                if (sr != null) sr.flipX = !sr.flipX;
            }
        }
    }
}

