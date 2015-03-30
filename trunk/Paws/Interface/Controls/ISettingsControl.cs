using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paws.Interface.Controls
{
    public interface ISettingsControl
    {
        SettingsForm SettingsForm { get; set; }
        void BindUISettings();
        void ApplySettings();
    }
}
