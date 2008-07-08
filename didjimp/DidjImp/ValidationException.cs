using System;
using System.Collections.Generic;
using System.Text;

namespace DidjImp
{
	public class ValidationException : Exception
	{
		public ValidationException(string message, params object[] args)
			: base(String.Format(message, args))
		{
		}
	}
}
