using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour
{
    public GameObject aoeIndicator;
    public int baseHp;
    public int baseDmg;

    private void Start()
    {
        aoeIndicator.gameObject.SetActive(false);
    }

    /// <summary>
    /// Q: Click Dash (refresh if kill)
    /// W: throw daggers
    /// E: AOE Slash
    /// R: AOE Stun + Dash
    /// </summary>
    /*
    public void clickDash(int baseDmg, float percentDmg, float range, float cd, Transform playerPos, Camera cam, LayerMask enemyLayer)
    {
        
        int baseDmg = 55;
        float percentDmg = 0.6f;
        float range = 3.5f;
        int cd = 3;
        
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(playerPos.position, range, enemyLayer);

        foreach(Collider2D enemy in enemiesInRange)
        {
            Debug.Log("awqweasd");
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.transform != null)
            {
                if (hit.transform.gameObject.layer == enemyLayer)
                {
                    Debug.Log("DASH");
                }
            }
            
            if (Camera.main.WorldToScreenPoint(Input.mousePosition) == enemy.transform.position)
            {
                
            }
        }
        

    }*/

    bool canSlash = true;
    public void slash(int abilityBaseDmg, float percentDmg, float range, float cd, Transform playerPos, LayerMask enemyLayer)
    {
        aoeIndicator.gameObject.SetActive(true);
        canSlash = false;
        float totalDamage = (baseDmg + abilityBaseDmg) + ((baseDmg + abilityBaseDmg) * percentDmg);
        int total = (int)totalDamage;

        Collider2D[] aoe = Physics2D.OverlapCircleAll(playerPos.position, range, enemyLayer);

        foreach(Collider2D hit in aoe)
        {
            if(hit != null)
            {
                Enemy enemy = hit.transform.GetComponent<Enemy>();
                enemy.TakeDamage(total);
            }
        }
        StartCoroutine(slashCD());
    }

    IEnumerator slashCD()
    {
        yield return new WaitForSeconds(.1f);
        aoeIndicator.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.9f);
        canSlash = true;
    }

}
