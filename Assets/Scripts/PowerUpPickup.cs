using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    public PowerUp powerUp;             
    public string playerTag = "Player";

    void Reset() => GetComponent<Collider2D>().isTrigger = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        var controller = other.GetComponent<PlayerPowerupController>();
        if (!controller) return;

        controller.ApplyPowerup(powerUp);
        Destroy(gameObject);
    }
}
