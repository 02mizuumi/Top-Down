using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Vector2 hitPos;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 1.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(GameObject.Find("Player").GetComponent<Mage>().eOnHitFX, collision.transform.position, Quaternion.identity);
        Destroy(gameObject);

        if(collision.gameObject.layer == 3)
        {
            collision.GetComponent<Enemy>().TakeDamage(35);
        }  
    }
}