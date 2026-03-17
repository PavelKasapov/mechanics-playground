using System.Collections.Generic;

namespace MechanicsPlayground.Core
{
    public interface ISettings
    {
        public IEnumerable<ISettingsDescriptor> GetDescriptors();
    }
}