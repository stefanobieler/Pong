using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PaddleController : NetworkBehaviour
{

    private enum PlayerNumber
    {
        Player1,
        Player2,
    }

    // [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private PlayerNumber playerNum = PlayerNumber.Player1;

    private NetworkVariable<float> playerMoveDir = new NetworkVariable<float>();
    [SerializeField] private NetworkVariable<float> moveSpeed = new NetworkVariable<float>(10.0f);
    private PlayerInputAction playerInput;
    private InputAction moveInput;

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

        if (IsServer)
        {
            UpdateServer();
        }
        else if (IsClient && IsOwner)
        {
            UpdateClient();
        }
    }

    private void UpdateServer()
    {
        float newVerticalPosition = playerMoveDir.Value * moveSpeed.Value * Time.deltaTime;
        transform.Translate(0.0f, newVerticalPosition, 0.0f);
    }
    private void UpdateClient()
    {
        float moveDir = moveInput.ReadValue<float>();
        UpdatePlayerMoveDirectionServerRpc(moveDir);
    }

    [ServerRpc]
    void UpdatePlayerMoveDirectionServerRpc(float moveDir)
    {
        playerMoveDir.Value = moveDir;
    }


}
