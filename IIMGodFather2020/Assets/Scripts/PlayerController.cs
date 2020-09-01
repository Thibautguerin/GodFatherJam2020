using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        float wheelMouse = Input.GetAxis("Mouse ScrollWheel");
        if (wheelMouse != 0)
        {
            if (wheelMouse > 0)
            {
                //Zoom Out
                CameraController.instance.ZoomAction(false);
            }
            else
            {
                //Zoom In
                CameraController.instance.ZoomAction(true);
            }
        }
    }
}
