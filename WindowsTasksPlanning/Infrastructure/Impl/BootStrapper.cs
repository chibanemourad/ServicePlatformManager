using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate;
using NHibernate.Cfg;

namespace WindowsTasksPlanning.Infrastructure.Impl
{
    public class BootStrapper
	{
		public static ISessionFactory SessionFactory
		{
			get;
			private set;
		}

		private const string SerializedConfiguration = "configurtion.serialized";
		private const string ConfigFile = "hibernate.cfg.xml";

		private static Configuration Configuration { get; set; }

		public static void Initialize()
		{
		    Configuration = LoadConfigurationFromFile();
			if(Configuration == null)
			{
				Configuration = new Configuration().Configure(ConfigFile);
				SaveConfigurationToFile(Configuration);
			}
			
			var intercepter = new DataBindingIntercepter();
			SessionFactory = Configuration
				.SetInterceptor(intercepter)
				.BuildSessionFactory();
			intercepter.SessionFactory = SessionFactory;
		}

		private static bool IsConfigurationFileValid
		{
			get
			{
				var assembly = Assembly.GetCallingAssembly();
			    var configInfo = new FileInfo(SerializedConfiguration);
				var assemblyInfo = new FileInfo(assembly.Location);
				var configFileInfo = new FileInfo(ConfigFile);
			    if (configInfo.LastWriteTime < assemblyInfo.LastWriteTime)
			    {
			        return false;
                }
				return configInfo.LastWriteTime >= configFileInfo.LastWriteTime;
			}
		}

		private static void SaveConfigurationToFile(Configuration configuration)
		{
			using(var file = File.Open(SerializedConfiguration, FileMode.Create))
			{
				var bf = new BinaryFormatter();
				bf.Serialize(file, configuration);
			}
		}

		private static Configuration LoadConfigurationFromFile()
		{
		    if (IsConfigurationFileValid == false)
		    {
		        return null;
            }
			try
			{
				using(var file = File.Open(SerializedConfiguration, FileMode.Open))
				{
					var bf = new BinaryFormatter();
					return bf.Deserialize(file) as Configuration;
				}
			}
			catch (Exception)
			{
				return null;
			}

		}
	}
}