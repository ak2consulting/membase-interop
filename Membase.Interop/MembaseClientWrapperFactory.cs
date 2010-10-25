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
using Enyim;

namespace Membase.Interop
{
	[Guid("c6c892e3-76ae-4684-a449-9d90f1b3bf7a"), ClassInterface(ClassInterfaceType.None)]
	[ProgId("Membase.Interop.MembaseClientFactory")]
	public class MembaseClientWrapperFactory : IMembaseClientWrapperFactory
	{
		private static Dictionary<string, IMembaseClientWrapper> cache = new Dictionary<string, IMembaseClientWrapper>(StringComparer.OrdinalIgnoreCase);

		IMembaseClientWrapper IMembaseClientWrapperFactory.Create(string configPath)
		{
			return ((IMembaseClientWrapperFactory)this).CreateWithBucket(configPath, null);
		}

		IMembaseClientWrapper IMembaseClientWrapperFactory.CreateWithBucket(string configPath, string bucketName)
		{
			var key = configPath + "++" + bucketName;
			IMembaseClientWrapper retval;

			if (!cache.TryGetValue(key, out retval))
				lock (cache)
					if (!cache.TryGetValue(key, out retval))
					{
						var config = this.Load(configPath, null);

						cache[key] = retval = new MembaseClientWrapper(config, bucketName);
					}

			return retval;
		}

		private IMembaseClientConfiguration Load(string path, string sectionName)
		{
			//System.Diagnostics.Debugger.Break();

			if (!File.Exists(path)) throw new InvalidOperationException("The config file '" + path + "' cannot be found.");
			var cfm = new ConfigurationFileMap();
			cfm.MachineConfigFilename = path;

			var cfg = ConfigurationManager.OpenMappedMachineConfiguration(cfm);
			if (cfg == null) if (!File.Exists(path)) throw new InvalidOperationException("The config file '" + path + "' cannot be found.");

			if (String.IsNullOrEmpty(sectionName))
				sectionName = "membase";

			var section = cfg.GetSection(sectionName) as IMembaseClientConfiguration;
			if (section == null) if (!File.Exists(path)) throw new InvalidOperationException("The config section '" + sectionName + "' cannot be found.");

			return section;
		}
	}
}
