using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TicketSystem.Models
{
    public class ShouldNotContainBugAttribute : ValidationAttribute
{
    private const string word = "bug";
    public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            if (valueAsString == null)
            {
                return false;
            }

            if (valueAsString.ToLower().Contains(word))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
