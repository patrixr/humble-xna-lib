using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Exceptions
{
    public class AnimationOperationException : Exception
    {
        public string ErrorMessage
        {
            get
            {
                return base.Message.ToString();
            }
        }

        public AnimationOperationException(string errorMessage)
            : base(errorMessage) { }
    }
}
