using System;

namespace HrManagementSystem.Core.GuidGenerator
{
	/// <summary>
	/// RFC 4122 UUID based.
	/// </summary>
	/// <seealso href="https://www.rfc-editor.org/rfc/rfc4122.html"></seealso>
	public class TimeBasedGuidGenerator : ITimeBasedGuidGenerator
	{
		#region Private Fields

		private const byte _nodeByte = 10;
		private const int _variantByte = 8;
		private const int _versionByte = 7;
		private const int _byteArraySize = 16;
		private const byte _timestampByte = 0;
		private byte[] NodeBytes { get; set; }
		private const int _versionByteShift = 4;
		private const int _variantByteMask = 0x3f;
		private const int _versionByteMask = 0x0f;
		private const int _variantByteShift = 0x80;
		private const byte _guidClockSequenceByte = 8;
		private readonly DateTimeOffset _gregorianCalendarStart = new(1582, 10, 15, 0, 0, 0, TimeSpan.Zero);

		#endregion

		public TimeBasedGuidGenerator()
		{
			NodeBytes = GenerateNodeBytes();
		}

		public Guid GenerateTimeBasedGuid(DateTime dateTime)
		{
			return GenerateTimeBasedGuid(dateTime, GenerateClockSequenceBytes(dateTime), NodeBytes);
		}

		public DateTime GetDateTimeFromGuid(Guid guid)
		{
			return GetDateTimeOffset(guid).DateTime;
		}

		private DateTimeOffset GetDateTimeOffset(Guid guid)
		{
			byte[] bytes = guid.ToByteArray();

			bytes[_versionByte] &= _versionByteMask;
			bytes[_versionByte] |= (byte)GuidVersion.TimeBased >> _versionByteShift;

			byte[] timestampBytes = new byte[8];
			Array.Copy(bytes, _timestampByte, timestampBytes, 0, 8);

			long timestamp = BitConverter.ToInt64(timestampBytes, 0);
			long ticks = timestamp + _gregorianCalendarStart.Ticks;

			return new DateTimeOffset(ticks, TimeSpan.Zero);
		}

		private static byte[] GenerateNodeBytes()
		{
			var node = new byte[6];
			var random = new Random();
			random.NextBytes(node);

			return node;
		}

		private static byte[] GenerateClockSequenceBytes(DateTime dt)
		{
			var utc = dt.ToUniversalTime();
			return GenerateClockSequenceBytes(utc.Ticks);
		}

		private static byte[] GenerateClockSequenceBytes(long ticks)
		{
			var bytes = BitConverter.GetBytes(ticks);

			if (bytes.Length == 0)
			{
				return new byte[] { 0x0, 0x0 };
			}

			if (bytes.Length == 1)
			{
				return new byte[] { 0x0, bytes[0] };
			}

			return new byte[] { bytes[0], bytes[1] };
		}

		private Guid GenerateTimeBasedGuid(DateTime dateTime, byte[] clockSequence, byte[] node)
		{
			return GenerateTimeBasedGuid(new DateTimeOffset(dateTime), clockSequence, node);
		}

		private Guid GenerateTimeBasedGuid(DateTimeOffset dateTime, byte[] clockSequence, byte[] node)
		{
			if (clockSequence == null)
			{
				throw new ArgumentNullException(nameof(clockSequence));
			}

			if (node == null)
			{
				throw new ArgumentNullException(nameof(node));
			}

			if (clockSequence.Length != 2)
			{
				throw new ArgumentOutOfRangeException(nameof(clockSequence), "The clockSequence must be 2 bytes.");
			}

			if (node.Length != 6)
			{
				throw new ArgumentOutOfRangeException(nameof(node), "The node must be 6 bytes.");
			}

			long ticks = (dateTime - _gregorianCalendarStart).Ticks;
			byte[] guid = new byte[_byteArraySize];
			byte[] timestamp = BitConverter.GetBytes(ticks);

			Array.Copy(node, 0, guid, _nodeByte, Math.Min(6, node.Length));
			Array.Copy(clockSequence, 0, guid, _guidClockSequenceByte, Math.Min(2, clockSequence.Length));
			Array.Copy(timestamp, 0, guid, _timestampByte, Math.Min(8, timestamp.Length));

			guid[_variantByte] &= _variantByteMask;
			guid[_variantByte] |= _variantByteShift;

			guid[_versionByte] &= _versionByteMask;
			guid[_versionByte] |= (byte)GuidVersion.TimeBased << _versionByteShift;

			return new Guid(guid);
		}
	}

	public enum GuidVersion
	{
		TimeBased = 0x01,
		Reserved = 0x02,
		NameBased = 0x03,
		Random = 0x04
	}
}