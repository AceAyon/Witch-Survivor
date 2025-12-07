using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public int enemyAmountTotal;
    public float spawnWidth;
    Vector2 randomNum;
    int enemyNum = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        if (enemyAmountTotal % 2 != 0)
        {
            enemyAmountTotal++;
        }
        for (int i = 0; i < enemyAmountTotal / 2; i++)
        {
            float randomY;
            if (Random.value < 0.5)
            {
                randomY = Random.Range(-13f - spawnWidth, -13f);
            }
            else
            {
                randomY = Random.Range(13f, 13f + spawnWidth);
            }
            randomNum = new Vector2(Random.Range(-20f - spawnWidth, 20f + spawnWidth) + transform.position.x, randomY + transform.position.y);
            Instantiate(enemy, randomNum, Quaternion.identity);
            enemyNum++;
        }
        for (int i = 0; i < enemyAmountTotal / 2; i++)
        {
            float randomX;
            if (Random.value < 0.5)
            {
                randomX = Random.Range(-20f - spawnWidth, -20f);
            }
            else
            {
                randomX = Random.Range(20f, 20f + spawnWidth);
            }
            randomNum = new Vector2(randomX + transform.position.x, Random.Range(-13f - spawnWidth, 13f + spawnWidth) + transform.position.y);
            Instantiate(enemy, randomNum, Quaternion.identity);
            enemyNum++;
        }
    }
    public void decreaseEnemyCount()
    {
        enemyNum--;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyNum == 0)
        {
            Spawn();
        }
    }

}
