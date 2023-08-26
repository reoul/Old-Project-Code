using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class freelooksc : MonoBehaviour
{
    private CinemachineFreeLook cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<CinemachineFreeLook>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.m_XAxis.Value -= 0.25f;
    }
}
