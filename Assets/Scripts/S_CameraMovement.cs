using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float inertia = 1.0f;
    public float maxInertiaSpeed = 10.0f; // Límite de velocidad de inercia

    private Vector3 lastTouchPosition;
    private Vector3 velocity;
    private bool isDragging = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchDeltaPosition = touch.deltaPosition;

            if (touch.phase == TouchPhase.Began)
            {
                isDragging = true;
                lastTouchPosition = touch.position;
            }

            if (isDragging)
            {
                if (touch.phase == TouchPhase.Moved)
                {
                    Vector3 moveDirection = new Vector3(-touchDeltaPosition.x, -touchDeltaPosition.y, 0);
                    transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
                    lastTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isDragging = false;
                    velocity = (new Vector3(touch.position.x, touch.position.y, 0) - lastTouchPosition) * inertia;

                    // Limitar la velocidad de inercia
                    velocity = Vector3.ClampMagnitude(velocity, maxInertiaSpeed);
                }
            }
        }

        // Aplicar inercia
        if (!isDragging)
        {
            transform.Translate(velocity * Time.deltaTime);
            velocity *= 0.95f; // Ajusta este valor según tus necesidades
        }
    }
}
