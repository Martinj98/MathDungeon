using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    //public SpawnPoint[] spawnPoints;
    public ExitRoom[] exitRooms;

    public Transform player;
    public enum directions { North, East, South, West };

    public Image fadeImage;
    public Image correctImage;
    public Image falseImage;


    private Color startColor;
    private float changeTicks = 100f;
    private float timeBetweenTicks = 0.01f;

    public bool isChangingColor; //Is the screen fading from or to black
    public bool isExitingOldScene; // is the screen fading to black / exiting the level

    ///Singleton
    public static SceneSwitcher Instance { get; private set; }

    private void Awake()
    {
        //Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
  
    public IEnumerator leaveRoom(directions exitDirection, string levelToLoad,bool showCorrect, bool ShowFalse)
    {
        isExitingOldScene = true;
        if (showCorrect) {
            correctImage.gameObject.SetActive(true);
        }
        if (ShowFalse)
        {
            falseImage.gameObject.SetActive(true);
        }

        //Make a fade transition to black
        //changeTicks = 100;
        Color currentCol = fadeImage.color;
        Color transColor = new Color(currentCol.r, currentCol.g, currentCol.b, 1);
        StartCoroutine(updateColor(transColor));
        while (isChangingColor)
        {
           
            yield return null;
        }
        loadNewLevel(exitDirection, levelToLoad);

        correctImage.gameObject.SetActive(false);
        falseImage.gameObject.SetActive(false);
    }
    private IEnumerator updateColor(Color newColor)
    {
        startColor = fadeImage.color;
        isChangingColor = true;
        int i = 0;
        float percentage = 0f;
        while (percentage < 1)
        {
            i++;
            percentage = (1f / changeTicks) * i;
            fadeImage.color = Color.Lerp(startColor, newColor, percentage);
            //changeTicks determines how many times to loop //care! if 0 this is an infinite loop
            yield return new WaitForSeconds(timeBetweenTicks);
        }
        isChangingColor = false;


    }

    private void loadNewLevel(directions exitDirection,string levelToLoad)
    {

           isExitingOldScene = false;

        SceneManager.LoadScene(levelToLoad);
        setPlayerPosition(exitDirection);

        //Fade in quicker
        //changeTicks = 10;
        Color currentCol = fadeImage.color;
        Color normalColor = new Color(currentCol.r, currentCol.g, currentCol.b, 0);
        StartCoroutine(updateColor(normalColor));

    }

    void setPlayerPosition(directions exitDirection)
    {
        //place the player on the spawnpoint opposite from his exitpoint.
        switch (exitDirection)
        {
            case directions.North:
                player.position = Array.Find(exitRooms, element => element.exitDirection == directions.South).transform.position;
                break;
            case directions.East:
                player.position = Array.Find(exitRooms, element => element.exitDirection == directions.West).transform.position;
                break;
            case directions.South:
                player.position = Array.Find(exitRooms, element => element.exitDirection == directions.North).transform.position;
                break;
            case directions.West:
                player.position = Array.Find(exitRooms, element => element.exitDirection == directions.East).transform.position;
                break;
            default:
                break;
        }
    }

}
