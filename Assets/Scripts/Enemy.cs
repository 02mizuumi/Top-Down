using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 100;
    public void TakeDamage(int damage)
    {
        hp -= damage;
        //Debug.Log(hp);
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
