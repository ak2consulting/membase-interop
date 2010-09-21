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
	[Guid("d420f99c-c96c-4032-9d9c-6ca5f4ae9b20"), ClassInterface(ClassInterfaceType.None)]
	[ProgId("NorthScale.Store.Interop.NorthScaleClientFactory")]
	public class NorthScaleClientWrapperFactory : INorthScaleClientWrapperFactory
	{
		INorthScaleClientWrapper INorthScaleClientWrapperFactory.Create(string configPath)
		{
			return ((INorthScaleClientWrapperFactory)this).CreateWithBucket(configPath, null);
		}

		INorthScaleClientWrapper INorthScaleClientWrapperFactory.CreateWithBucket(string configPath, string bucketName)
		{
			var config = this.Load(configPath, null);

			return new NorthScaleClientWrapper(config, bucketName);
		}

		private INorthScaleClientConfiguration Load(string path, string sectionName)
		{
			//System.Diagnostics.Debugger.Break();

			if (!File.Exists(path)) throw new InvalidOperationException("The config file '" + path + "' cannot be found.");
			var cfm = new ConfigurationFileMap();
			cfm.MachineConfigFilename = path;

			var cfg = ConfigurationManager.OpenMappedMachineConfiguration(cfm);
			if (cfg == null) if (!File.Exists(path)) throw new InvalidOperationException("The config file '" + path + "' cannot be found.");

			if (String.IsNullOrEmpty(sectionName))
				sectionName = "northscale";

			var section = cfg.GetSection(sectionName) as INorthScaleClientConfiguration;
			if (section == null) if (!File.Exists(path)) throw new InvalidOperationException("The config section '" + sectionName + "' cannot be found.");

			return section;
		}
	}
}
