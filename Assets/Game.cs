using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text scoreText;
    public GameObject upgradesPanel; // Panel contenant les options d'am�lioration
    public GameObject hitPowerPanel; // Panel d�di� au Hit Power
    public Text hitPowerText; // Affichage de la puissance actuelle
    public Text hitPowerCostText; // Affichage du co�t pour am�liorer le Hit Power

    private bool upgradesVisible = false;
    private bool hitPowerVisible = false;

    public float currentScore;
    public float hitPower;
    public float scoreIncreasePerSecond;
    public float x;

    public int shop1prize;
    public Text shop1text;

    public int shop2prize;
    public Text shop2text;

    public Text amount1Text;
    public int amount1;
    public float amount1Profit;

    public Text amount2Text;
    public int amount2;
    public float amount2Profit;

    public int upgradePrize;
    public Text upgradeText;

    public int allUpgradePrize;
    public Text allUpgradeText;

    public GameObject plusObject;
    public Text plusText;

    public float hitPowerUpgradeCost = 50f; // Co�t de l'am�lioration du Hit Power

    // Dernier objectif : variables
    private bool lastObjectiveAchieved = false; // Pour s'assurer qu'il ne se d�clenche qu'une fois
    public float lastObjectiveScore = 1000000f; // Score cible pour l'objectif

    void Start()
    {
        currentScore = 0;
        hitPower = 1;
        scoreIncreasePerSecond = 1;
        x = 0f;

        shop1prize = 25;
        shop2prize = 125;
        amount1 = 0;
        amount1Profit = 1;
        amount2 = 0;
        amount2Profit = 5;

        upgradePrize = 50;
        allUpgradePrize = 500;

        // D�sactiver les panneaux par d�faut
        upgradesPanel.SetActive(false);
        hitPowerPanel.SetActive(false);

        UpdateHitPowerUI();
    }

    void Update()
    {
        // Formater uniquement le currentScore avec les suffixes
        scoreText.text = FormatCurrentScore(currentScore) + "$";

        scoreIncreasePerSecond = x * Time.deltaTime;
        currentScore += scoreIncreasePerSecond;

        // Afficher les autres valeurs sans suffixes
        shop1text.text = "Tier 1: " + shop1prize + " $";
        shop2text.text = "Tier 2: " + shop2prize + " $";

        amount1Text.text = "Tier 1: " + amount1 + "  anvil: " + amount1Profit + "/s";
        amount2Text.text = "Tier 2: " + amount2 + "  anvil: " + amount2Profit + "/s";

        if (upgradesVisible)
        {
            upgradeText.text = "Cost: " + upgradePrize + " $";
            allUpgradeText.text = "Cost: " + allUpgradePrize + " $";
        }

        // V�rification du dernier objectif
        if (!lastObjectiveAchieved && currentScore >= lastObjectiveScore)
        {
            lastObjectiveAchieved = true; // Marquer comme atteint
            UnlockLastObjective();
        }
    }

    private string FormatCurrentScore(float number)
    {
        string[] suffixes = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp" };
        int magnitude = 0;

        while (number >= 1000 && magnitude < suffixes.Length - 1)
        {
            number /= 1000;
            magnitude++;
        }

        // Retourner le score format� avec un suffixe et une seule d�cimale
        return string.Format("{0:0.0}", number) + suffixes[magnitude];
    }

    public void ShowUpgrades()
    {
        upgradesVisible = true;
        upgradesPanel.SetActive(true);
    }

    public void CloseUpgrades()
    {
        upgradesVisible = false;
        upgradesPanel.SetActive(false);
    }

    public void ShowHitPowerPanel()
    {
        hitPowerVisible = true;
        hitPowerPanel.SetActive(true);
    }

    public void CloseHitPowerPanel()
    {
        hitPowerVisible = false;
        hitPowerPanel.SetActive(false);
    }

    public void IncreaseHitPower()
    {
        if (currentScore >= hitPowerUpgradeCost)
        {
            currentScore -= hitPowerUpgradeCost;
            hitPower *= 2; // Double la puissance du Hit Power
            hitPowerUpgradeCost *= 2; // Augmente le co�t pour la prochaine am�lioration

            UpdateHitPowerUI();
        }
        else
        {
            Debug.Log("Pas assez de score pour am�liorer le Hit Power !");
        }
    }

    private void UpdateHitPowerUI()
    {
        if (hitPowerText != null)
            hitPowerText.text = "Puissance actuelle : " + hitPower;

        if (hitPowerCostText != null)
            hitPowerCostText.text = "Co�t : " + hitPowerUpgradeCost + " $";
    }

    public void Hit()
    {
        currentScore += hitPower;

        plusObject.SetActive(false);

        plusObject.transform.position = new Vector3(Random.Range(800, 1200 + 1), Random.Range(600, 700 + 1), 0);

        plusObject.SetActive(true);

        StopAllCoroutines();
        StartCoroutine(Fly());
    }

    public void Shop1()
    {
        if (currentScore >= shop1prize)
        {
            currentScore -= shop1prize;
            amount1 += 1;
            amount1Profit += 1;
            x += 1;
            shop1prize += 25;
        }
    }

    public void Shop2()
    {
        if (currentScore >= shop2prize)
        {
            currentScore -= shop2prize;
            amount2 += 1;
            amount2Profit += 5;
            x += 5;
            shop2prize += 125;
        }
    }

    public void Upgrade()
    {
        if (currentScore >= upgradePrize)
        {
            currentScore -= upgradePrize;
            hitPower *= 2;
            upgradePrize *= 3;
        }
    }

    public void AllProfitsUpgrade()
    {
        if (currentScore >= allUpgradePrize)
        {
            currentScore -= allUpgradePrize;
            x *= 2;
            allUpgradePrize *= 3;
            amount1Profit *= 2;
            amount2Profit *= 2;
        }
    }

    IEnumerator Fly()
    {
        for (int i = 0; i < 19; i++)
        {
            yield return new WaitForSeconds(0.01f);
            plusObject.transform.position = new Vector3(plusObject.transform.position.x, plusObject.transform.position.y + 2, 0);
        }
        plusObject.SetActive(false);
    }

    /// <summary>
    /// D�bloque le dernier objectif et affiche un message de f�licitations.
    /// </summary>
    private void UnlockLastObjective()
    {
        Debug.Log("Congratulations! You've achieved the final objective: 1 million score!");
        // Ajouter ici une notification visuelle ou une r�compense
    }
}
