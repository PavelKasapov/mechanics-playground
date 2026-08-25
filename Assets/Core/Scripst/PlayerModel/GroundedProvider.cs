using UnityEngine;

public class GroundedProvider : MonoBehaviour
{
    [SerializeField] private float _slopeAngle = 45f;
    private readonly Vector3 _planeNormal = Vector3.up;
    private float _slopeCos = 0.707f;
    public bool IsGrounded { get; private set; }

    private void FixedUpdate()
    {
        IsGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (IsGrounded)
            return;

        foreach (var contact in collision.contacts)
        {
            float dot = Vector3.Dot(contact.normal, _planeNormal);
            bool isVertical = dot > _slopeCos;
            if (isVertical)
            {
                IsGrounded = true;
                return;
            }    
        }
    }

    private void OnValidate()
    {
        _slopeCos = Mathf.Cos(_slopeAngle * Mathf.Deg2Rad);
    }
}
