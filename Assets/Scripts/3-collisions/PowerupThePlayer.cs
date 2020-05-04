using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupThePlayer : MonoBehaviour
{
    [Tooltip("The number of seconds that the powerup remains active")] [SerializeField] float duration;
    KeyboardSpawner player;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Powerup triggered by player");
            player = other.transform.GetComponent<KeyboardSpawner>();
            if (player != null)
            {
                player.PowerupActive(true);
            }
            var destroyComponent = other.GetComponent<DestroyOnTrigger2D>();
            if (destroyComponent)
            {
                destroyComponent.StartCoroutine(ShieldTemporarily(destroyComponent));
                // NOTE: If you just call "StartCoroutine", then it will not work, 
                //       since the present object is destroyed!
                Destroy(gameObject);  // Destroy the shield itself - prevent double-use
            }
        }
        else
        {
            Debug.Log("Powerup triggered by " + other.name);
        }
    }

    private IEnumerator ShieldTemporarily(DestroyOnTrigger2D destroyComponent)
    {
        destroyComponent.enabled = false;
        for (float i = duration; i > 0; i--)
        {
            Debug.Log("Powerup: " + i + " seconds remaining!");
            yield return new WaitForSeconds(1);
        }
        if (player != null)
        {
            player.PowerupActive(false);
        }
        Debug.Log("Powerup is gone!");
        destroyComponent.enabled = true;
    }
}
