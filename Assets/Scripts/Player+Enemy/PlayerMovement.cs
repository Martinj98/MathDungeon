using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    public Rigidbody2D rb;
    public float moveSpeed =5f;
    Vector2 movement;

    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //If we are loading a new scene/talking/dead
        if (SceneSwitcher.Instance.isExitingOldScene || dialogueManager.isInConversation|| !GetComponent<PlayerHealth>().isAlive)
        {
            GetComponent<Animator>().SetBool("IsMoving", false);
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y=Input.GetAxisRaw("Vertical");
       // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //if we are not moving
        if (movement.x == 0f&&movement.y==0)
        {
            GetComponent<Animator>().SetBool("IsMoving", false);
           
        }
        else
        {
            GetComponent<Animator>().SetBool("IsMoving", true);
        
            //Moving left And facing left 
            if (movement.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            //Moving right And facing right
         
            else if(movement.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
