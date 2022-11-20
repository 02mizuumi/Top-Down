using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 100;
    public ParticleSystem hitFx;
    EnemySpawner spawner;

    private void Start()
    {
        spawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        //Debug.Log(hp);
        if(hp <= 0)
        {
            Instantiate(hitFx, transform.position, Quaternion.identity);
            Destroy(this.gameObject);

            spawner.enemyCount -= 1 ;
        }
    }

}
