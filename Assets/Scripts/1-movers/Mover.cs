using UnityEngine;

/**
 * This component moves its object in a fixed velocity.
 * NOTE: velocity is defined as speed+direction.
 *       speed is a number; velocity is a vector.
 */
public class Mover: MonoBehaviour {
    [Tooltip("Movement vector in meters per second")]
    [SerializeField] Vector3 velocity;

    void Update() {
        transform.position += velocity * Time.deltaTime;

        // If object is out of screen bounds - delete
        if (transform.position.y <= -7)
        {
            Destroy(this.gameObject);
        }
        else if (transform.position.y >= 10)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetVelocity(Vector3 newVelocity) {
        this.velocity = newVelocity;
    }
}
