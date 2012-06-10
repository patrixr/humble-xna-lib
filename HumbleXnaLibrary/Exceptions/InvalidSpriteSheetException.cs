using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Humble.Exceptions
{
    public class InvalidSpriteSheetException : Exception
    {
        public string ErrorMessage
        {
            get
            {
                return base.Message.ToString();
            }
        }

        public InvalidSpriteSheetException(string errorMessage)
            : base(errorMessage) { }

        public InvalidSpriteSheetException(string errorMessage, Exception innerEx)
            : base(errorMessage, innerEx) { }
    }
}
