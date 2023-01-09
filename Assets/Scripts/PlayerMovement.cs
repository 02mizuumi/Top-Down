using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    Vector2 lastClickPos;

    bool moving;

    public int hp = 800;
    public TextMeshProUGUI hpText;

    private void Update()
    {
        hpText.text = hp.ToString();

        Vector3 mouse_pos = Input.mousePosition;
        mouse_pos.z = -20;
        Vector3 object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(1))
        {
            lastClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            moving = false;
        }

        if(moving && (Vector2)transform.position != lastClickPos)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastClickPos, step);
        }
        else
        {
            moving = false;
        }

    }

    public void takeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Hittable")
        {
            takeDamage(25);
            Destroy(col.gameObject);
        }
    }
}
