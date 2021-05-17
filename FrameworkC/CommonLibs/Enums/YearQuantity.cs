using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace FrameworkC.CommonLibs.Enums
{
    public enum YearQuantity
    {
        [Description("1 year")]
        ONE_YEAR=0,
        [Description("+1 year")]
        TWO_YEARS=1,
        [Description("+2 years")]
        THREE_YEARS=2,
        [Description("+3 years")]
        FOUR_YEARS=3,
        [Description("+4 years")]
        FIVE_YEARS=4
    }
}
