using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{

    [SerializeField] float horizontalSpeed = 5f;
    [SerializeField] float verticalSpeed = 5f;

    [SerializeField] float xOffset = 5;
    [SerializeField] float yOffset = 5;

    private float leftRestriction;
    private float rightRestriction;

    private float upRestriction;
    private float downRestriction;
    // Start is called before the first frame update
    void Start()
    {
        leftRestriction = transform.position.x - xOffset;
        rightRestriction = transform.position.x + xOffset;

        upRestriction = transform.position.y + yOffset;
        downRestriction = transform.position.y - yOffset;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRestrictions();
        float verticalMovement = verticalSpeed * Time.deltaTime;
        float horizontalMovement = horizontalSpeed * Time.deltaTime;
        transform.Translate(new Vector3(horizontalMovement , verticalMovement,0));
        
    }

    void CheckRestrictions()
    {
        if (transform.position.x >= rightRestriction || transform.position.x <= leftRestriction)
        {
            horizontalSpeed = -horizontalSpeed;
        }

        if (transform.position.y >= upRestriction || transform.position.y <= rightRestriction)
        {
            verticalSpeed = -verticalSpeed;
        }
    }
}
