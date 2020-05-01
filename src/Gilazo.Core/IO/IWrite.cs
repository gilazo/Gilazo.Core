using System.Threading.Tasks;

namespace Gilazo.Core.IO
{
	public interface IWrite<in TIn, TOut>
	{
		Task<TOut> Write(TIn @in);
	}

	public interface IWrite<T> : IWrite<T, T>
	{
	}
}
