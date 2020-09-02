using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    
    public float speedFollow;

    [Header("Zoom")]
    public float gapOnZoom;
    public float speedTransitionZoom;
    public float limitMinZoom;
    public float limitMaxZoom;
    private Vector3 _basePosition;
    private Vector3 _actualMovementPosition;

    [Header("Follow")]
    public float speedTransitionMovement;

    private Camera _camera;

    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);

        _camera = GetComponent<Camera>();
        _basePosition = transform.position;
    }

    private void Update()
    {
        //Smooth movement on wheel
        float percent = (_camera.orthographicSize -limitMinZoom) / (limitMaxZoom - limitMinZoom);
        _actualMovementPosition = Vector3.Lerp(GameController.instance.player.GetEndPositionMovement(), _basePosition, percent);

        //Change position of camera
        transform.position = Vector3.Lerp(transform.position, _actualMovementPosition, speedTransitionZoom * Time.deltaTime* (1 - percent));
        transform.position = Vector3.Lerp(transform.position, GameController.instance.player.GetEndPositionMovement(), speedTransitionMovement * Time.deltaTime* (1 - percent));
    }

    /// <summary>
    /// Zoom action is to make a zoom to your player
    /// true is to zoom in and false is to zoom out
    /// </summary>
    public void ZoomAction(bool ZoomIn)
    {
        if (ZoomIn)
        {
            _camera.orthographicSize += gapOnZoom * Time.deltaTime;
        }
        else
        {
            _camera.orthographicSize -= gapOnZoom * Time.deltaTime;
        }
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, limitMinZoom, limitMaxZoom);
    }
}
