using System;
using System.Diagnostics.CodeAnalysis;

namespace Gilazo.Core
{
	public readonly struct StringGuid : IEquatable<StringGuid>
	{
		private readonly string _guid;

		private StringGuid(Guid guid) => _guid = guid.ToString();

		public static StringGuid NewGuid() => new StringGuid(Guid.NewGuid());

		public static implicit operator StringGuid(Guid guid) => new StringGuid(guid);

		public static implicit operator StringGuid(string guid) => new StringGuid(Guid.Parse(guid));

		public static implicit operator Guid(StringGuid guid) => Guid.Parse(guid._guid);

		public static implicit operator string(StringGuid guid) => guid._guid;

		public static bool operator ==(StringGuid left, StringGuid right) => Equals(left, right);

		public static bool operator !=(StringGuid left, StringGuid right) => !Equals(left, right);

		public override bool Equals(object? obj) => (obj is StringGuid other) && Equals(other);

		public bool Equals([AllowNull] StringGuid other) => _guid.Equals(other._guid);

		public override int GetHashCode() => _guid.GetHashCode();

		public override string ToString() => _guid;
	}
}
