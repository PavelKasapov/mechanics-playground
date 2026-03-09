using R3;

namespace MechanicsPlayground.Core
{
    public interface IFeatureRegistry
    {
        public ReadOnlyReactiveProperty<ICameraController> ActiveCamera { get; } 
    }
}
