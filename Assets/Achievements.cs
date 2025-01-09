using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    [Header("UI References")]
    public GameObject achievementsPanel; // Achievements panel
    public Transform contentParent; // Content in the Scroll View
    public GameObject achievementPrefab; // Prefab for an achievement (Image + Text)

    [Header("Achievement Images")]
    public Image achievementScoreImage;
    public Image achievementShopImage;
    public Image achievement500PointsImage;
    public Image achievementTier1Image;
    public Image achievementTier2Image;
    public Image achievementPassive50Image;
    public Image achievementPassive100Image;
    public Image achievementClickPower8Image;
    public Image achievement10kPointsImage;
    public Image achievement2000PointsImage; // New achievement image for 2000 points

    [Header("Achievement Conditions")]
    public float scoreThreshold = 50f;
    public int shopAmountThreshold = 2;
    public float totalPointsThreshold = 500f;
    public int tier1UpgradesThreshold = 5;
    public int tier2UpgradesThreshold = 3;
    public float passiveIncome50Threshold = 50f;
    public float passiveIncome100Threshold = 100f;
    public float clickPowerMultiplierThreshold = 8f;
    public float totalScoreThreshold = 10000f;
    public float score2000Threshold = 50000f; // New threshold for 2000 points

    // Unlock flags
    private bool achievementScoreUnlocked = false;
    private bool achievementShopUnlocked = false;
    private bool achievement500PointsUnlocked = false;
    private bool achievementTier1Unlocked = false;
    private bool achievementTier2Unlocked = false;
    private bool achievementPassive50Unlocked = false;
    private bool achievementPassive100Unlocked = false;
    private bool achievementClickPower8Unlocked = false;
    private bool achievement10kPointsUnlocked = false;
    private bool achievement2000PointsUnlocked = false; // New flag for 2000 points

    // Reference to the Game script
    private Game gameScript;

    void Start()
    {
        // Find the Game script
        gameScript = FindObjectOfType<Game>();
        if (gameScript == null)
        {
            Debug.LogError("Game script not found in the scene.");
        }

        // Disable the achievements panel by default
        achievementsPanel.SetActive(false);

        // Initialize achievement images as locked (dimmed)
        InitializeAchievementImages();

        // Populate achievements list in the UI
        PopulateAchievements();
    }

    void Update()
    {
        if (gameScript == null) return;

        // Check and unlock achievements based on conditions
        if (!achievementScoreUnlocked && gameScript.currentScore >= scoreThreshold)
        {
            achievementScoreUnlocked = true;
            UnlockAchievement(achievementScoreImage, "Scored 50 points!");
        }

        if (!achievementShopUnlocked && gameScript.amount1 >= shopAmountThreshold)
        {
            achievementShopUnlocked = true;
            UnlockAchievement(achievementShopImage, "Purchased 2 upgrades!");
        }

        if (!achievement500PointsUnlocked && gameScript.currentScore >= totalPointsThreshold)
        {
            achievement500PointsUnlocked = true;
            UnlockAchievement(achievement500PointsImage, "Reached 500 total points!");
        }

        if (!achievementTier1Unlocked && gameScript.amount1 >= tier1UpgradesThreshold)
        {
            achievementTier1Unlocked = true;
            UnlockAchievement(achievementTier1Image, "Bought 5 Tier 1 upgrades!");
        }

        if (!achievementTier2Unlocked && gameScript.amount2 >= tier2UpgradesThreshold)
        {
            achievementTier2Unlocked = true;
            UnlockAchievement(achievementTier2Image, "Bought 3 Tier 2 upgrades!");
        }

        if (!achievementPassive50Unlocked && gameScript.x >= passiveIncome50Threshold)
        {
            achievementPassive50Unlocked = true;
            UnlockAchievement(achievementPassive50Image, "Reached $50 passive income per second!");
        }

        if (!achievementPassive100Unlocked && gameScript.x >= passiveIncome100Threshold)
        {
            achievementPassive100Unlocked = true;
            UnlockAchievement(achievementPassive100Image, "Reached $100 passive income per second!");
        }

        if (!achievementClickPower8Unlocked && gameScript.hitPower >= clickPowerMultiplierThreshold)
        {
            achievementClickPower8Unlocked = true;
            UnlockAchievement(achievementClickPower8Image, "Multiplied click power by 8!");
        }

        if (!achievement10kPointsUnlocked && gameScript.currentScore >= totalScoreThreshold)
        {
            achievement10kPointsUnlocked = true;
            UnlockAchievement(achievement10kPointsImage, "Earned a total of 10,000 points!");
        }

        // New achievement for 2000 points
        if (!achievement2000PointsUnlocked && gameScript.currentScore >= score2000Threshold)
        {
            achievement2000PointsUnlocked = true;
            UnlockAchievement(achievement2000PointsImage, "Earned a total of 50,000 points!");
        }
    }

    /// <summary>
    /// Initialize all achievement images as locked (dimmed).
    /// </summary>
    private void InitializeAchievementImages()
    {
        SetImageLocked(achievementScoreImage);
        SetImageLocked(achievementShopImage);
        SetImageLocked(achievement500PointsImage);
        SetImageLocked(achievementTier1Image);
        SetImageLocked(achievementTier2Image);
        SetImageLocked(achievementPassive50Image);
        SetImageLocked(achievementPassive100Image);
        SetImageLocked(achievementClickPower8Image);
        SetImageLocked(achievement10kPointsImage);
        SetImageLocked(achievement2000PointsImage); // Initialize the new achievement image
    }

    /// <summary>
    /// Set an achievement image as locked (dimmed).
    /// </summary>
    /// <param name="image">The achievement image.</param>
    private void SetImageLocked(Image image)
    {
        if (image != null)
            image.color = new Color(0.2f, 0.2f, 0.2f, 0.2f);
    }

    /// <summary>
    /// Unlock an achievement and update its image.
    /// </summary>
    /// <param name="achievementImage">The achievement image to update.</param>
    /// <param name="message">The message to display when unlocked.</param>
    private void UnlockAchievement(Image achievementImage, string message)
    {
        if (achievementImage != null)
            achievementImage.color = new Color(1f, 1f, 1f, 1f);

        Debug.Log("Achievement unlocked: " + message);
    }

    /// <summary>
    /// Populate achievements list in the Scroll View.
    /// </summary>
    private void PopulateAchievements()
    {
        string[] achievementDescriptions = {
            "Score 50 points",
            "Purchase 2 upgrades",
            "Reach 500 total points",
            "Buy 5 Tier 1 upgrades",
            "Buy 3 Tier 2 upgrades",
            "Reach $50 passive income per second",
            "Reach $100 passive income per second",
            "Multiply your click power by 8",
            "Earn a total of 10,000 points",
            "Earn a total of 50,000 points" // New description
        };

        foreach (var description in achievementDescriptions)
        {
            GameObject newAchievement = Instantiate(achievementPrefab, contentParent);

            Text descriptionText = newAchievement.GetComponentInChildren<Text>();
            if (descriptionText != null)
            {
                descriptionText.text = description;
            }
        }
    }

    /// <summary>
    /// Show the achievements panel.
    /// </summary>
    public void ShowAchievements()
    {
        achievementsPanel.SetActive(true);
    }

    /// <summary>
    /// Close the achievements panel.
    /// </summary>
    public void CloseAchievements()
    {
        achievementsPanel.SetActive(false);
    }
}
