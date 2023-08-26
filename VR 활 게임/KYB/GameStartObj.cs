using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartObj : MonoBehaviour, IRayInteractive
{
    public GameObject ExitObj;
    public void RayInteractive()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().FixBar.gameObject.SetActive(true);
        GameObject.Find("GameManager").GetComponent<GameManager>().FixBar.StartShow();
        ExitObj.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
