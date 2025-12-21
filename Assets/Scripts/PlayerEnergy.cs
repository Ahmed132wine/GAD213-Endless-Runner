using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEnergy : MonoBehaviour
{
    [Header("Energy")]
    public float maxEnergy = 100f;
    [Tooltip("Energy used as the run goes on (per second).")]
    public float drainPerSecond = 3f;
    [Tooltip("Optional: restart the scene when energy hits zero.")]
    public bool restartOnZero = true;

    [HideInInspector] public float currentEnergy;

    void Awake()
    {
        currentEnergy = maxEnergy;
    }


    public void AddEnergy(float amount)
    {
        if (amount <= 0f) return;
        currentEnergy = Mathf.Clamp(currentEnergy + amount, 0f, maxEnergy);
    }

    void Update()
    {
        Drain(Time.deltaTime * drainPerSecond);
    }
    public void Drain(float amount)
    {
        if (amount <= 0f) return;
        currentEnergy = Mathf.Clamp(currentEnergy - amount, 0f, maxEnergy);

        if (currentEnergy <= 0f && restartOnZero)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public float Normalized => maxEnergy <= 0f ? 0f : currentEnergy / maxEnergy;
}
