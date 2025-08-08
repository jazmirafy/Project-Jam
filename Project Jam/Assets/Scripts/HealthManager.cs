using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthManager : MonoBehaviour
{
    public Image playerHealthBar;
    public float playerHealthAmount = 100f;
    public Image robotHealthBar;
    public float robotHealthAmount = 100f;

    public UIManager UIManager;
    public Greyscaler Greyscaler;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayerTakeDamage(float damage)
    {
        //check if theres even health to take

        if (playerHealthAmount > 0)
        {
            playerHealthAmount -= damage;
            playerHealthBar.fillAmount = playerHealthAmount / 100f;
        }
        else
        {
            UIManager.onGameOver();
        }
        Debug.Log("player took damage. new health:" + playerHealthAmount);

        //desaturate screen
        Greyscaler.subtractColor();
    }
    public void PlayerHeal(float healAmount)
    {
        //make sure we are not already at full health
        if (playerHealthAmount < 100f)
        {

            playerHealthAmount += healAmount;
            playerHealthAmount = Mathf.Clamp(playerHealthAmount, 0, 100);
            playerHealthBar.fillAmount = playerHealthAmount / 100f;
        }
        Debug.Log("player healed. new health:" + playerHealthAmount);

        //add more color to the screen
        Greyscaler.addColor();
    }
    public void RobotTakeDamage(float damage)
    {

        //check if theres even health to take
        if (robotHealthAmount > 0)
        {
            robotHealthAmount -= damage;
            robotHealthBar.fillAmount = robotHealthAmount / 100f;
        }
        Debug.Log("robot took damage. new health:" + robotHealthAmount);
    }
    public void RobotHeal(float healAmount)
    {

        //make sure we are not already at full health
        if (robotHealthAmount < 100f)
        {
            robotHealthAmount += healAmount;
            robotHealthAmount = Mathf.Clamp(robotHealthAmount, 0, 100);
            robotHealthBar.fillAmount = robotHealthAmount / 100f;
        }
        Debug.Log("robot healed. new health:" + robotHealthAmount);
    }
    
}
