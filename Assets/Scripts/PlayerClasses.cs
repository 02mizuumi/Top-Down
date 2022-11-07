using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerClasses : MonoBehaviour 
{
    public Transform player;
    public Camera cam;
    public LayerMask enemyLayer;
    public enum classes {Assassin, Mage};
    classes currentClass;
    

    private void Start()
    {
        currentClass = classes.Assassin;
    }

    private void Update()
    {
        if(currentClass == classes.Mage)
        {
            Mage mage = this.GetComponent<Mage>();
            
        }

        if (currentClass == classes.Assassin)
        {
            Assassin assassin = this.GetComponent<Assassin>();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //assassin.clickDash(55, 0.6f, 3.5f, 3, player, cam, enemyLayer);
            }
            //if(Input.GetKeyDown(KeyCode.W))
            //{
                //assassin.slash(12, 0.6f, 1.5f, 3, player, enemyLayer);
            //}
        }
        
    }

}
