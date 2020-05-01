using System;

namespace Gilazo.Core.Functional.IO
{
	public interface IWrite<in TIn, TOut> : Core.IO.IWrite<TIn, Either<Exception, TOut>>
	{
	}


	public interface IWrite<T> : IWrite<T, T>
	{
	}
}
