using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float rotationSensitivity = 10f;
    CharacterController _characterController;
    Camera _camera;

    float jaw;
    float pitch;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>();

        // pitch = transform.localEulerAngles.x;
        // jaw = transform.localEulerAngles.y;
    
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float mouseHorizontal = Input.GetAxis("Mouse X");
        float mouseVertical = Input.GetAxis("Mouse Y");

        jaw += mouseHorizontal * rotationSensitivity * Time.deltaTime;
        pitch -= mouseVertical * rotationSensitivity * Time.deltaTime;

        pitch = Mathf.Clamp(pitch, -90f, 90f);

        Vector3 inputOnAxis = new Vector3(horizontal, 0, vertical); //input XZ axis
        Vector3 motion = inputOnAxis * speed * Time.deltaTime; //motion on global axis
        motion = transform.rotation * motion; //motion multiplied by rotation -> getting local transformation
        motion += Physics.gravity * Time.deltaTime;

        transform.localRotation = Quaternion.Euler(Vector3.up * jaw);
        _characterController.Move(motion);
    }

    private void LateUpdate()
    {
        _camera.transform.localRotation = Quaternion.Euler(Vector3.right * pitch);
    }

}
