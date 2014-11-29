using Cinemapark.Lib.Entities;
using System.IO.IsolatedStorage;

namespace Cinemapark
{
    class AppSettings
    {
        #region Properties

        private readonly IsolatedStorageSettings _settings;

        #endregion //Properties

        #region .ctor

        public AppSettings()
        {
            _settings = IsolatedStorageSettings.ApplicationSettings;
        }

        #endregion //.ctor

        #region Multiplex

        private const string MultiplexKey = "MultiplexKey";

        /// <summary>
        /// Gets or sets Multiplex
        /// </summary>
        public Multiplex Multiplex
        {
            get { return GetValueOrDefault<Multiplex>(MultiplexKey, null); }
            set
            {
                if (AddOrUpdateValue(MultiplexKey, value))
                    Save();
            }
        }

        #endregion

        #region Helper methods

        private bool AddOrUpdateValue(string key, object value)
        {
            var valueChanged = false;

            if (_settings.Contains(key))
            {
                if (_settings[key] != value)
                {
                    _settings[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                _settings.Add(key, value);
                valueChanged = true;
            }
            return valueChanged;
        }

        private T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;

            if (_settings.Contains(key))
            {
                value = (T)_settings[key];
            }
            else
            {
                value = defaultValue;
            }
            return value;
        }

        private void Save()
        {
            _settings.Save();
        }

        #endregion //Helper methods
    }
}
