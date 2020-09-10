using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Bird
{
    public float _radius = 1f;
    public float _explodeForce = 100;
    public float _reduceDamageFactor = 15f;
    public bool _hasExplode = false;

    public LayerMask layerHit;

    private IEnumerator Explode(float seconds)
    {
        if(!_hasExplode)
        {
            _hasExplode = true;

            yield return new WaitForSeconds(seconds);
            
            Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, _radius, layerHit);

            foreach (Collider2D obj in objects)
            {
                Vector2 direction = obj.transform.position - transform.position;

                obj.GetComponent<Rigidbody2D>().AddForce(direction * _explodeForce);

                if(obj.GetComponent<Obstacle>() != null)
                {
//                    Debug.Log((_explodeForce * direction).magnitude / _reduceDamageFactor);
                    obj.GetComponent<Obstacle>().Damage((_explodeForce * direction).magnitude / _reduceDamageFactor);
                }
            }

            Destroy(gameObject);
        }
    }

    public override void OnHit()
    {
        base.OnHit();
        StartCoroutine(Explode(2f));
    }

    public override void OnTap()
    {
        StartCoroutine(Explode(.1f));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
