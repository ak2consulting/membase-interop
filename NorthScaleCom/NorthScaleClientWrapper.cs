using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using NorthScale.Store;
using NorthScale.Store.Configuration;
using Enyim.Caching.Memcached;
using System.Web.Configuration;
using System.Configuration;
using System.IO;

namespace NorthScale.Store.Interop
{
	[Guid("e6dfee2d-80f3-4d12-8370-aca16b7f3bdd"), ClassInterface(ClassInterfaceType.None)]
	[ProgId("NorthScale.Store.Interop.NorthScaleClient")]
	public class NorthScaleClientWrapper : INorthScaleClientWrapper
	{
		private NorthScaleClient nsc;

		public NorthScaleClientWrapper(INorthScaleClientConfiguration config) : this(config, null) { }

		public NorthScaleClientWrapper(INorthScaleClientConfiguration config, string bucketName)
		{
			this.nsc = new NorthScaleClient(config, bucketName);
		}

		public void Dispose()
		{
			this.nsc.Dispose();
			this.nsc = null;
		}

		object INorthScaleClientWrapper.Get(string key)
		{
			return this.nsc.Get(key);
		}

		bool INorthScaleClientWrapper.Add(string key, object value)
		{
			return this.nsc.Store(StoreMode.Add, key, value);
		}

		bool INorthScaleClientWrapper.Set(string key, object value)
		{
			return this.nsc.Store(StoreMode.Set, key, value);
		}

		bool INorthScaleClientWrapper.Replace(string key, object value)
		{
			return this.nsc.Store(StoreMode.Replace, key, value);
		}

		bool INorthScaleClientWrapper.AddWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.nsc.Store(StoreMode.Add, key, value, expiresAt);
		}

		bool INorthScaleClientWrapper.SetWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.nsc.Store(StoreMode.Set, key, value, expiresAt);
		}

		bool INorthScaleClientWrapper.ReplaceWithExpiration(string key, object value, DateTime expiresAt)
		{
			return this.nsc.Store(StoreMode.Replace, key, value, expiresAt);
		}

		bool INorthScaleClientWrapper.Remove(string key)
		{
			return this.nsc.Remove(key);
		}

		ulong INorthScaleClientWrapper.Increment(string key, ulong defaultValue, ulong delta)
		{
			return this.nsc.Increment(key, defaultValue, delta);
		}

		ulong INorthScaleClientWrapper.IncrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt)
		{
			return this.nsc.Increment(key, defaultValue, delta, expiresAt);
		}

		ulong INorthScaleClientWrapper.Decrement(string key, ulong defaultValue, ulong delta)
		{
			return this.nsc.Decrement(key, defaultValue, delta);
		}

		ulong INorthScaleClientWrapper.DecrementWithExpiration(string key, ulong defaultValue, ulong delta, DateTime expiresAt)
		{
			return this.nsc.Decrement(key, defaultValue, delta, expiresAt);
		}

		bool INorthScaleClientWrapper.Append(string key, byte[] data)
		{
			return this.nsc.Append(key, new ArraySegment<byte>(data));
		}

		bool INorthScaleClientWrapper.Prepend(string key, byte[] data)
		{
			return this.nsc.Prepend(key, new ArraySegment<byte>(data));
		}

		void INorthScaleClientWrapper.FlushAll()
		{
			this.nsc.FlushAll();
		}
	}
}
