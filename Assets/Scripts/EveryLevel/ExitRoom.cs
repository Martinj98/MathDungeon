using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneSwitcher;

public class ExitRoom : MonoBehaviour
{
    private SceneSwitcher sceneSwitcher;
    public directions exitDirection;
    public string levelToLoad;
    public bool showCorrect;
    public bool ShowFalse;

    public void Start()
    {
        sceneSwitcher=SceneSwitcher.Instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").);
        if (collision.gameObject.CompareTag("Player")&& GameObject.FindGameObjectsWithTag("Enemy").Length==0&&levelToLoad!="")
        {
       
            //If the screen is not fading yet, do so // this prevents a user from leaving when he is still entering
            if (!sceneSwitcher.isChangingColor)
            {
           
                Debug.Log("coroutine Leaving room");
                StartCoroutine(sceneSwitcher.leaveRoom(exitDirection,levelToLoad,showCorrect,ShowFalse));
               
            }
       
        }
    }
}
