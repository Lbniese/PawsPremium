using System.Reflection;

namespace Paws.Interface
{
    public interface IItemConditionParameterControl
    {
        PropertyInfo BoundProperty { get; set; }
        object GetParameterValue();
    }
}