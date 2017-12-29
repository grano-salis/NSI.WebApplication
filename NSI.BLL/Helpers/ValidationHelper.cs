using NSI.DC.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.Helpers
{
    public static class ValidationHelper
    {
        public static void IntegerGreaterThanZero(int value, string name="Number")
        {
            if (value < 0) throw new NSIException($"{name} must be greater than zero");
        }
    }
}
