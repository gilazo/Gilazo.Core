using System;

namespace Gilazo.Core.Functional.IO
{
	public interface IRead<in TIn, TOut> : Core.IO.IRead<TIn, Either<Exception, TOut>>
	{
	}


	public interface IRead<T> : IRead<T, T>
	{
	}
}
