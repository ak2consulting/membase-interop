using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Membase;
using Membase.Configuration;
using Enyim.Caching.Memcached;
using System.Web.Configuration;
using System.Configuration;
using System.IO;

namespace Membase.Interop
{
	[Guid("e6dfee2d-80f3-4d12-8370-aca16b7f3bdd"), ClassInterface(ClassInterfaceType.None)]
	[ProgId("Membase.Interop.MembaseClient")]
	public class MembaseClientWrapper : IMembaseClientWrapper
	{
		private MembaseClient nsc;

		public MembaseClientWrapper(IMembaseClientConfiguration config) : this(config, null) { }

		public MembaseClientWrapper(IMembaseClientConfiguration config, string bucketName)
		{
			this.nsc = new MembaseClient(config, bucketName);
		}

		public void Dispose()
		{
			this.nsc.Dispose();
			this.nsc = null;
		}

		object IMembaseClientWrapper.Get(string key)
		{
			return this.nsc.Get(key);
		}

		bool IMembaseClientWrapper.Add(string key, object value)
		{
			return this.nsc.Store(StoreMode.Add, key, value);
		}

		bool IMembaseClientWrapper.Set(string key, object value)
		{
			return this.nsc.Store(StoreMode.Set, key, value);
		}

		bool IMembaseClientWrapper.Replace(string key, object value)
		{
			return this.nsc.Store(StoreMode.Replace, key, value);
		}

		bool IMembaseClientWrapper.AddWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.nsc.Store(StoreMode.Add, key, value, expiresAt);
		}

		bool IMembaseClientWrapper.SetWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.nsc.Store(StoreMode.Set, key, value, expiresAt);
		}

		bool IMembaseClientWrapper.ReplaceWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.nsc.Store(StoreMode.Replace, key, value, expiresAt);
		}

		bool IMembaseClientWrapper.Remove(string key)
		{
			return this.nsc.Remove(key);
		}

		ulong IMembaseClientWrapper.Increment(string key, ulong defaultValue, ulong delta)
		{
			return this.nsc.Increment(key, defaultValue, delta);
		}

		ulong IMembaseClientWrapper.IncrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt)
		{
			return this.nsc.Increment(key, defaultValue, delta, expiresAt);
		}

		ulong IMembaseClientWrapper.Decrement(string key, ulong defaultValue, ulong delta)
		{
			return this.nsc.Decrement(key, defaultValue, delta);
		}

		ulong IMembaseClientWrapper.DecrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt)
		{
			return this.nsc.Decrement(key, defaultValue, delta, expiresAt);
		}

		bool IMembaseClientWrapper.Append(string key, byte[] data)
		{
			return this.nsc.Append(key, new ArraySegment<byte>(data));
		}

		bool IMembaseClientWrapper.Prepend(string key, byte[] data)
		{
			return this.nsc.Prepend(key, new ArraySegment<byte>(data));
		}

		void IMembaseClientWrapper.FlushAll()
		{
			this.nsc.FlushAll();
		}
	}
}
