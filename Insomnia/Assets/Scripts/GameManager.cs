using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Enemy enemy;
    
    public GameObject spawnPosPlayer1;
    public GameObject spawnPosPlayer2;

    public GameObject basePlayer1;
    public GameObject basePlayer2;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Plusieurs instances de GameManager dans la scène");
            return;
        }
        instance = this;
    }
    
    void Update()
    {
        GatherInputs();
    }

    void GatherInputs()
    {
        if (Input.GetKeyDown("a"))
        {
            enemy.isFromPlayer1 = true;
            enemy.SpawnUnite(spawnPosPlayer1.transform.position);
        }
        if (Input.GetKeyDown("e"))
        {
            enemy.isFromPlayer1 = false;
            enemy.SpawnUnite(spawnPosPlayer2.transform.position);
        }
    }
}