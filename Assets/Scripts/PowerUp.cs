using System.Collections;
using UnityEngine;


public abstract class PowerUp : ScriptableObject
{
    public string id = "powerup";
    public string displayName = "Power Up";
    public float duration = 3f;
    public Sprite icon;

    public abstract IEnumerator Activate(PlayerPowerupController target);
}
