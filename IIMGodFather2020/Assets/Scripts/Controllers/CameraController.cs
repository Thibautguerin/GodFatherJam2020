using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance = null;

    [Header("Zoom")]
    public float gapOnZoom = 1;
    public float speedZoom = 1;

    public float speedTransitionZoom = 1;
    public float limitMinZoom = 1;
    public float limitMaxZoom = 1;
    private Vector3 _basePosition = Vector3.zero;
    private Vector3 _actualMovementPosition = Vector3.zero;
    private float _finalZoom = 1;

    [Header("Follow")]
    public Vector2 viewportLimit = new Vector2(0.2f, 0.2f);
    public float speedTransitionMovement = 1;
    private float _currentSpeedTransitionMovement = 0;

    [Header("Bounds Cam")]
    public CameraBounds bounds;


    private Camera _camera = null;

    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);

        _camera = GetComponent<Camera>();
        _basePosition = transform.position;
        _finalZoom = limitMinZoom;
        _camera.fieldOfView = _finalZoom;
    }


    [System.Obsolete("FOV non Size ortho")]
    private void LateUpdate()
    {
        ////Player on viewport
        Vector2 PlayerOnScreen = Camera.main.WorldToViewportPoint(GameController.instance.player.transform.position);
        if (PlayerOnScreen.x < viewportLimit.x || PlayerOnScreen.y < viewportLimit.y || PlayerOnScreen.x > 1 - viewportLimit.x || PlayerOnScreen.y > 1 - viewportLimit.y)
        {

            bool up, down, left, right;
            down = PlayerOnScreen.y < viewportLimit.y;
            up = PlayerOnScreen.y > 1 - viewportLimit.y;
            left = PlayerOnScreen.x < viewportLimit.x;
            right = PlayerOnScreen.x > 1 - viewportLimit.x;

            Vector2 directionMovement = GameController.instance.player.GetEndPositionMovement() - GameController.instance.player.transform.position;

            if (PlayerOnScreen.x < 0.1f || PlayerOnScreen.y < 0.1f || PlayerOnScreen.x > 1 - 0.1f || PlayerOnScreen.y > 1 - 0.1f)
            {
                if (GameController.instance.player.GetVelocity() != Vector2.zero)
                {
                    _currentSpeedTransitionMovement = GameController.instance.player.GetVelocity().magnitude;
                }
                else
                {
                    down = PlayerOnScreen.y < 0.1f;
                    up = PlayerOnScreen.y > 1 - 0.1f;
                    left = PlayerOnScreen.x < 0.1f;
                    right = PlayerOnScreen.x > 1 - 0.1f;

                    if ((directionMovement.x > 0 && left) || (directionMovement.x < 0 && right) || (directionMovement.y > 0 && down) || (directionMovement.y < 0 && up))
                    {
                        _currentSpeedTransitionMovement = 0;
                    }
                    else
                    {
                        _currentSpeedTransitionMovement = speedTransitionMovement;
                    }
                }
            }
            else
            {
                if (GameController.instance.player.GetVelocity() != Vector2.zero)
                {
                    _currentSpeedTransitionMovement = GameController.instance.player.GetVelocity().magnitude;
                }
                else
                {
                    if ((directionMovement.x > 0 && left) || (directionMovement.x < 0 && right) || (directionMovement.y > 0 && down) || (directionMovement.y < 0 && up))
                    {
                        _currentSpeedTransitionMovement = GameController.instance.player.speedMovement;
                    }
                    else
                    {
                        _currentSpeedTransitionMovement = speedTransitionMovement;
                    }
                }
            }
        }
        else
        {
            _currentSpeedTransitionMovement = speedTransitionMovement;
        }

        Vector3 NewPosCam = Vector3.zero;
        //Use gravity or not
        if (GameController.instance.player.GetVelocity() != Vector2.zero)
        {
            //Change position of camera
            NewPosCam = Vector2.MoveTowards(transform.position, GameController.instance.player.GetVelocity(), _currentSpeedTransitionMovement * Time.deltaTime);
            NewPosCam = ClampPositionToScreen(NewPosCam);
        }
        else
        {
            //Change position of camera
            if (_camera.fieldOfView > _finalZoom+1)
            {
                Debug.Log(_camera.fieldOfView + "   " + _finalZoom);
                NewPosCam = Vector2.MoveTowards(transform.position, GameController.instance.player.transform.position, speedTransitionMovement * Time.deltaTime);
            }
            else
            {
                NewPosCam = Vector2.MoveTowards(transform.position, GameController.instance.player.GetEndPositionMovement(), _currentSpeedTransitionMovement * Time.deltaTime);
            }
            NewPosCam = ClampPositionToScreen(NewPosCam);
        }
        NewPosCam = new Vector3(NewPosCam.x, NewPosCam.y, -10);
        transform.position = NewPosCam;
        //Smooth zoom
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _finalZoom, speedZoom*Time.deltaTime);
    }

    /// <summary>
    /// Zoom action is to make a zoom to your player
    /// true is to zoom in and false is to zoom out
    /// </summary>
    public void ZoomAction(bool ZoomIn)
    {
        if (ZoomIn)
        {
            _finalZoom += gapOnZoom * Time.deltaTime;
        }
        else
        {
            _finalZoom -= gapOnZoom * Time.deltaTime;
        }

        _finalZoom = Mathf.Clamp(_finalZoom, limitMinZoom, limitMaxZoom);
    }

    private Vector3 ClampPositionToScreen(Vector3 position)
    {
        Vector3 bottomLeft = _camera.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRight = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, 0));

        //Calculate screen size
        Vector2 screenSize = new Vector2(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);
        Vector2 halfScreenSize = screenSize / 2;

        //Clamp position
        position.x = Mathf.Clamp(
            position.x,
            bounds.left + halfScreenSize.x,
            bounds.right - halfScreenSize.x
        );

        position.y = Mathf.Clamp(
            position.y,
            bounds.bot + halfScreenSize.y,
            bounds.top - halfScreenSize.y
        );

        return position;
    }

    /// <summary>
    /// Zoom action is to make a zoom to your player
    /// This version is to select a particular strenght
    /// </summary>
    public void ZoomAction(float Strenght)
    {
        _finalZoom = Strenght;
        _finalZoom = Mathf.Clamp(_finalZoom, limitMinZoom, limitMaxZoom);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        float gizmosCubeSize = 0.3f;

        Vector3 topLeft = new Vector3(bounds.left, bounds.top, transform.position.z);
        Gizmos.DrawCube(topLeft, Vector3.one * gizmosCubeSize);

        Vector3 BottomLeft = new Vector3(bounds.left, bounds.bot, transform.position.z);
        Gizmos.DrawCube(BottomLeft, Vector3.one * gizmosCubeSize);

        Vector3 topRight = new Vector3(bounds.right, bounds.top, transform.position.z);
        Gizmos.DrawCube(topRight, Vector3.one * gizmosCubeSize);

        Vector3 bottomRight = new Vector3(bounds.right, bounds.bot, transform.position.z);
        Gizmos.DrawCube(bottomRight, Vector3.one * gizmosCubeSize);


        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, BottomLeft);
        Gizmos.DrawLine(BottomLeft, topLeft);
    }
}

[System.Serializable]
public class CameraBounds
{
    public float top = 1;
    public float right = 1;
    public float bot = 1;
    public float left = 1;
}
