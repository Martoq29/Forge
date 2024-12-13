using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GachaSystem : MonoBehaviour
{
    public Game GameScript; // Référence au script du jeu (Game.cs)

    public Text gachaResultText;
    public Text gachaCostText;
    public GameObject gachaPanel;

    private int gachaCost = 100; // Coût initial d'un tirage de gacha
    private string[] possibleRewards = { "Bonus x2 Score", "Increase Profit", "Anvil Power", "Double Hit Power", "Random Bonus" };
    private float bonusMultiplier = 1.5f; // Exemple de multiplicateur de score

    void Start()
    {
        gachaPanel.SetActive(false);
        UpdateGachaCostText();
    }

    void Update()
    {
        // Mettre à jour le texte avec le coût actuel du gacha
        UpdateGachaCostText();
    }

    private void UpdateGachaCostText()
    {
        gachaCostText.text = "Gacha Cost: " + gachaCost + " $";
    }

    public void ShowGachaPanel()
    {
        gachaPanel.SetActive(true);
    }

    public void CloseGachaPanel()
    {
        gachaPanel.SetActive(false);
    }

    public void RollGacha()
    {
        if (GameScript.currentScore >= gachaCost)
        {
            // Deduct score for the roll
            GameScript.currentScore -= gachaCost;

            // Get a random reward
            string reward = GetRandomReward();

            // Apply the reward based on the result
            ApplyReward(reward);

            // Update the gacha cost for the next roll
            gachaCost = Mathf.CeilToInt(gachaCost * 1.2f); // Augmenter le coût du tirage à chaque fois
        }
        else
        {
            gachaResultText.text = "Not enough money!";
        }
    }

    private string GetRandomReward()
    {
        int index = Random.Range(0, possibleRewards.Length);
        return possibleRewards[index];
    }

    private void ApplyReward(string reward)
    {
        switch (reward)
        {
            case "Bonus x2 Score":
                // Applique un multiplicateur de score pour une période temporaire
                StartCoroutine(ApplyScoreBonus());
                break;
            case "Increase Profit":
                // Augmenter le profit des "tiers"
                GameScript.amount1Profit *= 2;
                GameScript.amount2Profit *= 2;
                break;
            case "Anvil Power":
                // Augmenter la puissance du marteau
                GameScript.hitPower *= 1.5f;
                break;
            case "Double Hit Power":
                // Double la puissance de frappe immédiatement
                GameScript.hitPower *= 2;
                break;
            case "Random Bonus":
                // Applique un bonus aléatoire à un attribut
                ApplyRandomBonus();
                break;
        }

        gachaResultText.text = "You got: " + reward;
    }

    private IEnumerator ApplyScoreBonus()
    {
        float originalMultiplier = GameScript.scoreIncreasePerSecond;
        GameScript.scoreIncreasePerSecond *= bonusMultiplier;

        // Appliquer le bonus pour 10 secondes
        yield return new WaitForSeconds(10);

        // Rétablir la valeur originale
        GameScript.scoreIncreasePerSecond = originalMultiplier;
    }

    private void ApplyRandomBonus()
    {
        // Choisir un bonus au hasard parmi différents attributs
        int bonusType = Random.Range(0, 3);

        switch (bonusType)
        {
            case 0:
                // Augmenter les profits du tiers 1
                GameScript.amount1Profit *= 2;
                break;
            case 1:
                // Augmenter les profits du tiers 2
                GameScript.amount2Profit *= 2;
                break;
            case 2:
                // Ajouter un bonus temporaire de score
                StartCoroutine(ApplyScoreBonus());
                break;
        }
    }
}
