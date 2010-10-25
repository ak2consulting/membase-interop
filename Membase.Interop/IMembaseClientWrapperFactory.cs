using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Membase;
using Membase.Configuration;
using System.Runtime.InteropServices;

namespace Membase.Interop
{
	[Guid("742c7d81-4c83-48b5-a59d-01565aaea33c"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface IMembaseClientWrapperFactory
	{
		[return: MarshalAs(UnmanagedType.IDispatch)]
		IMembaseClientWrapper Create(string configPath);
		[return: MarshalAs(UnmanagedType.IDispatch)]
		IMembaseClientWrapper CreateWithBucket(string configPath, string bucketName);
	}
}
