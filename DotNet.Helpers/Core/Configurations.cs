using System;
using System.Configuration;
using System.Linq;
namespace DotNet.Helpers.Core
{
    /// <summary>
    /// Class to work with application configuration.
    /// </summary>
    public class Configurations
    {
        /// <summary>
        /// Gets the config value in typed way.
        /// </summary>
        /// <typeparam name="ResultType">Type of result.</typeparam>
        /// <param name="nameofConfiguration">Key of AppSettings entry.</param>
        /// <param name="defaultValue"> Default value delegate. This will be called if there is no appSetting entry found.</param>
        /// <returns>Configuration value in <typeparamref name="ResultType"/> </returns>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// // If the config file has the value, it will return that value. Else defaultValue.
        /// string expected = "defaultValue";
        /// string actual = new Configurations().Get<string>("nameOfStringAppSetting", () => "defaultValue");
        /// Assert.AreEqual(expected, actual);
        /// ]]>
        /// </code>
        /// </example>
        public ResultType Get<ResultType>(string nameofConfiguration, Func<ResultType> defaultValue)
        {
            return GetValueAdjustedToDefaultValue<ResultType>(nameofConfiguration, defaultValue);
        }

        private static ResultType GetValueAdjustedToDefaultValue<ResultType>(string nameofConfiguration, Func<ResultType> defaultValue)
        {
            if (string.IsNullOrWhiteSpace(GetStringValue(nameofConfiguration)) && DoesKeyExists(nameofConfiguration) == false)
            {
                return defaultValue();
            }
            else
            {
                return (ResultType)Convert.ChangeType(ConfigurationManager.AppSettings[nameofConfiguration], typeof(ResultType));
            }
        }

        private static bool DoesKeyExists(string nameofConfiguration)
        {
            return ConfigurationManager.AppSettings.AllKeys.Any(name => name == nameofConfiguration);
        }

        private static string GetStringValue(string nameofConfiguration)
        {
            return ConfigurationManager.AppSettings[nameofConfiguration];
        }
    }
}
