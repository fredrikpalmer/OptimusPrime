using System;
using System.Collections.Generic;

namespace OptimusPrime.Cli.Config
{
    internal class DefaultApplicationConfiguration : IApplicationConfiguration
    {
        private readonly IDictionary<string, string> _dictionary;

        public DefaultApplicationConfiguration(IDictionary<string,string> dictionary)
        {
            _dictionary = dictionary;
        }

        public string GetValue(string key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            _dictionary.TryGetValue(key, out string value);

            return value;
        }
    }
}