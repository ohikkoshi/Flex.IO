using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Flex.IO
{
	public class FlexWriter : IDisposable
	{
		MemoryStream stream;


		public FlexWriter() => stream = new MemoryStream();
		public FlexWriter(byte[] buffer) => stream = new MemoryStream(buffer);
		public FlexWriter(int capacity) => stream = new MemoryStream(capacity);
		~FlexWriter() => Dispose();

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
			stream.SetLength(0L);
		}

		public byte[] Flush()
		{
			Debug.Assert(stream != null);

			if (stream.TryGetBuffer(out ArraySegment<byte> buffer)) {
				Reset();
				return buffer.Array;
			}

			return null;
		}

		public void Write(byte[] bytes) => stream?.Write(bytes, 0, bytes.Length);
		public void Write(byte value) => stream?.WriteByte(value);
		public void Write(short value) => Write(BitConverter.GetBytes(value));
		public void Write(int value) => Write(BitConverter.GetBytes(value));
		public void Write(long value) => Write(BitConverter.GetBytes(value));
		public void Write(float value) => Write(BitConverter.GetBytes(value));
		public void Write(double value) => Write(BitConverter.GetBytes(value));
		public void Write(bool value) => Write(BitConverter.GetBytes(value));

		public void Write(string value)
		{
			byte[] buf = Encoding.UTF8.GetBytes(value);
			Write(buf.Length);
			Write(buf);
		}
	}
}
