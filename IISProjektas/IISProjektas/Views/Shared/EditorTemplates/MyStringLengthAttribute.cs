using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IISProjektas.Views.Shared.EditorTemplates
{
    public class MyStringLengthAttribute : StringLengthAttribute
    {


            public MyStringLengthAttribute(int maximumLength)
                : base(maximumLength)
            {
            }

            public override bool IsValid(object value)
            {
                string val = Convert.ToString(value);
                if (val.Length < base.MinimumLength)
                    base.ErrorMessage = "Skelbimo aprašymas turi būti ilgesnis nei 10 simbolių";
                if (val.Length > base.MaximumLength)
                    base.ErrorMessage = "Skelbimo aprašymas turi būti ne ilgesnis nei 200 simbolių";
                return base.IsValid(value);
            }
        }
    }
