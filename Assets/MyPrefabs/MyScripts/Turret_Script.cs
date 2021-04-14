using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Script : MonoBehaviour
{



    [Header("Unity Information")]

    private Transform target;
    public string enemyTag = "Enemy";

    [Header("General")]

    public float range = 15f;
    public float turnSpeed = 10f;

    [Header("UsingBulletOptional")]
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public GameObject bulletPrefab;
    public Transform bulletInitilizer;

    [Header("UsingLaserOptional")]

    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem laserimpact;
    public int laserDPS = 50;
    public float laserSlowPercent = 0.5f;



    private Enemy_Script targetref;


    Building_Manager buildingManager;



    // Start is called before the first frame update
    void Start()
    {
        buildingManager = Building_Manager.instance;

        if(!(laserimpact == null))
        {
            if (!laserimpact.isPlaying)
            {
                laserimpact.Play();
            }

        }

        bulletInitilizer = transform.GetChild(0).GetChild(0).transform;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        if(target.GetComponent<Enemy_Script>().dead)
        {
            return;
        }

        LockOnTarget();

        if(useLaser)
        {
            ShootLaser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }


            fireCountdown -= Time.deltaTime;
        }



       
    }


    void ShootLaser()
    {

        targetref.TakeDamage(laserDPS * Time.deltaTime);
        targetref.SlowEnemy(laserSlowPercent);

        //print(target.GetComponent<Enemy_Script>().health);

        Vector3 temppos = target.position;
        temppos.y += 1.5f;



        laserimpact.transform.GetChild(1).GetComponent<Light>().enabled = true;
        Vector3 dir = bulletInitilizer.transform.position - target.transform.position;
        laserimpact.transform.position = temppos + dir.normalized * .1f;
        laserimpact.transform.rotation = Quaternion.LookRotation(dir);



        if (!laserimpact.isPlaying)
        {
            laserimpact.Play();
        }


        lineRenderer = transform.GetComponent<LineRenderer>();

        lineRenderer.SetPosition(0,bulletInitilizer.position);
        lineRenderer.SetPosition(1, temppos);

        buildingManager.PlayTheAudio(buildingManager.laserfiresound, 0.1f);

    }

    void LineRendererReset()
    {

        lineRenderer = transform.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, bulletInitilizer.position);
        lineRenderer.SetPosition(1, bulletInitilizer.position);

        if(laserimpact.isPlaying)
        {

            laserimpact.transform.GetChild(1).GetComponent<Light>().enabled = false;
            laserimpact.Stop();
        }
       
    }


    void Shoot()
    {
       GameObject Tempbullet =  Instantiate(bulletPrefab, bulletInitilizer.position, bulletInitilizer.rotation);

        Bullet_Script bullet = Tempbullet.GetComponent<Bullet_Script>();
        Missile_Script misc = Tempbullet.GetComponent<Missile_Script>();

        if(bullet != null)
        {
            bullet.SetTargert(target);
        }
        else if (misc != null)
        {
            misc.SetTargert(target);
        }
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            if(enemy.transform.GetComponent<Enemy_Script>().dead)
            {
                continue;
            }

            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetref = target.GetComponent<Enemy_Script>();
        }
        else if (shortestDistance > range)
        {
            target = null;
            LineRendererReset();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 Rotation = Quaternion.Lerp(transform.GetChild(0).rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;  // lep to make it slowly rotate towards that roataion  to only rotate it against only one axis
        transform.GetChild(0).rotation = Quaternion.Euler(0f, Rotation.y, 0f); // 0 in x and z and y rotation only
    }
}
