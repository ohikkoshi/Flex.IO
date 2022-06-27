using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Flex.IO
{
	public class FlexWriter : IDisposable
	{
		MemoryStream stream;


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public FlexWriter() => stream = new MemoryStream();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public FlexWriter(byte[] buffer) => stream = new MemoryStream(buffer);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="capacity"></param>
		/// <returns></returns>
		public FlexWriter(int capacity) => stream = new MemoryStream(capacity);

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		~FlexWriter() => Dispose();

		/// <summary>
		/// 
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			if (stream != null) {
				stream.Close();
				stream = null;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Reset()
		{
			stream.Position = 0L;
			stream.SetLength(0L);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public byte[] Flush()
		{
			if (stream.TryGetBuffer(out var buffer)) {
				Reset();
				return buffer.Array;
			}

			return null;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bytes"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Write(byte[] bytes) => stream?.Write(bytes, 0, bytes.Length);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Write(byte value) => stream?.WriteByte(value);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Write(short value) => Write(BitConverter.GetBytes(value));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Write(int value) => Write(BitConverter.GetBytes(value));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Write(long value) => Write(BitConverter.GetBytes(value));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Write(float value) => Write(BitConverter.GetBytes(value));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Write(double value) => Write(BitConverter.GetBytes(value));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Write(bool value) => Write(BitConverter.GetBytes(value));

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Write(string value)
		{
			var buf = Encoding.UTF8.GetBytes(value);
			Write(buf.Length);
			Write(buf);
		}
	}
}
