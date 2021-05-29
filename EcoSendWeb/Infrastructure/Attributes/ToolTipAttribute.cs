using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoSendWeb.Infrastructure.Attributes
{
    public class ToolTipAttribute : DescriptionAttribute
    {
        public ToolTipAttribute() : base("")
        {
        }

        public ToolTipAttribute(string strDescription) : base(strDescription)
        {
        }
    }
}
