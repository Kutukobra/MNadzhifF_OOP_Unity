using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_Data : MonoBehaviour
{
    public static UI_Data Instance
    {
        get;
        private set;
    }

    private UIDocument document;
    private Label health;
    private Label points;
    private Label waveNumber;
    private Label enemyRemaining;

    public void Awake()
    {
        if (Instance == null)
        {
            document = GetComponent<UIDocument>();
            health = document.rootVisualElement.Q("Health") as Label;
            points = document.rootVisualElement.Q("Point") as Label;
            waveNumber = document.rootVisualElement.Q("Wave") as Label;
            enemyRemaining = document.rootVisualElement.Q("EnemyRemaining") as Label;

            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static void UpdateGameUI(float health, int points, int waveNumber, int enemyRemaining)
    {
        Instance.health.text = "Health: " + health.ToString();
        Instance.points.text = "Points: " + points.ToString();
        Instance.waveNumber.text = "Wave Number: " + waveNumber.ToString();
        Instance.enemyRemaining.text = "Enemy Remaining: " + enemyRemaining.ToString();
    }
}
