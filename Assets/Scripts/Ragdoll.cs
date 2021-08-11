using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    private Rigidbody[] _rigidbodies;
    private Animator _animator;
    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    public void MakeAnimated()
    {
        _animator.enabled = true;
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }
    
    public void MakePhysical()
    {
        _animator.enabled = false;
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }
}
