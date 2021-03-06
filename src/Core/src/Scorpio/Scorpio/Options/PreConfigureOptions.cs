﻿using System;
namespace Scorpio.Options
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    public class PreConfigureOptions<TOptions> : IPreConfigureOptions<TOptions> where TOptions : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="action"></param>
        internal PreConfigureOptions(string name, Action<TOptions> action)
        {
            Name = name;
            Action = action;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public Action<TOptions> Action { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public virtual void PreConfigure(string name, TOptions options)
        {
            Check.NotNull(options, nameof(options));

            // Null name is used to initialize all named options.
            if (Name == null || name == Name)
            {
                Action?.Invoke(options);
            }
        }

        /// <summary>
        /// Invoked to configure a TOptions instance using the <see cref="Microsoft.Extensions.Options.Options.DefaultName"/>.
        /// </summary>
        /// <param name="options">The options instance to configured.</param>
        public void PreConfigure(TOptions options) => PreConfigure(Microsoft.Extensions.Options.Options.DefaultName, options);
    }

    /// <summary>
    /// Implementation of IPreConfigureOptions.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="TDep"></typeparam>
    public class PreConfigureOptions<TOptions, TDep> : IPreConfigureOptions<TOptions>
        where TOptions : class
        where TDep : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the options.</param>
        /// <param name="dependency">A dependency.</param>
        /// <param name="action">The action to register.</param>
        internal PreConfigureOptions(string name, TDep dependency, Action<TOptions, TDep> action)
        {
            Name = name;
            Action = action;
            Dependency = dependency;
        }

        /// <summary>
        /// The options name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The configuration action.
        /// </summary>
        public Action<TOptions, TDep> Action { get; }

        /// <summary>
        /// The dependency.
        /// </summary>
        public TDep Dependency { get; }

        /// <summary>
        /// Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configured.</param>
        public virtual void PreConfigure(string name, TOptions options)
        {
            Check.NotNull(options, nameof(options));

            // Null name is used to configure all named options.
            if (Name == null || name == Name)
            {
                Action?.Invoke(options, Dependency);
            }
        }

        /// <summary>
        /// Invoked to configure a TOptions instance using the <see cref="Microsoft.Extensions.Options.Options.DefaultName"/>.
        /// </summary>
        /// <param name="options">The options instance to configured.</param>
        public void PreConfigure(TOptions options) => PreConfigure(Microsoft.Extensions.Options.Options.DefaultName, options);
    }

    /// <summary>
    /// Implementation of IPreConfigureOptions.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="TDep1"></typeparam>
    /// <typeparam name="TDep2"></typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    public class PreConfigureOptions<TOptions, TDep1, TDep2> : IPreConfigureOptions<TOptions>
        where TOptions : class
        where TDep1 : class
        where TDep2 : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the options.</param>
        /// <param name="dependency">A dependency.</param>
        /// <param name="dependency2">A second dependency.</param>
        /// <param name="action">The action to register.</param>
        internal PreConfigureOptions(string name, TDep1 dependency, TDep2 dependency2, Action<TOptions, TDep1, TDep2> action)
        {
            Name = name;
            Action = action;
            Dependency1 = dependency;
            Dependency2 = dependency2;
        }

        /// <summary>
        /// The options name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The configuration action.
        /// </summary>
        public Action<TOptions, TDep1, TDep2> Action { get; }

        /// <summary>
        /// The first dependency.
        /// </summary>
        public TDep1 Dependency1 { get; }

        /// <summary>
        /// The second dependency.
        /// </summary>
        public TDep2 Dependency2 { get; }

        /// <summary>
        /// Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configured.</param>
        public virtual void PreConfigure(string name, TOptions options)
        {
            Check.NotNull(options, nameof(options));
            // Null name is used to configure all named options.
            if (Name == null || name == Name)
            {
                Action?.Invoke(options, Dependency1, Dependency2);
            }
        }

        /// <summary>
        /// Invoked to configure a TOptions instance using the <see cref="Microsoft.Extensions.Options.Options.DefaultName"/>.
        /// </summary>
        /// <param name="options">The options instance to configured.</param>
        public void PreConfigure(TOptions options) => PreConfigure(Microsoft.Extensions.Options.Options.DefaultName, options);
    }

    /// <summary>
    /// Implementation of IPreConfigureOptions.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="TDep1"></typeparam>
    /// <typeparam name="TDep2"></typeparam>
    /// <typeparam name="TDep3"></typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    public class PreConfigureOptions<TOptions, TDep1, TDep2, TDep3> : IPreConfigureOptions<TOptions>
        where TOptions : class
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the options.</param>
        /// <param name="dependency">A dependency.</param>
        /// <param name="dependency2">A second dependency.</param>
        /// <param name="dependency3">A third dependency.</param>
        /// <param name="action">The action to register.</param>
        internal PreConfigureOptions(string name, TDep1 dependency, TDep2 dependency2, TDep3 dependency3, Action<TOptions, TDep1, TDep2, TDep3> action)
        {
            Name = name;
            Action = action;
            Dependency1 = dependency;
            Dependency2 = dependency2;
            Dependency3 = dependency3;
        }

        /// <summary>
        /// The options name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The configuration action.
        /// </summary>
        public Action<TOptions, TDep1, TDep2, TDep3> Action { get; }

        /// <summary>
        /// The first dependency.
        /// </summary>
        public TDep1 Dependency1 { get; }

        /// <summary>
        /// The second dependency.
        /// </summary>
        public TDep2 Dependency2 { get; }

        /// <summary>
        /// The third dependency.
        /// </summary>
        public TDep3 Dependency3 { get; }

        /// <summary>
        /// Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configured.</param>
        public virtual void PreConfigure(string name, TOptions options)
        {
            Check.NotNull(options, nameof(options));

            // Null name is used to configure all named options.
            if (Name == null || name == Name)
            {
                Action?.Invoke(options, Dependency1, Dependency2, Dependency3);
            }
        }

        /// <summary>
        /// Invoked to configure a TOptions instance using the <see cref="Microsoft.Extensions.Options.Options.DefaultName"/>.
        /// </summary>
        /// <param name="options">The options instance to configured.</param>
        public void PreConfigure(TOptions options) => PreConfigure(Microsoft.Extensions.Options.Options.DefaultName, options);
    }

    /// <summary>
    /// Implementation of IPreConfigureOptions.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="TDep1"></typeparam>
    /// <typeparam name="TDep2"></typeparam>
    /// <typeparam name="TDep3"></typeparam>
    /// <typeparam name="TDep4"></typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    public class PreConfigureOptions<TOptions, TDep1, TDep2, TDep3, TDep4> : IPreConfigureOptions<TOptions>
        where TOptions : class
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the options.</param>
        /// <param name="dependency1">A dependency.</param>
        /// <param name="dependency2">A second dependency.</param>
        /// <param name="dependency3">A third dependency.</param>
        /// <param name="dependency4">A fourth dependency.</param>
        /// <param name="action">The action to register.</param>
        internal PreConfigureOptions(string name, TDep1 dependency1, TDep2 dependency2, TDep3 dependency3, TDep4 dependency4, Action<TOptions, TDep1, TDep2, TDep3, TDep4> action)
        {
            Name = name;
            Action = action;
            Dependency1 = dependency1;
            Dependency2 = dependency2;
            Dependency3 = dependency3;
            Dependency4 = dependency4;
        }

        /// <summary>
        /// The options name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The configuration action.
        /// </summary>
        public Action<TOptions, TDep1, TDep2, TDep3, TDep4> Action { get; }

        /// <summary>
        /// The first dependency.
        /// </summary>
        public TDep1 Dependency1 { get; }

        /// <summary>
        /// The second dependency.
        /// </summary>
        public TDep2 Dependency2 { get; }

        /// <summary>
        /// The third dependency.
        /// </summary>
        public TDep3 Dependency3 { get; }

        /// <summary>
        /// The fourth dependency.
        /// </summary>
        public TDep4 Dependency4 { get; }

        /// <summary>
        /// Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configured.</param>
        public virtual void PreConfigure(string name, TOptions options)
        {
            Check.NotNull(options, nameof(options));
            // Null name is used to configure all named options.
            if (Name == null || name == Name)
            {
                Action?.Invoke(options, Dependency1, Dependency2, Dependency3, Dependency4);
            }
        }

        /// <summary>
        /// Invoked to configure a TOptions instance using the <see cref="Microsoft.Extensions.Options.Options.DefaultName"/>.
        /// </summary>
        /// <param name="options">The options instance to configured.</param>
        public void PreConfigure(TOptions options) => PreConfigure(Microsoft.Extensions.Options.Options.DefaultName, options);
    }


    /// <summary>
    /// Implementation of IPreConfigureOptions.
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <typeparam name="TDep1"></typeparam>
    /// <typeparam name="TDep2"></typeparam>
    /// <typeparam name="TDep3"></typeparam>
    /// <typeparam name="TDep4"></typeparam>
    /// <typeparam name="TDep5"></typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2436:Types and methods should not have too many generic parameters", Justification = "<挂起>")]
    public class PreConfigureOptions<TOptions, TDep1, TDep2, TDep3, TDep4, TDep5> : IPreConfigureOptions<TOptions>
        where TOptions : class
        where TDep1 : class
        where TDep2 : class
        where TDep3 : class
        where TDep4 : class
        where TDep5 : class
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the options.</param>
        /// <param name="dependency1">A dependency.</param>
        /// <param name="dependency2">A second dependency.</param>
        /// <param name="dependency3">A third dependency.</param>
        /// <param name="dependency4">A fourth dependency.</param>
        /// <param name="dependency5">A fifth dependency.</param>
        /// <param name="action">The action to register.</param>
        internal PreConfigureOptions(string name, TDep1 dependency1, TDep2 dependency2, TDep3 dependency3, TDep4 dependency4, TDep5 dependency5, Action<TOptions, TDep1, TDep2, TDep3, TDep4, TDep5> action)
        {
            Name = name;
            Action = action;
            Dependency1 = dependency1;
            Dependency2 = dependency2;
            Dependency3 = dependency3;
            Dependency4 = dependency4;
            Dependency5 = dependency5;
        }

        /// <summary>
        /// The options name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The configuration action.
        /// </summary>
        public Action<TOptions, TDep1, TDep2, TDep3, TDep4, TDep5> Action { get; }

        /// <summary>
        /// The first dependency.
        /// </summary>
        public TDep1 Dependency1 { get; }

        /// <summary>
        /// The second dependency.
        /// </summary>
        public TDep2 Dependency2 { get; }

        /// <summary>
        /// The third dependency.
        /// </summary>
        public TDep3 Dependency3 { get; }

        /// <summary>
        /// The fourth dependency.
        /// </summary>
        public TDep4 Dependency4 { get; }

        /// <summary>
        /// The fifth dependency.
        /// </summary>
        public TDep5 Dependency5 { get; }

        /// <summary>
        /// Invoked to configure a TOptions instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configured.</param>
        public virtual void PreConfigure(string name, TOptions options)
        {
            Check.NotNull(options, nameof(options));

            // Null name is used to configure all named options.
            if (Name == null || name == Name)
            {
                Action?.Invoke(options, Dependency1, Dependency2, Dependency3, Dependency4, Dependency5);
            }
        }

        /// <summary>
        /// Invoked to configure a TOptions instance using the <see cref="Microsoft.Extensions.Options.Options.DefaultName"/>.
        /// </summary>
        /// <param name="options">The options instance to configured.</param>
        public void PreConfigure(TOptions options) => PreConfigure(Microsoft.Extensions.Options.Options.DefaultName, options);
    }

}
