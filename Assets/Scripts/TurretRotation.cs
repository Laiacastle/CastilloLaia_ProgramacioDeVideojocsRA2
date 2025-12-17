using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class TurretRotation : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    
    public GameObject ShootPoint;
    public GameObject Bullet;
    public float ProjectileSpeed;
    private Vector2 _distanceBetweenMouseAndPlayer;
    [SerializeField] private float minDistance;
    [SerializeField] private float offset = 180f;
    private InputSystem_Actions _input;
    private void Awake()
    {
        _input = new InputSystem_Actions();
        _input.Player.SetCallbacks(this);
    }
    private void OnEnable()
    {
        _input.Enable();
    }
    private void OnDisable()
    {
        _input.Disable();
    }
    private void Update()
    {
        
    }


    public void OnShoot(InputAction.CallbackContext context)
    {
        if (_distanceBetweenMouseAndPlayer.magnitude < minDistance) return; //comprovar si el cursor està dins la distància mínima
        if (context.started)
        {
            var projectile = Instantiate(Bullet, ShootPoint.transform.position, Quaternion.identity);
            var direction = _distanceBetweenMouseAndPlayer.normalized;
            Vector3 velocity = ProjectileSpeed * direction;
            projectile.GetComponent<Rigidbody2D>().linearVelocity = velocity;//quina velocitat ha de tenir la bala? s'ha de fer alguna cosa al vector direcció?
        }
    }

    public void OnMousepos(InputAction.CallbackContext context)
    {
        Vector3 mousePos = context.ReadValue<Vector2>(); //obtenir el valor del click del cursor 
        var mouseInWorldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        
        //if (mousePos - transform.position < minDistance) return; //comprovar si el cursor està dins la distància mínima
        
        _distanceBetweenMouseAndPlayer = mouseInWorldPoint - transform.position;//obtenir el vector distància entre el canó i el cursor
        float angle = (Mathf.Atan2(_distanceBetweenMouseAndPlayer.y,
                _distanceBetweenMouseAndPlayer.x) * 180f / Mathf.PI + offset);
        transform.rotation = Quaternion.Euler(0, 0, angle); //en quin dels tres eixos va l'angle?
    }

    
}
