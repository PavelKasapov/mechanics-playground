using R3;
using UnityEngine;

namespace MechanicsPlayground.Core
{
    public class PlayerPivotHandler : IReadOnlyPlayerPivotHandler
    {
        private readonly ReactiveProperty<Transform> _pivotTransform = new ReactiveProperty<Transform>(null);
        public ReadOnlyReactiveProperty<Transform> PivotTransform => _pivotTransform;

        public void RegisterPivot(Transform pivot)
        {
            _pivotTransform.Value = pivot;
        }

        public void UnregisterPivot()
        {
            _pivotTransform.Value = null;
        }
    }

    public interface IReadOnlyPlayerPivotHandler
    {
        public ReadOnlyReactiveProperty<Transform> PivotTransform { get; }
    }
}