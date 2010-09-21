using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NorthScale.Store;
using NorthScale.Store.Configuration;
using System.Runtime.InteropServices;

namespace NorthScale.Store.Interop
{
	[Guid("742c7d81-4c83-48b5-a59d-01565aaea33c"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	public interface INorthScaleClientWrapperFactory
	{
		[return: MarshalAs(UnmanagedType.IDispatch)]
		INorthScaleClientWrapper Create(string configPath);
		[return: MarshalAs(UnmanagedType.IDispatch)]
		INorthScaleClientWrapper CreateWithBucket(string configPath, string bucketName);
	}
}
