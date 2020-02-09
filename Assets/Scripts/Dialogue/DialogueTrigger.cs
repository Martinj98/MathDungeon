using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    public bool inRange ;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&inRange)
        {
            TriggerDialogue();
        }
    }
    public void TriggerDialogue()
    {
        //Turn to player //Triggers always face right at default rotation
        if(FindObjectOfType<PlayerMovement>().gameObject.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        //Display sentences
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    //This object has a coliider to prevent things from running into it, and a trigger to trigger dialogue
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }

}
