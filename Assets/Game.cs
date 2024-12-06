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

    // Start is called before the first frame update
    void Start()
    {
        currentScore =0;
        hitPower = 1;
        scoreIncreasePerSecond = 1;
        x = 0f;

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
}
