using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)]
    float maxSpeed = 10f;
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;
    [SerializeField]
    Vector3 velocity;
    [SerializeField]
    GameController gameController;


    void Start()
    {
        //TODO : Speed modifier depuis le gamecontroller
        gameController = gameObject.GetComponent<GameController>();
    }

    void Update()
    {
        if (gameController.isPlayerAlreadyDefined())
        {
            MovePlayer();
            SpecialActions();
        }
    }

    void MovePlayer()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        Vector3 acceleration = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        Vector3 displacement = velocity * Time.deltaTime *gameController.GetVelocityModifier();

        gameController.GetPlayer().transform.localPosition += displacement;
    }

    void SpecialActions()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameController.UseRock();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //TODO : Pause
        }
    }

}
