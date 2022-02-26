using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] enemyPrefabs; // knight = 1, mage = 2
    
    public GameObject spawnPosPlayer1;
    public GameObject spawnPosPlayer2;

    public GameObject basePlayer1;
    public GameObject basePlayer2;

    public Slider lifebarPlayer1;
    public Slider lifebarPlayer2;
    
    public int basePlayer1Life;
    public int basePlayer2Life;

    public int player1Gold;
    public int player2Gold;

    public TextMeshProUGUI p1GoldText;
    public TextMeshProUGUI p2GoldText;

    [HideInInspector] public bool isFromPlayer1;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Plusieurs instances de GameManager dans la scène");
            return;
        }
        instance = this;

        lifebarPlayer1.maxValue = basePlayer1Life;
        lifebarPlayer2.maxValue = basePlayer2Life;
        
        UpdateUI();
    }
    
    void Update()
    {
        if (basePlayer1Life == 0)
        {
            Debug.Log("Player2 won");
        }
        if (basePlayer2Life == 0)
        {
            Debug.Log("Player1 won");
        }
    }

    public void UpdateUI()
    {
        lifebarPlayer1.value = basePlayer1Life;
        lifebarPlayer2.value = basePlayer2Life;

        p1GoldText.text = player1Gold.ToString();
        p2GoldText.text = player2Gold.ToString();
    }
    
    public void SpawnKnightPlayer1()
    {
        isFromPlayer1 = true;
        Instantiate(enemyPrefabs[0],spawnPosPlayer1.transform.position, Quaternion.identity);
        player1Gold -= 10;
        UpdateUI();
    }
    public void SpawnKnightPlayer2()
    {
        isFromPlayer1 = false;
        Instantiate(enemyPrefabs[0],spawnPosPlayer2.transform.position, Quaternion.identity);
        player2Gold -= 10;
        UpdateUI();
    }
    public void SpawnMagePlayer1()
    {
        isFromPlayer1 = true;
        Instantiate(enemyPrefabs[1],spawnPosPlayer1.transform.position, Quaternion.identity);
        player1Gold -= 10;
        UpdateUI();
    }
    public void SpawnMagePlayer2()
    {
        isFromPlayer1 = false;
        Instantiate(enemyPrefabs[1],spawnPosPlayer2.transform.position, Quaternion.identity);
        player2Gold -= 10;
        UpdateUI();
    }
}