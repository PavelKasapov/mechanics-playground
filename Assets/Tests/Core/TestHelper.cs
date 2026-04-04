using System.Reflection;

namespace MechanicsPlayground.Tests.Core
{
    public static class TestHelper
    {
        public static void SetPrivateField(object obj, string fieldName, object value)
        {
            var field = obj.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(obj, value);
        }
    }
}