using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EnergyUI : MonoBehaviour
{
    public PlayerEnergy playerEnergy;
    public Image fillImage;
    public TextMeshProUGUI energyText;

    [Header("Debug")]
    public bool showDebugLogs = true;
    public bool showOnScreenDebug = true;

    void Start()
    {
        
        if (playerEnergy == null)
        {
            playerEnergy = FindObjectOfType<PlayerEnergy>();

            if (playerEnergy == null)
            {
                Debug.LogWarning("EnergyUI: No PlayerEnergy found in scene!");
                enabled = false; 
                return;
            }
        }
        if (fillImage == null)
        {
            fillImage = GetComponentInChildren<Image>();
            if (showDebugLogs) Debug.Log($"Auto-found Image: {fillImage != null}");
        }

        if (energyText == null)
        {
            energyText = GetComponentInChildren<TextMeshProUGUI>();
            if (showDebugLogs) Debug.Log($"Auto-found Text: {energyText != null}");
        }
        
        if (showDebugLogs)
        {
            Debug.Log($"=== EnergyUI Initialized ===");
            Debug.Log($"PlayerEnergy: {(playerEnergy != null ? "Found" : "MISSING")}");
            Debug.Log($"Fill Image: {(fillImage != null ? "Found" : "MISSING")}");
            Debug.Log($"Energy Text: {(energyText != null ? "Found" : "MISSING")}");
        }

       
    }

    void LateUpdate()
    {
        if (playerEnergy == null || fillImage == null) return;

        // Update fill bar
        float normalizedEnergy = playerEnergy.Normalized;
        fillImage.fillAmount = normalizedEnergy;

        // Update text if available
        if (energyText != null)
        {
            energyText.text = $"{playerEnergy.currentEnergy:F0}/{playerEnergy.maxEnergy:F0}";
        }

        // Debug in console every second
        if (showDebugLogs && Time.frameCount % 60 == 0) // Every ~second at 60fps
        {
            Debug.Log($"Energy: {playerEnergy.currentEnergy:F1}/{playerEnergy.maxEnergy} ({normalizedEnergy * 100:F1}%)");
        }
    }

    void OnGUI()
    {
        if (!showOnScreenDebug || playerEnergy == null) return;

        // Show energy values directly on screen (overrides everything)
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.white;

        string energyInfo = $"ENERGY: {playerEnergy.currentEnergy:F1}/{playerEnergy.maxEnergy} ({playerEnergy.Normalized * 100:F1}%)";

        // Add color coding
        if (playerEnergy.Normalized < 0.3f)
            style.normal.textColor = Color.red;
        else if (playerEnergy.Normalized < 0.6f)
            style.normal.textColor = Color.yellow;

        GUI.Label(new Rect(20, 20, 400, 30), energyInfo, style);

        // Show PlayerEnergy component status
        GUI.Label(new Rect(20, 50, 400, 30),
                 $"PlayerEnergy Found: {playerEnergy != null}, Enabled: {playerEnergy.enabled}", style);
    }



    public Color fullColor = Color.green;
    public Color lowColor = Color.red;
    public float lowEnergyThreshold = 0.3f; // 30%

    void UpdateEnergyColor()
    {
        if (fillImage == null) return;

        float energyPercent = playerEnergy.Normalized;

        if (energyPercent <= lowEnergyThreshold)
        {
            // Flash red when energy is low
            fillImage.color = Color.Lerp(Color.white, lowColor, Mathf.PingPong(Time.time * 2f, 1f));
        }
        else
        {
            // Gradient from red to green
            fillImage.color = Color.Lerp(lowColor, fullColor, energyPercent);
        }
    }
}
