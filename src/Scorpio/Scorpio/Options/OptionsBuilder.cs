using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Options
{
    /// <summary>
    /// 
    /// </summary>
    public  class OptionsBuilder<TOptions>:Microsoft.Extensions.Options.OptionsBuilder<TOptions> where TOptions:class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="name"></param>
        public OptionsBuilder(IServiceCollection services, string name) : base(services, name)
        {
        }

        /// <summary>
        /// Registers an action used to configure a particular type of options.
        /// Note: These are run after all <seealso cref="Microsoft.Extensions.Options.OptionsBuilder{TOptions}.Configure(Action{TOptions})"/>
        /// </summary>
        /// <param name="configureOptions">The action used to configure the options.</param>
        public virtual OptionsBuilder<TOptions> PreConfigure(Action<TOptions> configureOptions)
        {
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            Services.AddSingleton<IPreConfigureOptions<TOptions>>(new PreConfigureOptions<TOptions>(Name, configureOptions));
            return this;
        }

        /// <summary>
        /// Registers an action used to post configure a particular type of options.
        /// Note: These are run before after <seealso cref="Microsoft.Extensions.Options.OptionsBuilder{TOptions}.Configure(Action{TOptions})"/>.
        /// </summary>
        /// <typeparam name="TDep">The dependency used by the action.</typeparam>
        /// <param name="configureOptions">The action used to configure the options.</param>
        /// <returns>The current OptionsBuilder.</returns>
        public virtual OptionsBuilder<TOptions> PreConfigure<TDep>(Action<TOptions, TDep> configureOptions)
            where TDep : class
        {
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            Services.AddTransient<IPreConfigureOptions<TOptions>>(sp =>
                new PreConfigureOptions<TOptions, TDep>(Name, sp.GetRequiredService<TDep>(), configureOptions));
            return this;
        }

        /// <summary>
        /// Registers an action used to post configure a particular type of options.
        /// Note: These are run before after <seealso cref="Microsoft.Extensions.Options.OptionsBuilder{TOptions}.Configure(Action{TOptions})"/>.
        /// </summary>
        /// <typeparam name="TDep1">The first dependency used by the action.</typeparam>
        /// <typeparam name="TDep2">The second dependency used by the action.</typeparam>
        /// <param name="configureOptions">The action used to configure the options.</param>
        /// <returns>The current OptionsBuilder.</returns>
        public virtual OptionsBuilder<TOptions> PreConfigure<TDep1, TDep2>(Action<TOptions, TDep1, TDep2> configureOptions)
            where TDep1 : class
            where TDep2 : class
        {
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            Services.AddTransient<IPreConfigureOptions<TOptions>>(sp =>
                new PreConfigureOptions<TOptions, TDep1, TDep2>(Name, sp.GetRequiredService<TDep1>(), sp.GetRequiredService<TDep2>(), configureOptions));
            return this;
        }

        /// <summary>
        /// Registers an action used to post configure a particular type of options.
        /// Note: These are run before after <seealso cref="Microsoft.Extensions.Options.OptionsBuilder{TOptions}.Configure(Action{TOptions})"/>.
        /// </summary>
        /// <typeparam name="TDep1">The first dependency used by the action.</typeparam>
        /// <typeparam name="TDep2">The second dependency used by the action.</typeparam>
        /// <typeparam name="TDep3">The third dependency used by the action.</typeparam>
        /// <param name="configureOptions">The action used to configure the options.</param>
        /// <returns>The current OptionsBuilder.</returns>
        public virtual OptionsBuilder<TOptions> PreConfigure<TDep1, TDep2, TDep3>(Action<TOptions, TDep1, TDep2, TDep3> configureOptions)
            where TDep1 : class
            where TDep2 : class
            where TDep3 : class
        {
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            Services.AddTransient<IPreConfigureOptions<TOptions>>(
                sp => new PreConfigureOptions<TOptions, TDep1, TDep2, TDep3>(
                    Name,
                    sp.GetRequiredService<TDep1>(),
                    sp.GetRequiredService<TDep2>(),
                    sp.GetRequiredService<TDep3>(),
                    configureOptions));
            return this;
        }

        /// <summary>
        /// Registers an action used to post configure a particular type of options.
        /// Note: These are run before after <seealso cref="Microsoft.Extensions.Options.OptionsBuilder{TOptions}.Configure(Action{TOptions})"/>.
        /// </summary>
        /// <typeparam name="TDep1">The first dependency used by the action.</typeparam>
        /// <typeparam name="TDep2">The second dependency used by the action.</typeparam>
        /// <typeparam name="TDep3">The third dependency used by the action.</typeparam>
        /// <typeparam name="TDep4">The fourth dependency used by the action.</typeparam>
        /// <param name="configureOptions">The action used to configure the options.</param>
        /// <returns>The current OptionsBuilder.</returns>
        public virtual OptionsBuilder<TOptions> PreConfigure<TDep1, TDep2, TDep3, TDep4>(Action<TOptions, TDep1, TDep2, TDep3, TDep4> configureOptions)
            where TDep1 : class
            where TDep2 : class
            where TDep3 : class
            where TDep4 : class
        {
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            Services.AddTransient<IPreConfigureOptions<TOptions>>(
                sp => new PreConfigureOptions<TOptions, TDep1, TDep2, TDep3, TDep4>(
                    Name,
                    sp.GetRequiredService<TDep1>(),
                    sp.GetRequiredService<TDep2>(),
                    sp.GetRequiredService<TDep3>(),
                    sp.GetRequiredService<TDep4>(),
                    configureOptions));
            return this;
        }

        /// <summary>
        /// Registers an action used to post configure a particular type of options.
        /// Note: These are run before after <seealso cref="Microsoft.Extensions.Options.OptionsBuilder{TOptions}.Configure(Action{TOptions})"/>.
        /// </summary>
        /// <typeparam name="TDep1">The first dependency used by the action.</typeparam>
        /// <typeparam name="TDep2">The second dependency used by the action.</typeparam>
        /// <typeparam name="TDep3">The third dependency used by the action.</typeparam>
        /// <typeparam name="TDep4">The fourth dependency used by the action.</typeparam>
        /// <typeparam name="TDep5">The fifth dependency used by the action.</typeparam>
        /// <param name="configureOptions">The action used to configure the options.</param>
        /// <returns>The current OptionsBuilder.</returns>
        public virtual OptionsBuilder<TOptions> PreConfigure<TDep1, TDep2, TDep3, TDep4, TDep5>(Action<TOptions, TDep1, TDep2, TDep3, TDep4, TDep5> configureOptions)
            where TDep1 : class
            where TDep2 : class
            where TDep3 : class
            where TDep4 : class
            where TDep5 : class
        {
            if (configureOptions == null)
            {
                throw new ArgumentNullException(nameof(configureOptions));
            }

            Services.AddTransient<IPreConfigureOptions<TOptions>>(
                sp => new PreConfigureOptions<TOptions, TDep1, TDep2, TDep3, TDep4, TDep5>(
                    Name,
                    sp.GetRequiredService<TDep1>(),
                    sp.GetRequiredService<TDep2>(),
                    sp.GetRequiredService<TDep3>(),
                    sp.GetRequiredService<TDep4>(),
                    sp.GetRequiredService<TDep5>(),
                    configureOptions));
            return this;
        }
    }
}
