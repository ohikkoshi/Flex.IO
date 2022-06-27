using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Flex.IO
{
	public class FlexReader : IDisposable
	{
		MemoryStream stream;


		/// <summary>
		/// 
		/// </summary>
		/// <value></value>
		FlexReader() { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="buffer"></param>
		/// <returns></returns>
		public FlexReader(byte[] buffer) => stream = new MemoryStream(buffer);

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		~FlexReader() => Dispose();

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
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="length"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool TryRead(int length, out byte[] data)
		{
			data = new byte[length];

			return (stream.Read(data, 0, length) == length);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public byte Byte()
		{
			if (TryRead(sizeof(byte), out var data)) {
				return data[0];
			}

			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public short Short()
		{
			if (TryRead(sizeof(short), out var data)) {
				return BitConverter.ToInt16(data, 0);
			}

			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int Int()
		{
			if (TryRead(sizeof(int), out var data)) {
				return BitConverter.ToInt32(data, 0);
			}

			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public long Long()
		{
			if (TryRead(sizeof(long), out var data)) {
				return BitConverter.ToInt64(data, 0);
			}

			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Float()
		{
			if (TryRead(sizeof(float), out var data)) {
				return BitConverter.ToSingle(data, 0);
			}

			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double Double()
		{
			if (TryRead(sizeof(double), out var data)) {
				return BitConverter.ToDouble(data, 0);
			}

			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Bool()
		{
			if (TryRead(sizeof(bool), out var data)) {
				return BitConverter.ToBoolean(data, 0);
			}

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string String()
		{
			int length = Int();

			if (TryRead(length, out var data)) {
				return Encoding.UTF8.GetString(data);
			}

			return null;
		}
	}
}
