using System.Threading.Tasks;

namespace Gilazo.Core.IO
{
	public interface IRead<in TIn, TOut>
	{
		Task<TOut> Read(TIn @in);
	}

	public interface IRead<T> : IRead<T, T>
	{
	}
}
