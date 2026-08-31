using UnityEngine;

public class GroundedProvider : MonoBehaviour
{
    [SerializeField] private float _slopeAngle = 45f;
    private readonly Vector3 _planeNormal = Vector3.up;
    private float _slopeCos = 0.707f;
    private float _maxDot = 0f;
    public bool IsGrounded { get; private set; }
    public Vector3 GroundNormal { get; private set; } = Vector3.up;

    private void FixedUpdate()
    {
        IsGrounded = false;
        _maxDot = 0f;
        GroundNormal = Vector3.up;
    }

    private void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            float dot = Vector3.Dot(contact.normal, _planeNormal);
            if (dot > _maxDot)
            {
                _maxDot = dot;

                if (_maxDot > _slopeCos)
                {
                    IsGrounded = true;
                    GroundNormal = contact.normal;
                }
            }
        }
    }

    private void OnValidate()
    {
        _slopeCos = Mathf.Cos(_slopeAngle * Mathf.Deg2Rad);
    }
}