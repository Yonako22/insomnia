using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Enemy enemy;
    
    public GameObject spawnPosPlayer1;
    public GameObject spawnPosPlayer2;

    private void Awake()
    {
        spawnPosPlayer1 = GameObject.FindWithTag("SpawnPosPlayer1");
        spawnPosPlayer2 = GameObject.FindWithTag("SpawnPosPlayer2");
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