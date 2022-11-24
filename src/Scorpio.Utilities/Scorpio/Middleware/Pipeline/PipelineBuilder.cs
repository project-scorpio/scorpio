using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Middleware.Pipeline
{
    /// <summary>
    /// 
    /// </summary>
    public static class PipelineBuilder
    {
        private static readonly MethodInfo _getServiceInfo = ((Func<IServiceProvider, Type, object>)PipelineBuilder.GetService).Method!;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPipelineContext"></typeparam>
        /// <param name="app"></param>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public static IPipelineBuilder<TPipelineContext> Use<TPipelineContext>(this IPipelineBuilder<TPipelineContext> app, Func<TPipelineContext, Func<Task>, Task> middleware)
        {
            return app.Use(next =>
            {
                return context =>
                {
                    Func<Task> simpleNext = () => next(context);
                    return middleware(context, simpleNext);
                };
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPipelineContext"></typeparam>
        /// <param name="app"></param>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public static IPipelineBuilder<TPipelineContext> Use<TPipelineContext>(this IPipelineBuilder<TPipelineContext> app, Func<TPipelineContext, PipelineRequestDelegate<TPipelineContext>, Task> middleware)
        {
            return app.Use(next => context => middleware(context, next));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPipelineContext"></typeparam>
        /// <typeparam name="TMiddleware"></typeparam>
        /// <param name="builder"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IPipelineBuilder<TPipelineContext> UseMiddleware<TPipelineContext, TMiddleware>(this IPipelineBuilder<TPipelineContext> builder, params object[] args) => UseMiddleware(builder, typeof(TMiddleware), args);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="middlewareType"></param>
        /// <param name="args"></param>
        public static IPipelineBuilder<TPipelineContext> UseMiddleware<TPipelineContext>(this IPipelineBuilder<TPipelineContext> builder, Type middlewareType, params object[] args)
        {
            Check.NotNull(builder, nameof(builder));
            Check.NotNull(middlewareType, nameof(middlewareType));
            builder.Use(next =>
           {
               var methods = middlewareType.GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(m => m.Name.IsIn("Invoke", "InvokeAsync")).ToArray();
               if (methods.Length > 1)
               {
                   throw new InvalidOperationException();
               }

               if (methods.Length == 0)
               {
                   throw new InvalidOperationException();
               }
               var methodinfo = methods[0];
               if (!typeof(Task).IsAssignableFrom(methodinfo.ReturnType))
               {
                   throw new InvalidOperationException();
               }
               var parameters = methodinfo.GetParameters();
               if (parameters.Length == 0 || parameters[0].ParameterType != typeof(TPipelineContext))
               {
                   throw new InvalidOperationException();
               }

               var ctorArgs = new object[args.Length + 1];
               ctorArgs[0] = next;
               Array.Copy(args, 0, ctorArgs, 1, args.Length);
               var instance = ActivatorUtilities.CreateInstance(builder.ApplicationServices, middlewareType, ctorArgs);
               if (parameters.Length == 1)
               {
                   return (PipelineRequestDelegate<TPipelineContext>)methodinfo.CreateDelegate(typeof(PipelineRequestDelegate<TPipelineContext>), instance);
               }

               var factory = Compile<object, TPipelineContext>(methodinfo, parameters);

               return context =>
               {
                   var serviceProvider = builder.ApplicationServices;
                   if (serviceProvider == null)
                   {
                       throw new InvalidOperationException();
                   }

                   return factory(instance, context, serviceProvider);
               };
           });
            return builder;
        }

        private static Func<T, TPipelineContext, IServiceProvider, Task> Compile<T, TPipelineContext>(MethodInfo methodinfo, ParameterInfo[] parameters)
        {
            var middleware = typeof(T);
            var httpContextArg = Expression.Parameter(typeof(TPipelineContext), "context");
            var providerArg = Expression.Parameter(typeof(IServiceProvider), "serviceProvider");
            var instanceArg = Expression.Parameter(middleware, "middleware");

            var methodArguments = new Expression[parameters.Length];
            methodArguments[0] = httpContextArg;
            for (var i = 1; i < parameters.Length; i++)
            {
                var parameterType = parameters[i].ParameterType;
                if (parameterType.IsByRef)
                {
                    throw new NotSupportedException();
                }

                var parameterTypeExpression = new Expression[]
                {
                    providerArg,
                    Expression.Constant(parameterType, typeof(Type))
                };

                var getServiceCall = Expression.Call(_getServiceInfo, parameterTypeExpression);
                methodArguments[i] = Expression.Convert(getServiceCall, parameterType);
            }

            Expression middlewareInstanceArg = instanceArg;
            if (methodinfo.DeclaringType != typeof(T))
            {
                middlewareInstanceArg = Expression.Convert(middlewareInstanceArg, methodinfo.DeclaringType);
            }

            var body = Expression.Call(middlewareInstanceArg, methodinfo, methodArguments);

            var lambda = Expression.Lambda<Func<T, TPipelineContext, IServiceProvider, Task>>(body, instanceArg, httpContextArg, providerArg);

            return lambda.Compile();
        }
        private static object GetService(IServiceProvider sp, Type type)
        {
            var service = sp.GetRequiredService(type);
            return service;
        }
    }
}
