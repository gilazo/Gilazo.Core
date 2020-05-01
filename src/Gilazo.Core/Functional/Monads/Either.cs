using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Gilazo.Core.Functional
{
	[DebuggerStepThrough]
	public readonly struct Either<TL, TR>
	{
		[AllowNull]
		private readonly TR _right;

		[AllowNull]
		private readonly TL _left;

		public bool IsRight { get; }

		public bool IsLeft => !IsRight;

		public static Either<TL, TR> Right(TR right) => new Either<TL, TR>(right, default, true);
		public static Either<TL, TR> Left(TL left) => new Either<TL, TR>(default, left, false);

		private Either([AllowNull]TR right, [AllowNull]TL left, bool isRight)
		{
			_right = right;
			_left = left;
			IsRight = isRight;
		}

		public static implicit operator Either<TL, TR>(TR right) => Right(right);

		public static implicit operator Either<TL, TR>(TL left) => Left(left);

		public static implicit operator Task<Either<TL, TR>>(Either<TL, TR> either) => Task.FromResult(either);

		public void Match(Action<TR> right, Action<TL> left) =>
			Execute(r => { right(r); return string.Empty; }, l => { left(l); return string.Empty; });

		public TTo Match<TTo>(Func<TR, TTo> right, Func<TL, TTo> left) => Execute(right, left);

		public Task<TTo> Match<TTo>(Func<TR, Task<TTo>> right, Func<TL, Task<TTo>> left) => Execute(right, left);

		public Either<TL, TTo> Map<TTo>(Func<TR, TTo> right) => Map(right, l => l);

		public Either<TToL, TToR> Map<TToL, TToR>(Func<TR, TToR> right, Func<TL, TToL> left) =>
			Execute(r => Either<TToL, TToR>.Right(right(r)), l => Either<TToL, TToR>.Left(left(l)));

		public Either<TL, TR> Validate(Func<TR, bool> validator, TL left) => Execute(validator, Right, _ => left);

		public Either<TL, TR> Apply(Func<TR, Either<TL, TR>> right) => Apply(right, Left);

		public Task<Either<TL, TR>> Apply(Func<TR, Task<Either<TL, TR>>> right) => Execute(right, l => Either<TL, TR>.Left(l));

		public Either<TL, TR> Apply(Func<TR, Either<TL, TR>> right, Func<TL, Either<TL, TR>> left) => Execute(right, left);

		public Task<Either<TL, TR>> Apply(Func<TR, Task<Either<TL, TR>>> right, Func<TL, Task<Either<TL, TR>>> left) => Execute(right, left);

		private T Execute<T>(Func<TR, T> right, Func<TL, T> left) => Execute(_ => true, right, left);

		private T Execute<T>(Func<TR, bool> predicate, Func<TR, T> right, Func<TL, T> left) =>
			IsRight && predicate(_right) ? right(_right) : left(_left);
	}
}
