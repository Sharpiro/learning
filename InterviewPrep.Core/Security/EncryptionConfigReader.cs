using System;
using System.Configuration;

namespace InterviewPrep.Core.Security
{
    public static class EncryptionConfigReader
    {
        public static string GetAppSetting(string key)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var appSeting = config.AppSettings.Settings[key];
            var appSetingValue = appSeting?.Value;
            if (string.IsNullOrEmpty(appSetingValue)) return null;

            string configItemValue;
            var wasEncrypted = GetConfigItem(key, appSetingValue, out configItemValue);
            if (wasEncrypted) return configItemValue;

            var newSetting = new KeyValueConfigurationElement(appSeting.Key, configItemValue);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(newSetting);
            config.Save(ConfigurationSaveMode.Modified);

            return appSetingValue;
        }

        public static string GetConnectionString(string key)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connection = config.ConnectionStrings.ConnectionStrings[key];
            var connectionString = connection?.ConnectionString;
            if (string.IsNullOrEmpty(connectionString)) return null;


            string configItemValue;
            var wasEncrypted = GetConfigItem(key, connectionString, out configItemValue);
            if (wasEncrypted) return configItemValue;

            var newConnection = new ConnectionStringSettings
            {
                Name = connection.Name,
                ConnectionString = configItemValue,
                ProviderName = connection.ProviderName
            };
            config.ConnectionStrings.ConnectionStrings.Remove(key);
            config.ConnectionStrings.ConnectionStrings.Add(newConnection);
            config.Save(ConfigurationSaveMode.Modified);

            return connectionString;
        }

        private static bool GetConfigItem(string key, string value, out string configValue)
        {
            configValue = null;
            string plainText;
            string cryptoText;

            var successfullyDecrypted = TryGetPlainText(value, out plainText);
            if (successfullyDecrypted)
            {
                configValue = plainText;
                return true;
            }

            var successfullyEncrypted = TryGetCypherText(value, out cryptoText);
            if (!successfullyEncrypted)
                throw new ArgumentException($"An error occurred while encrypting config item: {key}");

            configValue = cryptoText;
            return false;
        }

        private static bool TryGetCypherText(string value, out string cryptoText)
        {
            cryptoText = null;
            try
            {
                var aesFacade = AesFacade.GetAesFacade();
                var cryptoBytes = aesFacade.Encrypt(value);
                cryptoText = Convert.ToBase64String(cryptoBytes);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool TryGetPlainText(string value, out string plainText)
        {
            plainText = null;
            try
            {
                var aesFacade = AesFacade.GetAesFacade();
                var cryptoBytes = Convert.FromBase64String(value);
                plainText = aesFacade.Decrypt(cryptoBytes);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}