using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float health = 1;
    [SerializeField] float timer = 2f;
    private float currentTimer = 0f;
    private bool isHurt = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
      _rb = GetComponent<Rigidbody>();

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Update()
    {
        if (isHurt)
        currentTimer += Time.deltaTime;
        if (currentTimer >= timer)
        {
            isHurt = false;
            currentTimer = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isHurt = true;
        TakeDamage(1);
        if (collision.gameObject.CompareTag("Turret"))
        {
            if(timer <= 0f)
            {
                Destroy(collision.gameObject);
            }
            
        }
    }
}
