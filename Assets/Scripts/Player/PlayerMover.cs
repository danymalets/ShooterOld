using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GameInput))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _cameraFollowPoint;
    
    [Header("Speed")]
    [SerializeField] private float _forwardSpeed = 8f;
    [SerializeField] private float _sidewaysSpeed = 5f;
    [SerializeField] private float _backSpeed = 4f;
    [SerializeField] private float _animationTransitionTime = 0.2f;
    
    [Header("Jump")]
    [SerializeField] private float _jumpHeight = 400f;
    
    [Header("Sensitivity")]
    [SerializeField] private float _sensitivityX = 1f;
    [SerializeField] private float _sensitivityY = 1f;
    
    [SerializeField] private float _maxSensitivityX = 10f;
    [SerializeField] private float _maxSensitivityY = 10f;

    private CharacterController _characterController;
    private Animator _animator;
    private GameInput _input;

    private Vector3 _velocity;

    public Vector3 Position => transform.position;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _input = GetComponent<GameInput>();
    }
    private void Update()
    {
        Move();
        transform.Rotate(_input.Look.x * Vector3.up * _sensitivityX);
        _cameraFollowPoint.Rotate(_input.Look.y * Vector3.left * _sensitivityY);
    }

    private void Move()
    {
        Vector2 direction = _input.Move.normalized;

        if (direction == Vector2.zero)
        {
            _velocity.x = 0f;
            _velocity.z = 0f;
        }
        else
        {
            float verticalSpeed = direction.y > 0 ? _forwardSpeed : _backSpeed;
            float ratio = Mathf.Sqrt(1 / ((direction.y * direction.y) / (verticalSpeed * verticalSpeed) +
                                          (direction.x * direction.x) / (_sidewaysSpeed * _sidewaysSpeed)));
            _velocity.x = direction.x * ratio;
            _velocity.z = direction.y * ratio;          
        }

        if (_characterController.isGrounded && _velocity.y <= 0)
        {
            _velocity.y = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            _velocity.y += Physics.gravity.y * Time.deltaTime;
        }
        Debug.Log(_velocity.y);
        _animator.SetFloat(PlayerAnimator.Parameters.Horizontal, direction.x, _animationTransitionTime, Time.deltaTime);
        _animator.SetFloat(PlayerAnimator.Parameters.Vertical, direction.y,  _animationTransitionTime, Time.deltaTime);
        _animator.SetBool(PlayerAnimator.Parameters.Grounded, _characterController.isGrounded);
        _animator.SetBool(PlayerAnimator.Parameters.Jump, _input.Jump);
        _animator.SetBool(PlayerAnimator.Parameters.Fall, _velocity.y < -0.01f);
        
        _characterController.Move(transform.TransformDirection(_velocity) * Time.deltaTime);
    }

    public void OnTakeOff()
    {
        _velocity.y = Mathf.Sqrt(-2f * _jumpHeight * Physics.gravity.y);
    }
}
