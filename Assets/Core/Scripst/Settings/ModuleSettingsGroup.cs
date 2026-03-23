using System.Collections.Generic;

namespace MechanicsPlayground.Core
{
    public class ModuleSettingsGroup : ISettings
    {
        public string ModuleName { get; }
        public IEnumerable<ISettingsDescriptor> Settings { get; }

        public ModuleSettingsGroup(string moduleName, IEnumerable<ISettingsDescriptor> settings)
        {
            ModuleName = moduleName;
            Settings = settings;
        }

        public IEnumerable<ISettingsDescriptor> GetDescriptors() => Settings;
    }
}
