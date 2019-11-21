using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OneSky.Services.Util;

namespace OneSky.Services.Tests.Basic.Core
{

    [TestFixture]
    public class ConfigurationTest
    {
        private const string ConfigFilename = "OneSky.Services.config";
        private const string StoredConfigFilename = ConfigFilename + ".orig";
        private const int DelayTimeForFileOperation = 1500;

        [SetUp]
        public void StoreConfig()
        {

            if (File.Exists(StoredConfigFilename))
            {
                File.Delete(StoredConfigFilename);
            }

            if (File.Exists(ConfigFilename))
            {
                File.Move(ConfigFilename, StoredConfigFilename);
            }
        }


        [TearDown]
        public void RestoreConfig()
        {
            if (File.Exists(ConfigFilename))
            {
                File.Delete(ConfigFilename);
            }

            if (File.Exists(StoredConfigFilename))
            {
                File.Move(StoredConfigFilename, ConfigFilename);
            }

            Thread.Sleep(DelayTimeForFileOperation);

            Networking.Init();
        }

        [Test] 
        public void TestConfigurationFileNotPresent()
        {
            if(File.Exists(ConfigFilename)){
                File.Delete(ConfigFilename);
            }

            Assert.Throws<ConfigurationErrorsException>(() => Networking.Init());
        }

        [Test]        
        public void TestConfigurationFileApiKeyNotPresent()
        {
            StringBuilder configFile = new StringBuilder();
            configFile.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            configFile.AppendLine("<configuration>");
            configFile.AppendLine("<appSettings>");
            configFile.AppendLine("<add key=\"BaseUrl\" value=\"https://saas.agi.com\"/>");
            configFile.AppendLine("</appSettings>");
            configFile.AppendLine("</configuration>");

            File.WriteAllText(ConfigFilename, configFile.ToString());
            Assert.Throws<ConfigurationErrorsException>(()=>Networking.Init());
        }

        [Test]        
        public void TestConfigurationFileBaseUrlNotPresent()
        {
            StringBuilder configFile = new StringBuilder();
            configFile.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            configFile.AppendLine("<configuration>");
            configFile.AppendLine("<appSettings>");
            configFile.AppendLine("<add key=\"ApiKey\" value=\"12345\"/>");
            configFile.AppendLine("</appSettings>");
            configFile.AppendLine("</configuration>");

            File.WriteAllText(ConfigFilename, configFile.ToString());
            Assert.Throws<ConfigurationErrorsException>(()=>Networking.Init());
        }

         [Test]        
        public void TestConfigurationFileNoSettingsPresent()
        {
            StringBuilder configFile = new StringBuilder();
            configFile.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            configFile.AppendLine("<configuration>");
            configFile.AppendLine("</configuration>");

            File.WriteAllText(ConfigFilename, configFile.ToString());
            Assert.Throws<ConfigurationErrorsException>(()=>Networking.Init());
        }
    }
}