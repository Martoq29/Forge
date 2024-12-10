using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text scoreText;
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

    // Start is called before the first frame update
    void Start()
    {
        currentScore =0;
        hitPower = 1;
        scoreIncreasePerSecond = 1;
        x = 0f;

        shop1prize = 25;
        shop2prize = 125;
        amount1 = 0;
        amount1Profit = 1;
        amount2 = 0;
        amount2Profit = 5;

        currentScore = PlayerPrefs.GetInt("currentScore", 0);
        hitPower = PlayerPrefs.GetInt("hitPower", 1);
        x = PlayerPrefs.GetInt("x", 0);

        shop1prize = PlayerPrefs.GetInt("shop1prize", 25);
        shop2prize = PlayerPrefs.GetInt("shop2prize", 125);
        amount1 = PlayerPrefs.GetInt("amount1", 0);
        amount1Profit = PlayerPrefs.GetInt("amount1Profit", 0);
        amount2 = PlayerPrefs.GetInt("amount2", 0);
        amount2Profit = PlayerPrefs.GetInt("amount2Profit", 0);
        upgradePrize = PlayerPrefs.GetInt("upgradePrize", 50);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = (int)currentScore + "$";
        scoreIncreasePerSecond = x * Time.deltaTime;
        currentScore = currentScore + scoreIncreasePerSecond;

        shop1text.text = "Tier 1: " + shop1prize + " $";
        shop2text.text = "Tier 2: " + shop2prize + " $";

        amount1Text.text = "Tier 1: "+amount1+" arts $: "+amount1Profit+"/s";
        amount2Text.text = "Tier 2: " + amount2 + " arts $: " + amount2Profit + "/s";

        upgradeText.text = "Cost: " + upgradePrize + " $";

        PlayerPrefs.SetInt("currentScore", (int)currentScore);
        PlayerPrefs.SetInt("hitPower", (int)hitPower);
        PlayerPrefs.SetInt("x", (int)x);

        PlayerPrefs.SetInt("shop1prize", (int)shop1prize);
        PlayerPrefs.SetInt("shop2prize", (int)shop2prize);
        PlayerPrefs.SetInt("amount1", (int)amount1);
        PlayerPrefs.SetInt("amount1Profit", (int)amount1Profit);
        PlayerPrefs.SetInt("amount2", (int)amount2);
        PlayerPrefs.SetInt("amount2Profit", (int)amount2Profit);
        PlayerPrefs.SetInt("upgradePrize", (int)upgradePrize);
    }

    public void Hit()
    {
        currentScore += hitPower;
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
        if(currentScore >= upgradePrize)
        {
            currentScore -= upgradePrize;
            hitPower *= 2;
            upgradePrize *= 3;
        }
    }
}
