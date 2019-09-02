using Scorpio.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ScorpioOptionsServiceCollectionExtensions
    {
        /// <summary>
        /// Gets an options builder that forwards Configure calls for the same <typeparamref name="TOptions"/> to the underlying service collection.
        /// </summary>
        /// <typeparam name="TOptions">The options type to be configured.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that configure calls can be chained in it.</returns>
        public static OptionsBuilder<TOptions> Options<TOptions>(this IServiceCollection services) where TOptions : class
            => services.Options<TOptions>(Microsoft.Extensions.Options.Options.DefaultName);

        /// <summary>
        /// Gets an options builder that forwards Configure calls for the same named <typeparamref name="TOptions"/> to the underlying service collection.
        /// </summary>
        /// <typeparam name="TOptions">The options type to be configured.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="name">The name of the options instance.</param>
        /// <returns>The <see cref="OptionsBuilder{TOptions}"/> so that configure calls can be chained in it.</returns>
        public static OptionsBuilder<TOptions> Options<TOptions>(this IServiceCollection services, string name)
            where TOptions : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();
            return new OptionsBuilder<TOptions>(services, name);
        }

        /// <summary>
        /// Registers an action used to initialize a particular type of options.
        /// Note: These are run after all <seealso cref="OptionsServiceCollectionExtensions.Configure{TOptions}(IServiceCollection, Action{TOptions})"/>.
        /// </summary>
        /// <typeparam name="TOptions">The options type to be configured.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configureOptions">The action used to configure the options.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection PreConfigure<TOptions>(this IServiceCollection services, Action<TOptions> configureOptions) where TOptions : class
            => services.PreConfigure(Microsoft.Extensions.Options.Options.DefaultName, configureOptions);

        /// <summary>
        /// Registers an action used to configure a particular type of options.
        /// Note: These are run after all <seealso cref="OptionsServiceCollectionExtensions.Configure{TOptions}(IServiceCollection, Action{TOptions})"/>.
        /// </summary>
        /// <typeparam name="TOptions">The options type to be configure.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="name">The name of the options instance.</param>
        /// <param name="configureOptions">The action used to configure the options.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection PreConfigure<TOptions>(this IServiceCollection services, string name, Action<TOptions> configureOptions)
            where TOptions : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            services.AddOptions();
            services.AddSingleton<IPreConfigureOptions<TOptions>>(new PreConfigureOptions<TOptions>(name, configureOptions));
            return services;
        }

        /// <summary>
        /// Registers an action used to post configure all instances of a particular type of options.
        /// Note: These are run after all <seealso cref="OptionsServiceCollectionExtensions.Configure{TOptions}(IServiceCollection, Action{TOptions})"/>.
        /// </summary>
        /// <typeparam name="TOptions">The options type to be configured.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configureOptions">The action used to configure the options.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection PreConfigureAll<TOptions>(this IServiceCollection services, Action<TOptions> configureOptions) where TOptions : class
            => services.PreConfigure(name: null, configureOptions: configureOptions);


    }
}
