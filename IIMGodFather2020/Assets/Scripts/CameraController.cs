using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    
    public float speedFollow;

    [Header("Zoom")]
    public float speedZoom;
    public float limitMinZoom;
    public float limitMaxZoom;
    private Vector3 _basePosition;

    private Camera _camera;

    private void Awake()
    {
        if (!instance) instance = this;
        else if (instance != this) Destroy(gameObject);

        _camera = GetComponent<Camera>();
        _basePosition = transform.position;
    }
    
    /// <summary>
    /// Zoom action is to make a zoom to your player
    /// true is to zoom in and false is to zoom out
    /// </summary>
    public void ZoomAction(bool ZoomIn)
    {
        if (ZoomIn)
        {
            _camera.orthographicSize += speedZoom * Time.deltaTime;
        }
        else
        {
            _camera.orthographicSize -= speedZoom * Time.deltaTime;
        }
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, limitMinZoom, limitMaxZoom);
        transform.position = Vector3.Lerp(GameController.instance.player.transform.position, _basePosition, (_camera.orthographicSize - limitMinZoom) / (limitMaxZoom - limitMinZoom));
    }
}
