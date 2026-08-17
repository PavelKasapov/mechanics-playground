using UnityEngine;

public class GroundedProvider : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    public bool IsGrounded { get; private set; }

    private void FixedUpdate()
    {
        IsGrounded = true;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        IsGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsGrounded = false;
    }*/
}
