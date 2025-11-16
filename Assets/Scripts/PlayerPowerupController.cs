using UnityEngine;

public class PlayerPowerupController : MonoBehaviour
{
    public PlayerRun2 Runner { get; private set; }

    void Awake() => Runner = GetComponent<PlayerRun2>();

    public void ApplyPowerup(PowerUp powerUp)
    {
        if (powerUp != null) StartCoroutine(powerUp.Activate(this));
    }

    // optional UI hooks
    public void ShowPowerUp(PowerUp p) { /* TODO: icon/SFX */ }
    public void HidePowerUp(PowerUp p) { /* */ }
}
