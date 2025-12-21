using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdEnemy : MonoBehaviour
{
    public float speed = 6f;
    [Header("Rewards")]
    public float energyOnDashKill = 20f;
    public float lifetime = 10f;

    void OnEnable() => Destroy(gameObject, lifetime);

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Bird triggered with: {other.tag} - {other.name}");

        if (!other.CompareTag("Player"))
        {
            Debug.Log($"Not a player, it's: {other.tag}");
            return;
        }

        var runner = other.GetComponent<PlayerRun2>();
        if (runner != null && runner.IsDashing)
        {
            Debug.Log("Player dashed through bird - awarding energy");
            var energy = other.GetComponent<PlayerEnergy>();
            if (energy)
            {
                energy.AddEnergy(energyOnDashKill);
                Debug.Log($"Added {energyOnDashKill} energy");
            }
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Player hit bird but not dashing - bird should damage player");
            // The PlayerRun2 script handles taking damage
        }
    }

    void OnBecameInvisible() => Destroy(gameObject);
}
