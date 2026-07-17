using System;
using Unity.Cinemachine;
using UnityEngine;
using VContainer.Unity;

namespace MechanicsPlayground.Core
{
    public class PerspectiveToOrthoCustomBlender : IInitializable, IDisposable, CinemachineBlend.IBlender
    {
        public void Initialize()
        {
            CinemachineCore.GetCustomBlender += GetCustomBlender;
        }

        public void Dispose()
        {
            CinemachineCore.GetCustomBlender -= GetCustomBlender;
        }

        private CinemachineBlend.IBlender GetCustomBlender(ICinemachineCamera camA, ICinemachineCamera camB)
        {
            if (camA == null || camB == null)
                return null;

            bool orthoA = camA.State.Lens.Orthographic;
            bool orthoB = camB.State.Lens.Orthographic;

            if (orthoA != orthoB)
                return this;

            return null;
        }

        public CameraState GetIntermediateState(ICinemachineCamera camA, ICinemachineCamera camB, float t)
        {
            if (camA == null || camB == null)
                return CameraState.Lerp(camA.State, camB.State, t);

            bool orthoA = camA.State.Lens.Orthographic;
            bool orthoB = camB.State.Lens.Orthographic;
            if (orthoA == orthoB)
                return CameraState.Lerp(camA.State, camB.State, t);

            if (!orthoA)
                return BlendToOrtho(camA, camB, t);
            else 
                return BlendToOrtho(camB, camA, 1 - t);
        }

        public float MinBlendDistance = 50f;
        public float OrthoSizeMultiplier = 1f;

        private CameraState BlendToOrtho(ICinemachineCamera perpectiveCam, ICinemachineCamera orthoCam, float t)
        {
            CameraState perspectiveState = perpectiveCam.State;
            CameraState orthoState = orthoCam.State;

            LensSettings orthoLens = orthoState.Lens;
            float orthoSize = orthoLens.OrthographicSize;
            Vector3 orthoPosition = orthoState.GetCorrectedPosition();
            Vector3 orthoDirection = orthoState.GetFinalOrientation() * Vector3.forward;

            Vector3 lookAt;
            if (orthoState.HasLookAt())
                lookAt = orthoState.ReferenceLookAt;
            else
            {
                lookAt = orthoPosition + orthoDirection * (-orthoPosition.y / orthoDirection.y);
            }

            Vector3 fromTargetToCam = (orthoPosition - lookAt).normalized;
            if (fromTargetToCam == Vector3.zero)
                fromTargetToCam = orthoDirection;

            float targetDistance = Mathf.Max(MinBlendDistance, orthoSize * OrthoSizeMultiplier);

            Vector3 newPosition = lookAt + fromTargetToCam * targetDistance;

            Vector3 toLookAt = (lookAt - newPosition).normalized;
            Quaternion newRotation = Quaternion.LookRotation(toLookAt, orthoState.GetFinalOrientation() * Vector3.up);

            orthoState.RawPosition = newPosition;
            orthoState.RawOrientation = newRotation;
            orthoState.PositionCorrection = Vector3.zero;
            orthoState.ReferenceUp = newRotation * Vector3.up;
            orthoState.BlendHint |= CameraState.BlendHints.SphericalPositionBlend;

            float fov = 2f * Mathf.Atan(orthoSize / targetDistance) * Mathf.Rad2Deg;

            LensSettings blendedLens = perspectiveState.Lens;
            blendedLens.FieldOfView = fov;

            blendedLens.NearClipPlane = Mathf.Max(blendedLens.NearClipPlane, targetDistance * 0.1f);
            blendedLens.FarClipPlane = Mathf.Max(blendedLens.FarClipPlane, targetDistance * 2f);
            orthoState.Lens = blendedLens;

            if (!perspectiveState.HasLookAt())
                perspectiveState.ReferenceLookAt = lookAt;

            return CameraState.Lerp(perspectiveState, orthoState, t * t);
        }
    }
}