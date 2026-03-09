using UnityEngine;

public class ProjectileReflector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Rigidbody2D projRb = other.GetComponent<Rigidbody2D>();
            if (projRb != null)
            {
                projRb.linearVelocity = new Vector2(-projRb.linearVelocity.x, projRb.linearVelocity.y);                
                SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
                if (sr != null) sr.flipX = !sr.flipX;
            }
        }
    }
}

