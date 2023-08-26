using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExitObj : MonoBehaviour, IRayInteractive
{
    public void RayInteractive()
    {
        Application.Quit();
    }
}
