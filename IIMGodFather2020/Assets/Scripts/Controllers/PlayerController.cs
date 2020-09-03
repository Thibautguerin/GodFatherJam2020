using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public SpriteRenderer display;
    public float speedMovement = 0;
    public float mouseDeadZoneRadius = 0.8f; 
    private Vector3 _positionOnMovement = Vector3.zero;

    public StatsPlayer[] changementStats = new StatsPlayer[2];
    private Rigidbody2D _rb = null;
    private int _currentStat = 0;
    private CircleCollider2D _areaAirAttack = null;

    private bool _biggerAttack = false;
    public float timerBigAttack = 0.5f;

    public Animator animator;
    private float _sizeScale = 0;

    public ParticleSystem shockwaveParticle;
    public ParticleSystem shockwaveInverseParticle;


    private bool inPause = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _areaAirAttack = GetComponentInChildren<CircleCollider2D>();
        _areaAirAttack.enabled = false;
        _sizeScale = transform.localScale.x;
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
        if (!inPause)
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
            mousePos.z = 10;

            if ((Vector2.Distance(Camera.main.ScreenToWorldPoint(mousePos), transform.position) > mouseDeadZoneRadius
                || Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(mousePos)) > mouseDeadZoneRadius)
                && MapController.instance.CheckPosition(Camera.main.ScreenToWorldPoint(mousePos), transform.position))
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

            if (Vector2.Distance(Camera.main.ScreenToWorldPoint(mousePos), transform.position) > mouseDeadZoneRadius
            || Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(mousePos)) > mouseDeadZoneRadius)
            {
                transform.position = Vector2.MoveTowards(transform.position, _positionOnMovement, speedMovement * Time.deltaTime);
                Vector3 directionMovement = _positionOnMovement - transform.position;
                if (directionMovement.x != 0)
                {
                    if (directionMovement.x < 0)
                    {
                        display.transform.localScale = new Vector3(_sizeScale, _sizeScale, _sizeScale);
                    }
                    else
                    {
                        display.transform.localScale = new Vector3(-_sizeScale, _sizeScale, _sizeScale);
                    }
                }
            }
        }
    }

    public void UpGradeStats()
    {
        _currentStat++;
        if (_currentStat >= changementStats.Length)
        {
            _currentStat = changementStats.Length - 1;
        }
        else
        {
            StartCoroutine(BiggerAttack());
            ApplyStats();
            animator.SetTrigger("Growth");
            SoundEffectsController.instance.MakeSpiritGrowthSound();
        }
    }
    public void DownGradeStats()
    {
        _currentStat--;
        if (_currentStat < 0)
        {
            _currentStat = 0;
        }
        else
        {
            StartCoroutine(AirAttack());
            ApplyStats();
            animator.SetTrigger("Narrowing");
            SoundEffectsController.instance.MakeShockWaveSound();
        }
    }
    public void ApplyStats()
    {
        _rb.velocity = Vector2.zero;
        StatsPlayer newStat = changementStats[_currentStat];
        speedMovement = newStat.speedMovement;
        _rb.gravityScale = newStat.gravityStrength;
        CameraController.instance.ZoomAction(newStat.zoomPower);
        
    }
    
    public IEnumerator AirAttack()
    {
        _areaAirAttack.enabled = true;
        shockwaveParticle.Play();
        yield return new WaitForSeconds(0.3f);
        _areaAirAttack.enabled = false;
    }

    public IEnumerator BiggerAttack()
    {
        _biggerAttack = true;
        shockwaveInverseParticle.Play();
        yield return new WaitForSeconds(timerBigAttack);
        _biggerAttack = false;
    }

    public bool GetBiggerAttack()
    {
        return _biggerAttack;
    }
    /// <summary>
    /// Return the end position of movement
    /// </summary>
    /// <returns></returns>
    public Vector3 GetEndPositionMovement()
    {
        return _positionOnMovement;
    }

    public Vector2 GetVelocity()
    {
        return _rb.velocity;
    }

    public float GetGravity()
    {
        return _rb.gravityScale;
    }
    
    public void SetInPause(bool value)
    {
        inPause = value;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, mouseDeadZoneRadius);
        Gizmos.DrawSphere(_positionOnMovement, 0.2f);
    }
    
}

[System.Serializable]
public struct StatsPlayer
{
    public string trigger;
    public float speedMovement;
    public float gravityStrength;
    public float zoomPower;
}
