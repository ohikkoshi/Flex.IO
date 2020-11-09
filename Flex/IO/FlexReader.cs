using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Flex.IO
{
	public class FlexReader : IDisposable
	{
		MemoryStream stream;


		private FlexReader() { }
		public FlexReader(byte[] buffer) => stream = new MemoryStream(buffer);
		~FlexReader() => Dispose();

		public void Dispose()
		{
			if (stream != null) {
				stream.Close();
				stream = null;
			}
		}

		public void Reset()
		{
			Debug.Assert(stream != null);

			stream.Position = 0L;
		}

		public bool TryRead(int length, out byte[] data)
		{
			Debug.Assert(stream != null);

			data = new byte[length];

			return (stream.Read(data, 0, length) == length);
		}

		public byte Byte()
		{
			if (TryRead(sizeof(byte), out byte[] data)) {
				return data[0];
			}

			return 0;
		}

		public short Short()
		{
			if (TryRead(sizeof(short), out byte[] data)) {
				return BitConverter.ToInt16(data, 0);
			}

			return 0;
		}

		public int Int()
		{
			if (TryRead(sizeof(int), out byte[] data)) {
				return BitConverter.ToInt32(data, 0);
			}

			return 0;
		}

		public long Long()
		{
			if (TryRead(sizeof(long), out byte[] data)) {
				return BitConverter.ToInt64(data, 0);
			}

			return 0;
		}

		public float Float()
		{
			if (TryRead(sizeof(float), out byte[] data)) {
				return BitConverter.ToSingle(data, 0);
			}

			return 0;
		}

		public double Double()
		{
			if (TryRead(sizeof(double), out byte[] data)) {
				return BitConverter.ToDouble(data, 0);
			}

			return 0;
		}

		public bool Bool()
		{
			if (TryRead(sizeof(bool), out byte[] data)) {
				return BitConverter.ToBoolean(data, 0);
			}

			return false;
		}

		public string String()
		{
			int length = Int();

			if (TryRead(length, out byte[] data)) {
				return Encoding.UTF8.GetString(data);
			}

			return null;
		}
	}
}
