using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class LoadScene : MonoBehaviour
{
    [Header("This script is assigned to every level/scene and sets the exits in sceneswitcher")]
    [Header("A room should have 1 exit for each direction(default grid has 4), if there is no exit leveltoload is empty")]
    public ExitRoom[] exits;
    void Start()
    {
        setCrates();
        //
        SceneSwitcher.Instance.exitRooms = exits;
    }

    void setCrates()
    {
        //If the room has no exit a crate is placed before it
        foreach (ExitRoom exit in exits)
        {
            var box = exit.transform.GetChild(0);
            if (exit.levelToLoad == "")
            {
                //enable the box's sprite and hitbox if there is no new level
                box.gameObject.SetActive(true);
            }
            else
            {
                //disable the box if this exits leads to another level
                box.gameObject.SetActive(false);
            }
        }
    }
}
