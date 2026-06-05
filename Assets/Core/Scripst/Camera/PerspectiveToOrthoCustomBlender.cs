using Unity.Cinemachine;
using UnityEngine;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class PerspectiveToOrthoCustomBlender : IInitializable, CinemachineBlend.IBlender
    {
        [Tooltip("Minimum distance at which to place the perspective camera which will mimic the orthographic one.  \n"
            + "Changing this distance may affect the feel of the blend: a large distance will produce a better approximation "
            + "of the ortho camera, but will also make the FOV change happen more quickly at the start of the blend.  \n"
            + "Keep this distance as small as you can tolerate, to avoid precision errors which can be present at "
            + "large camera distances.")]
        public float FakeOrthoCameraDistance = 100;

        public void Initialize()
        {
            CinemachineCore.GetCustomBlender += GetCustomBlender;
        }

        public void Dispose()
        {
            CinemachineCore.GetCustomBlender -= GetCustomBlender;
        }

        // CinemachineCore.GetCustomBlender handler
        CinemachineBlend.IBlender GetCustomBlender(ICinemachineCamera camA, ICinemachineCamera camB)
        {
            // Use the custom blender if and only if we're transitioning between ortho and perspective cameras
            if (camA != null && camB != null)
            {
                var stateA = camA.State;
                var stateB = camB.State;
                Debug.Log($"{camA.Name} {camA.State.Lens.Orthographic} => {camB} {camB.State.Lens.Orthographic}");
                if (IsBlendToOrthoCandidate(ref stateA, ref stateB))
                    return this;
            }
            // Use default blender
            return null;
        }

        // CinemachineBlend.IBlender implementation
        public CameraState GetIntermediateState(ICinemachineCamera camA, ICinemachineCamera camB, float t)
        {
            var stateA = camA.State;
            var stateB = camB.State;

            // This can happen if we're blending intermediate states due to interrupted blend
            if (!IsBlendToOrthoCandidate(ref stateA, ref stateB))
                return CameraState.Lerp(stateA, stateB, t);

            if (!stateA.Lens.Orthographic)
            {
                return BlendToOrtho(ref stateA, ref stateB, t);
            }

            return BlendToOrtho(ref stateB, ref stateA, 1 - t);
        }

        bool IsBlendToOrthoCandidate(ref CameraState stateA, ref CameraState stateB)
        {
            bool orthoA = stateA.Lens.Orthographic;
            bool orthoB = stateB.Lens.Orthographic;
            // A lookAt target is required on the ortho camera in order to establish the mimic fov
            return orthoA != orthoB;
        }

        // Replaces stateB with a fake ortho camera which is a far-away perspective camera with a small fov
        CameraState BlendToOrtho(ref CameraState stateA, ref CameraState stateB, float t)
        {
            var lensB = stateB.Lens;
            var orthoSize = lensB.OrthographicSize;

            Vector3 lookAt;
            float distanceFromTarget;

            if (stateB.HasLookAt())
            {
                lookAt = stateB.ReferenceLookAt;
                distanceFromTarget = Vector3.Distance(lookAt, stateB.GetCorrectedPosition());
            }
            else
            {
                lookAt = stateB.GetCorrectedPosition();
                distanceFromTarget = 10f;
            }

            if (!stateA.HasLookAt())
                stateA.ReferenceLookAt = lookAt;

            // We want it to be far compared to the ortho size
            var extraDistance = Mathf.Max(0, Mathf.Max(FakeOrthoCameraDistance, orthoSize * 2) - distanceFromTarget);

            var rotB = stateB.GetFinalOrientation();
            stateB.RawPosition = stateB.GetCorrectedPosition() + rotB * Vector3.back * extraDistance;
            stateB.PositionCorrection = Vector3.zero;
            stateB.ReferenceUp = rotB * Vector3.up;

            // Force a spherical position algorithm
            stateB.BlendHint |= CameraState.BlendHints.SphericalPositionBlend;

            // The fov should be such as to produce the ortho size at the target's position
            var lens = stateA.Lens;
            lens.FieldOfView = 2f * Mathf.Atan(orthoSize / (extraDistance + distanceFromTarget)) * Mathf.Rad2Deg;

            // Lerp the clip planes to reduce popping
            lens.NearClipPlane = Mathf.Max(lens.NearClipPlane, extraDistance + lensB.NearClipPlane);
            lens.FarClipPlane = extraDistance + lensB.FarClipPlane;
            stateB.Lens = lens;

            // We square t to spend more time at the start of the blend, producing a smoother result
            // when the fake ortho camera is far away.  This could potentially be tweaked.
            return CameraState.Lerp(stateA, stateB, t * t);
        }
    }
}
