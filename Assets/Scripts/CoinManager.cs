using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public TMP_Text coinText;
    public int coinCount;

    void Start() => UpdateUI();

    public void AddFood(int amount)
    {
        coinCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        coinText.text = "Apples collected:" + coinCount;
    }


}
