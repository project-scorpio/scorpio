using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Options
{
    /// <summary>
    /// Implementation of IOptionsFactory.
    /// </summary>
    /// <typeparam name="TOptions">The type of options being requested.</typeparam>
    public class OptionsFactory<TOptions> : IOptionsFactory<TOptions> where TOptions : class, new()
    {
        private readonly IEnumerable<IConfigureOptions<TOptions>> _setups;
        private readonly IEnumerable<IPostConfigureOptions<TOptions>> _postConfigures;
        private readonly IEnumerable<IPreConfigureOptions<TOptions>> _preConfigureOptions;


        /// <summary>
        /// Initializes a new instance with the specified options configurations.
        /// </summary>
        /// <param name="setups">The configuration actions to run.</param>
        /// <param name="postConfigures">The initialization actions to run.</param>
        /// <param name="preConfigureOptions"></param>
        public OptionsFactory(IEnumerable<IConfigureOptions<TOptions>> setups, IEnumerable<IPostConfigureOptions<TOptions>> postConfigures, IEnumerable<IPreConfigureOptions<TOptions>>  preConfigureOptions)
        {
            _setups = setups;
            _postConfigures = postConfigures;
            _preConfigureOptions = preConfigureOptions;
        }

        /// <summary>
        /// Returns a configured TOptions instance with the given name.
        /// </summary>
        public TOptions Create(string name)
        {
            var options = new TOptions();
            foreach (var post in _preConfigureOptions)
            {
                post.PreConfigure(name, options);
            }
            foreach (var setup in _setups)
            {
                if (setup is IConfigureNamedOptions<TOptions> namedSetup)
                {
                    namedSetup.Configure(name, options);
                }
                else if (name == Microsoft.Extensions.Options.Options.DefaultName)
                {
                    setup.Configure(options);
                }
            }
            foreach (var post in _postConfigures)
            {
                post.PostConfigure(name, options);
            }

            return options;
        }
    }
}
