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

    private Camera _camera = null;

    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);

        _camera = GetComponent<Camera>();
        _basePosition = transform.position;
        _finalZoom = limitMinZoom;
    }

    private void LateUpdate()
    {
        ////Player on viewport
        Vector2 PlayerOnScreen = Camera.main.WorldToViewportPoint(GameController.instance.player.transform.position);
        if (PlayerOnScreen.x < viewportLimit.x || PlayerOnScreen.y < viewportLimit.y || PlayerOnScreen.x > 1 - viewportLimit.x || PlayerOnScreen.y > 1 - viewportLimit.y)
        {
            if (PlayerOnScreen.x < 0.1f || PlayerOnScreen.y < 0.1f || PlayerOnScreen.x > 1 - 0.1f || PlayerOnScreen.y > 1 - 0.1f)
            {
                _currentSpeedTransitionMovement = 0;
            }
            else
            {
                if (GameController.instance.player.GetVelocity() != Vector2.zero)
                {
                    _currentSpeedTransitionMovement = GameController.instance.player.GetVelocity().magnitude;
                }
                else
                {
                    _currentSpeedTransitionMovement = GameController.instance.player.speedMovement;
                }
            }
        }
        else
        {
            _currentSpeedTransitionMovement = speedTransitionMovement;
        }


        if (GameController.instance.player.GetVelocity() != Vector2.zero)
        {
            //Change position of camera
            transform.position = Vector2.MoveTowards(transform.position, GameController.instance.player.GetVelocity(), _currentSpeedTransitionMovement * Time.deltaTime);
        }
        else
        {
            //Change position of camera
            transform.position = Vector2.MoveTowards(transform.position, GameController.instance.player.GetEndPositionMovement(), _currentSpeedTransitionMovement * Time.deltaTime);
        }

        //Smooth zoom
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _finalZoom, speedZoom*Time.deltaTime);
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

    /// <summary>
    /// Zoom action is to make a zoom to your player
    /// This version is to select a particular strenght
    /// </summary>
    public void ZoomAction(float Strenght)
    {
        _finalZoom = Strenght;
        _finalZoom = Mathf.Clamp(_finalZoom, limitMinZoom, limitMaxZoom);
    }
}
