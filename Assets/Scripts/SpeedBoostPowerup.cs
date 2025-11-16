using UnityEngine;
using System.Collections;
[CreateAssetMenu(menuName = "PowerUps/Speed Boost", fileName = "PU_SpeedBoost")]
public class SpeedBoostPowerup : PowerUp
{
    [Range(1f, 5f)] public float multiplier = 1.5f;
    public AudioClip sfx;

    public override IEnumerator Activate(PlayerPowerupController target)
    {
        target.ShowPowerUp(this);
        if (sfx) AudioSource.PlayClipAtPoint(sfx, target.transform.position);

        target.Runner.speedMultiplier *= multiplier;
        yield return new WaitForSeconds(duration);
        target.Runner.speedMultiplier /= multiplier;

        target.HidePowerUp(this);
    }
}
