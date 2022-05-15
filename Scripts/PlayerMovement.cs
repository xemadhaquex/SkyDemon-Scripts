using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public GameObject playerCamera;
    public float lookSpeed = 1.0f;
    public float lookXLimit = 45.0f;
    public int health=3;
    private AudioSource audioSource;

    public GameObject deathScreen;
    public GameObject crosshair;
    public GameObject options;
    public GameObject NumberOfEnemyTracker;



    public GameObject throwingAxe;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Physics.IgnoreCollision(throwingAxe.GetComponent<Collider>(), characterController.GetComponent<Collider>(),false);
        audioSource=GetComponent<AudioSource>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            //playerCamera.GetComponent<Camera>().transform.localRotation = Quaternion.Euler(rotationX*lookSpeed, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetButton("Cancel")){
            Time.timeScale = 0;
            OpenOptionsMenu();
        }
    }

    private bool hasCollide = false;

    void OnTriggerEnter(Collider col){
        if(col.tag=="Enemy" || col.tag=="EnemyProjectile"){
            if(hasCollide == false){
                hasCollide = true;
                health=health-1;
                Debug.Log("Player Hurt");

                audioSource.Play();

                StartCoroutine(hasCollided());

            }
        }
        if(col.tag=="Lava"){
            if(hasCollide == false){
                hasCollide = true;
                health=health-10;
                Debug.Log("Player Hurt");

                audioSource.Play();
                
                StartCoroutine(hasCollided());
            }
        }
        if(health<=0){
            PlayerDeath();
        }
    }

    void OpenOptionsMenu(){
        crosshair.SetActive(false);
        options.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void OutOfAxes(){

        NumberOfEnemyTracker.GetComponent<NumberOfEnemyTracker>().gameOverAxes();
        deathScreen.SetActive(true);
        crosshair.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }


    void PlayerDeath(){
        
        NumberOfEnemyTracker.GetComponent<NumberOfEnemyTracker>().gameOverDeath();
        deathScreen.SetActive(true);
        crosshair.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private IEnumerator hasCollided()
    {
        //gunAudio.Play();

        yield return new WaitForSeconds(1f);

        hasCollide = false;
    }
}