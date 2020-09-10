using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Obstacle
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null) return;

        if (collision.gameObject.tag == "Bird")
        {
            Damage(collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10);
        }
    }
}
