using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyBehaviour : MonoBehaviour
{
    BoxCollider2D boxColl;
    GameObject player;
    EnemySpawn spawner;

    public float speed = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxColl = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spawner = player.transform.Find("Spawner").gameObject.GetComponent<EnemySpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime * speed);
        if (boxColl.IsTouching(player.GetComponent<CapsuleCollider2D>()))
        {
            spawner.decreaseEnemyCount();
            Destroy(gameObject);
        }
    }
}
