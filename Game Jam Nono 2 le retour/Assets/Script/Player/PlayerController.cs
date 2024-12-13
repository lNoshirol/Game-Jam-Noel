using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Connection;
using FishNet.Object;
using UnityEditor.UIElements;

public class PlayerController : NetworkBehaviour
{

    [SerializeField] GameObject _jspCommentLappeler;



    [SerializeField] float _baseMoveSpeed;
    [SerializeField] float _moveSpeed;

    [SerializeField] Vector3 _direction;
    [SerializeField] Vector3 _lookDirection;

    [SerializeField] Camera _playerCamera;
    [SerializeField] GameObject _cameraHolder;

    [SerializeField] bool _playerIsUsingJoystick;

    private void Start()
    {
        _moveSpeed = _baseMoveSpeed;

        _playerCamera = Camera.main;
        _playerCamera.transform.SetParent(_cameraHolder.transform);
        _playerCamera.transform.position = _cameraHolder.transform.position;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            _playerCamera = Camera.main;
            _playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _playerCamera.transform.SetParent(_cameraHolder.transform);
        }
        else
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    public void OnLook(InputAction.CallbackContext callbackContext)
    {
        Vector2 value = callbackContext.ReadValue<Vector2>();


        if (callbackContext.started)
        {
            _playerIsUsingJoystick = true;
        }

        if (callbackContext.canceled)
        {
            _playerIsUsingJoystick = false;
        }
        
        if (value == Vector2.zero) { return; }

        _lookDirection = new Vector3(value.x, 0, value.y);
        
    }

    public void OnMove(InputAction.CallbackContext callbackContext)
    {
        var _valueRead = callbackContext.ReadValue<Vector2>();
        _direction = new Vector3(_valueRead.x, 0, _valueRead.y);
    }

    private void Update()
    {
        //Move
        transform.position += Time.deltaTime * _moveSpeed * _direction;

        //look Controller
        if (_direction != Vector3.zero || _playerIsUsingJoystick)
        {
            Cursor.lockState = CursorLockMode.Locked;

            Vector3 _lookHere = _jspCommentLappeler.transform.position + _lookDirection;
            _lookHere.y = transform.position.y;
            _jspCommentLappeler.transform.LookAt(_lookHere);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == 3)
                {
                    Vector3 _mouseLookDirection = hit.point;
                    _mouseLookDirection.y = transform.position.y;
                    _jspCommentLappeler.transform.LookAt(_mouseLookDirection);
                }
            }
        }
    }
}
