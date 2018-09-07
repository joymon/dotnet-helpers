using DotNet.Helpers.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace DotNet.Helpers.Tests.Core
{
    [TestClass]
    public class Configurations_Inheritance
    {
        [TestMethod]
        ///<summary>
        ///Not sure this is needed or not. but helps if someone change the Configurations static or something else.
        ///</summary>
        public void WhenAClassInheritConfigurations_ShouldWork()
        {
            //Setup
            MyAppConfigurations config = new MyAppConfigurations();
            Assert.IsInstanceOfType(config, typeof(Configurations));
        }
        [TestMethod]
        public void WhenAClassInheritConfigurations_GetShouldBeAccessiblePublicly()
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

            string actual = new MyAppConfigurations().Get<string>("nameOfStringAppSetting", () => "");

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void WhenAppSettingsAvailableAndAccessedViaChildClassMethod_ReturnValue()
        {
            //Setup
            string configName = "nameOfMyIntAppSetting";
            int configValue = 10;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[configName] != null)
            {
                config.AppSettings.Settings.Remove(configName);
            }
            config.AppSettings.Settings.Add(configName, configValue.ToString());
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");
            //End of setup

            int expected = 10;

            int actual = new MyAppConfigurations().MyIntAppSetting;

            Assert.AreEqual(expected, actual);

        }
        [TestMethod]
        public void WhenAppSettingsNotAvailableAndAccessedViaChildClassMethod_ReturnDefaultValue()
        {
            double expected = 0;

            double actual = new MyAppConfigurations().MyNonExistingDoubleAppSetting;

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
    public class MyAppConfigurations : Configurations
    {
        public int MyIntAppSetting { get { return base.Get<int>("nameOfMyIntAppSetting", () => 0); } }
        public double MyNonExistingDoubleAppSetting
        {
            get { return base.Get<int>("nameOfMyNonExistingDoubleAppSetting", () => 0); }
        }
    }
}
