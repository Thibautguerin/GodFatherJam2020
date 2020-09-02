using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public SpriteRenderer display;
    public float speedMovement = 0;
    private Vector3 _positionOnMovement = Vector3.zero;

    public StatsPlayer[] changementStats = new StatsPlayer[2];
    private Rigidbody2D _rb = null;
    private int _currentStat = 0;
    private CircleCollider2D _areaAirAttack = null;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _areaAirAttack = GetComponentInChildren<CircleCollider2D>();
        _areaAirAttack.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameController.instance.player = this;
        ApplyStats();
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

        //Movement with mouse position
        
         Vector3 mousePos = Input.mousePosition;
         mousePos.z = Camera.main.nearClipPlane;

         if (MapController.instance.CheckPosition(Camera.main.ScreenToWorldPoint(mousePos), transform.position))
         {
            _positionOnMovement = Camera.main.ScreenToWorldPoint(mousePos);
         }
        
        if (Input.GetMouseButtonDown(0))
        {
            UpGradeStats();
        }
        if (Input.GetMouseButtonDown(1))
        {
            DownGradeStats();
        }
        transform.position = Vector2.MoveTowards(transform.position, _positionOnMovement, speedMovement * Time.deltaTime);
    }

    public void UpGradeStats()
    {
        _currentStat++;
        if (_currentStat >= changementStats.Length)
        {
            _currentStat = 0;
        }
        ApplyStats();
    }
    public void DownGradeStats()
    {
        StartCoroutine(AirAttack());
        _currentStat--;
        if (_currentStat < 0)
        {
            _currentStat = changementStats.Length-1;
        }
        ApplyStats();
    }
    public void ApplyStats()
    {
        _rb.velocity = Vector2.zero;
        StatsPlayer newStat = changementStats[_currentStat];
        speedMovement = newStat.speedMovement;
        _rb.gravityScale = newStat.gravityStrength;
        CameraController.instance.ZoomAction(newStat.zoomPower);
        display.color = newStat.color;
    }
    
    public IEnumerator AirAttack()
    {
        _areaAirAttack.enabled = true;
        yield return new WaitForSeconds(0.3f);
        _areaAirAttack.enabled = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if (collision.GetComponent<EnemyBehaviour>())
        {
            collision.GetComponent<EnemyBehaviour>().Die();
        }
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

[System.Serializable]
public struct StatsPlayer
{
    public Color color;
    public float speedMovement;
    public float gravityStrength;
    public float zoomPower;
}
