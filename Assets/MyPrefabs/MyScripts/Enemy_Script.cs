using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Script : MonoBehaviour
{
    public float starthealth = 100;
    public float health;


    public float startSpeed = 2f;
    public int rewardPoints = 50;


    public bool dead;
    Animator anim;
    private Transform target;
    private int waypointindex = 0;

    private float speed;
    private float animatorStartSpeed = 1;



    Building_Manager buildManager;

    public Image Healthbar;


    // Start is called before the first frame update
    void Start()
    {
        health = starthealth;
        Healthbar.fillAmount = health / starthealth;

        speed = startSpeed;


        buildManager = Building_Manager.instance;
        anim = transform.GetComponent<Animator>();
        target = waypointscript.points[0];

        anim.speed = animatorStartSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;

        anim.SetBool("isWalking", true);

        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.1f);

        if (Vector3.Distance(target.position, transform.position) <= 0.1f)
        {
            GetNextWayPoint();
        }


        speed = startSpeed;
        anim.speed = animatorStartSpeed;
    }

    void LateUpdate()
    {
        Quaternion temprotation = Healthbar.transform.parent.transform.parent.rotation;
        temprotation.y = -90;
        temprotation.z = -45;
        Healthbar.transform.parent.transform.parent.rotation = temprotation;




    }


    public void GetNextWayPoint()
    {
        if(waypointindex == (waypointscript.points.Length-1))
        {
            Destroy(gameObject);
            Player_Stats.lives--;
            return;
        }

        waypointindex++;
        target = waypointscript.points[waypointindex];
    }


    public void TakeDamage(float amount)
    {
        health -= amount;

        Healthbar.fillAmount = health / starthealth;

        if (health<=0)
        {
            Die();
        }
    }

    void Die()
    {

        dead = true;
        anim.SetTrigger("IsDead");


        Player_Stats.money += rewardPoints;
        buildManager.ShowCoinsAdd(rewardPoints);


        transform.GetComponent<Enemy_Script>().enabled = false;

        Destroy(transform.gameObject, 3f);
    }


    public void SlowEnemy(float percent)
    {
        speed = startSpeed * (1f - percent);
        anim.speed = animatorStartSpeed * (1f - percent);
    }
}
