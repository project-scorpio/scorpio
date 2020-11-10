using System;

namespace Scorpio.BackgroundJobs
{
    /// <summary>
    /// The type of background job arguments helper 
    /// </summary>
    public static class BackgroundJobArgsHelper
    {
        /// <summary>
        /// Get a <see cref="Type"/> object that represent the type arguments of a type of background job or the type parameters of a background job definition.
        /// </summary>
        /// <param name="jobType">The type of background job.</param>
        /// <returns>Return a <see cref="Type"/> object that represent the type arguments of a background job type.</returns>
        /// <exception cref="ScorpioException">The current type is not a background job type.</exception>
        public static Type GetJobArgsType(Type jobType)
        {
            foreach (var @interface in jobType.GetInterfaces())
            {
                if (!@interface.IsGenericType)
                {
                    continue;
                }

                if (@interface.GetGenericTypeDefinition() != typeof(IBackgroundJob<>) &&
                    @interface.GetGenericTypeDefinition() != typeof(IAsyncBackgroundJob<>))
                {
                    continue;
                }

                var genericArgs = @interface.GetGenericArguments();


                return genericArgs[0];
            }

            throw new ScorpioException($"Could not find type of the job args. Ensure that given type implements the {typeof(IBackgroundJob<>).AssemblyQualifiedName} or {typeof(IAsyncBackgroundJob<>).AssemblyQualifiedName} interface. Given job type: {jobType.AssemblyQualifiedName}");
        }
    }
}