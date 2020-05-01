using System;
using System.Threading.Tasks;

namespace Gilazo.Core.Functional
{
	public static class EitherExtensions
	{
		public static void Match<TL, TR>(
			this Task<Either<TL, TR>> self,
			Action<TR> right,
			Action<TL> left
		) => self.ContinueWith(t => t.Result.Match(right, left));

		public static Task<TTo> Match<TL, TR, TTo>(
			this Task<Either<TL, TR>> self,
			Func<TR, TTo> right,
			Func<TL, TTo> left
		) => self.ContinueWith(t => t.Result.Match(right, left));

		public static Task<TTo> Match<TL, TR, TTo>(
			this Task<Either<TL, TR>> self,
			Func<TR, Task<TTo>> right,
			Func<TL, Task<TTo>> left
		) => self.ContinueWith(t => t.Result.Match(right, left)).Unwrap();

		public static Task<Either<TL, TTo>> Map<TL, TR, TTo>(
			this Task<Either<TL, TR>> self,
			Func<TR, TTo> right
		) => self.ContinueWith(t => t.Result.Map(right));

		public static Task<Either<TToL, TToR>> Map<TL, TR, TToL, TToR>(
			this Task<Either<TL, TR>> self,
			Func<TR, TToR> right,
			Func<TL, TToL> left
		) => self.ContinueWith(t => t.Result.Map(right, left));

		public static Task<Either<TL, TR>> Validate<TL, TR>(
			this Task<Either<TL, TR>> self,
			Func<TR, bool> validator,
			TL left
		) => self.ContinueWith(t => t.Result.Validate(validator, left));

		public static Task<Either<TL, TR>> Apply<TL, TR>(
			this Task<Either<TL, TR>> self,
			Func<TR, Either<TL, TR>> right
		) => self.ContinueWith(t => t.Result.Apply(right));

		public static Task<Either<TL, TR>> Apply<TL, TR>(
			this Task<Either<TL, TR>> self,
			Func<TR, Task<Either<TL, TR>>> right
		) => self.ContinueWith(t => t.Result.Apply(right)).Unwrap();

		public static Task<Either<TL, TR>> Apply<TL, TR>(
			this Task<Either<TL, TR>> self,
			Func<TR, Either<TL, TR>> right,
			Func<TL, Either<TL, TR>> left
		) => self.ContinueWith(t => t.Result.Apply(right, left));

		public static Task<Either<TL, TR>> Apply<TL, TR>(
			this Task<Either<TL, TR>> self,
			Func<TR, Task<Either<TL, TR>>> right,
			Func<TL, Task<Either<TL, TR>>> left
		) => self.ContinueWith(t => t.Result.Apply(right, left)).Unwrap();
	}
}
