using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public float xPos;
    public float yPos;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DropEnemies());
    }

    IEnumerator DropEnemies()
    {
        while(enemyCount < 30)
        {
            xPos = Random.Range(-53, 48f);
            yPos = Random.Range(-45.2f, 56.7f);
            Instantiate(enemy, new Vector3(xPos, 0, yPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
