using DotNet.Helpers.Core;
using DotNet.Helpers.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DotNet.Helpers.Tests.Core
{
    [TestClass]
    public class Configurations_Get
    {
        [TestMethod]
        public void WhenThereIsAppSettingAvailable_ReturnValue()
        {
            //Setup
            string configName = "nameOfStringAppSetting";
            string configValue = "valueOfStringAppSetting";
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[configName] != null)
            {
                config.AppSettings.Settings.Remove(configName);
            }
            config.AppSettings.Settings.Add(configName, configValue);
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");
            //End of setup

            string expected = "valueOfStringAppSetting";

            string actual = new Configurations().Get<string>("nameOfStringAppSetting", () => "");
            
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void WhenAppSettingIsNotAvailable_ReturnDefault()
        {
            string expected = "default";

            string actual = new Configurations().Get<string>("nameOfNonExistingAppSetting", () => "default");

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void WhenAppSettingIsAvailableButEmpty_ReturnEmpty()
        {
            //Setup
            string configName = "nameOfEmptyStringAppSetting";
            string configValue = "";
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[configName] != null)
            {
                config.AppSettings.Settings.Remove(configName);
            }
            config.AppSettings.Settings.Add(configName, configValue);
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");
            //End of setup

            string expected = "";

            string actual = new Configurations().Get<string>(configName, () => "default");

            Assert.AreEqual(expected, actual);
        }
    }
}
