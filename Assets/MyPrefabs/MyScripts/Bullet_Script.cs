using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{

    

    private Transform target;

    [Header("BulletParameters")]

    public float bulletspeed = 30f;

    public GameObject hitEffect;

    public float explosionRadius;

    public int projectileDamage = 20;


    public void SetTargert(Transform _target)
    {
        target = _target;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {

            Vector3 dir = target.position - transform.position;

            float temp = bulletspeed * Time.deltaTime;

            if (dir.magnitude <= temp)
            {
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * temp, Space.World);
        }
    }

    void HitTarget()
    {
        GameObject tempBulletEffect = Instantiate(hitEffect,transform.position, transform.rotation);
        Destroy(tempBulletEffect, 3f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {

                
                Damage(collider.transform);
            }
        }

        Destroy(gameObject);

    }

    void Damage(Transform _transform)
    {

        Enemy_Script enem = _transform.GetComponent<Enemy_Script>();



        if (enem != null)
        {
            enem.TakeDamage(projectileDamage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


}
