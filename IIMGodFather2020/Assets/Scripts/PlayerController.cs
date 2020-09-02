using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedMovement;
    private Vector3 _positionOnMovement;

    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Wheel Mouse
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

        //Movement on click
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;

            _positionOnMovement = Camera.main.ScreenToWorldPoint(mousePos);
        }
        transform.position = Vector2.Lerp(transform.position, _positionOnMovement, speedMovement * Time.deltaTime);

    }

    /// <summary>
    /// Return the end position of movement
    /// </summary>
    /// <returns></returns>
    public Vector3 GetEndPositionMovement()
    {
        return _positionOnMovement;
    }
}
