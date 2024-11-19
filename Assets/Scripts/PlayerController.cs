using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpSpeed = 5.0f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 5)
        {
            winTextObject.SetActive(true);

            Destroy(GameObject.FindGameObjectWithTag("Enemy"));

            // Cambio de escena
            SceneManager.LoadScene("Escena2");
        }
    }

    void FixedUpdate()
    {
        

        //UpdateJump();
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.spaceKey.isPressed)
            {
                Debug.Log("saltando");
                Vector3 movement = new Vector3(0.0f, 20.0f, 0.0f);
                rb.AddForce(movement);

            }
            else
            {
                Vector3 movement = new Vector3(movementX, 0.0f, movementY);
                rb.AddForce(movement * speed);
                //Debug.Log("sin saltar");
            }
            
        } 
        


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Destroy the current object
            Destroy(gameObject);
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

            // en caso de perder llamo al menu
            SceneManager.LoadScene("Menu");
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

    }

    private void UpdateJump()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                Vector3 movement = new Vector3(0.0f, jumpSpeed, 0.0f);
                rb.AddForce(movement);

            }
            
        }

    }
}
