using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{

    private enum PlayerNumber
    {
        Player1,
        Player2,
    }

    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private PlayerNumber playerNum = PlayerNumber.Player1;
    private PlayerInputAction playerInput;
    private InputAction moveInput;
    private float moveDir;

    private void Awake()
    {
        playerInput = new PlayerInputAction();

    }

    private void OnEnable()
    {
        switch (playerNum)
        {
            case PlayerNumber.Player1:
                moveInput = playerInput.Player1.Move;
                break;
            case PlayerNumber.Player2:
                moveInput = playerInput.Player2.Move;
                break;
            default:
                Debug.Log("No Player Controller Selected");
                break;

        }

        moveInput.Enable();

    }

    private void OnDisable()
    {
        moveInput.Disable();
    }

    private void Update()
    {
        moveDir = moveInput.ReadValue<float>();
        float newPosition = moveDir * moveSpeed * Time.deltaTime;
        transform.Translate(0.0f, newPosition, 0.0f);
    }
}
