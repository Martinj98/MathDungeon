using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class damageEnemy : MonoBehaviour
{
    public PlayerMovement player;
    public Camera cam;

    private float timer;
    public float timeBetweenAttacks;
    public int damage;

    private float maxDistanceFromPlayer = .5f;
    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Spins the sword around the player
    void Update()
    {

        Vector3 pos=cam.ScreenToWorldPoint(Input.mousePosition);
        //there is no depth in difference set z equal to player z
        pos.z =player.transform.position.z;

       // Debug.DrawLine(pos,player.transform.position,Color.yellow,2f);
        var difference=(pos-player.transform.position);
        difference = Vector3.ClampMagnitude(difference, maxDistanceFromPlayer);
       
        transform.localPosition = difference;
        float z = 0f;
        //Maps based on x position if y is negative rotation is opossite 
        if (difference.y>0)
        {
             z = Remap(difference.x, -0.5f, 0.5f, 90, -90); //from 90 to 0 and 360 to 270
        }
        else
        {
             z = Remap(difference.x, -0.5f, 0.5f, 90f, 270f);// from 90 to 270
        }
        

        transform.rotation = Quaternion.Euler(0f, 0f, z);
        timer += Time.deltaTime;
    }
    public float Remap( float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("sword hit collision:" + collision);
     
        if (collision.gameObject.CompareTag("Enemy")&&player.GetComponent<PlayerHealth>().isAlive)
        {
            if (timer >= timeBetweenAttacks)
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                timer = 0f;
            }
        }
    }

}
