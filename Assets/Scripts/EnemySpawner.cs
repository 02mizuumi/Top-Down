using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    float xPos;
    float yPos;
    public int enemyCount;

    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator DropEnemies()
    {
        while(enemyCount < 50)
        {
            xPos = Random.Range(-53, 48f);
            yPos = Random.Range(-45.2f, 56.7f);
            Instantiate(enemy, new Vector3(xPos, yPos), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            enemyCount += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DropEnemies());

        timeText.text = Time.time.ToString("0.00");

    }
}
