using UnityEngine;

public class Puck : MonoBehaviour
{
    [SerializeField] private float startSpeed = 5.0f;
    [SerializeField] private float maxSpeed = 10.0f;
    [SerializeField] private float minSpeed = 1.0f;
    private Vector2 moveDir;
    private Rigidbody2D puckRB;
    private float currentSpeed;

    private void OnEnable()
    {
        GameManager.ResetPuck += OnResetPuck;
    }
    private void OnResetPuck(){
        transform.position = new Vector2(0.0f, 0.0f);
        moveDir = new Vector2(-1.0f, 0.0f);
        puckRB.velocity = moveDir * startSpeed;
        currentSpeed = puckRB.velocity.magnitude;
    }

    private void Start()
    {
        puckRB = GetComponent<Rigidbody2D>();
        OnResetPuck();
    }
    private void Update()
    {
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
    }

    private void FixedUpdate()
    {
        puckRB.velocity = currentSpeed * moveDir;
    }
    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Paddle"))
        {
            Vector2 currentBallPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 currentPaddlePosition = new Vector2(col.transform.position.x, col.transform.position.y);
            moveDir = (currentBallPosition - currentPaddlePosition).normalized;
            currentSpeed *= 1.15f;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            moveDir.y = Mathf.Sign(transform.position.y - col.transform.position.y);
            currentSpeed *= 0.9f;
        }
    }
}
