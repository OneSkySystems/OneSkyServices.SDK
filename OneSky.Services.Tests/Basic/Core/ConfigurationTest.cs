using System;
using System.IO;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OneSky.Services.Util;

namespace OneSky.Services.Tests.Basic.Core
{
    [TestFixture,Explicit]
    public class ConfigurationTest
    {
        [Test] 
        public void TestConfigurationFileNotPresent()
        {
            if(File.Exists("OneSky.Services.config.test")){
                File.Delete("OneSky.Services.config.test");
            }       
            if(File.Exists("OneSky.Services.config")){
                File.Move("OneSky.Services.config","OneSky.Services.config.test");
            }
            Assert.Throws<TypeInitializationException>(() => Networking.Init());
            
            if(File.Exists("OneSky.Services.config.test")){
                File.Move("OneSky.Services.config.test","OneSky.Services.config");
            }
            Thread.Sleep(1500);
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

            if(File.Exists("OneSky.Services.config.test")){
                File.Delete("OneSky.Services.config.test");
            }       

            if(File.Exists("OneSky.Services.config")){
                File.Move("OneSky.Services.config","OneSky.Services.config.test");
            }
            File.WriteAllText("OneSky.Services.config", configFile.ToString());
            Assert.Throws<TypeInitializationException>(()=>Networking.Init());

            if (File.Exists("OneSky.Services.config.test") &&
               File.Exists("OneSky.Services.config")){
                File.Delete("OneSky.Services.config");
                File.Move("OneSky.Services.config.test","OneSky.Services.config");
            }      
            Thread.Sleep(1500);
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

            if(File.Exists("OneSky.Services.config.test")){
                File.Delete("OneSky.Services.config.test");
            }       
            if(File.Exists("OneSky.Services.config")){
                File.Move("OneSky.Services.config","OneSky.Services.config.test");                
            }
            File.WriteAllText("OneSky.Services.config", configFile.ToString());
            Assert.Throws<TypeInitializationException>(()=>Networking.Init());

            if (File.Exists("OneSky.Services.config.test") &&
               File.Exists("OneSky.Services.config")){
                File.Delete("OneSky.Services.config");
                File.Move("OneSky.Services.config.test","OneSky.Services.config");
            }
            Thread.Sleep(1500);
        }

         [Test]        
        public void TestConfigurationFileNoSettingsPresent()
        {
            StringBuilder configFile = new StringBuilder();
            configFile.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            configFile.AppendLine("<configuration>");
            configFile.AppendLine("</configuration>");

            if(File.Exists("OneSky.Services.config.test")){
                File.Delete("OneSky.Services.config.test");
            }       
            if(File.Exists("OneSky.Services.config")){
                File.Move("OneSky.Services.config","OneSky.Services.config.test");
            }
            File.WriteAllText("OneSky.Services.config", configFile.ToString());
            Assert.Throws<TypeInitializationException>(()=>Networking.Init());

            if (File.Exists("OneSky.Services.config.test") &&
               File.Exists("OneSky.Services.config")){
                File.Delete("OneSky.Services.config");
                File.Move("OneSky.Services.config.test","OneSky.Services.config");
            }
            Thread.Sleep(1500);
        }
    }
}