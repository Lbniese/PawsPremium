using Paws.Interface.Forms;

namespace Paws.Interface.Controls
{
    public interface ISettingsControl
    {
        SettingsForm SettingsForm { get; set; }
        void BindUiSettings();
        void ApplySettings();
    }
}