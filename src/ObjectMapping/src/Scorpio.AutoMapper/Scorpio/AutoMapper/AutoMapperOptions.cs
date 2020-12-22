using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Scorpio.AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoMapperOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Action<IAutoMapperConfigurationContext>> Configurators { get; }

        /// <summary>
        /// 
        /// </summary>
        public ITypeList<Profile> ValidatingProfiles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public AutoMapperOptions()
        {
            Configurators = new List<Action<IAutoMapperConfigurationContext>>();
            ValidatingProfiles = new TypeList<Profile>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModule"></typeparam>
        /// <param name="validate"></param>
        public void AddMaps<TModule>(bool validate = false)
        {
            var assembly = typeof(TModule).Assembly;

            Configurators.Add(context =>
            {
                context.MapperConfiguration.AddMaps(assembly);
            });

            if (validate)
            {
                var profileTypes = assembly
                    .DefinedTypes
                    .Where(type => typeof(Profile).IsAssignableFrom(type) && !type.IsAbstract && !type.IsGenericType);

                foreach (var profileType in profileTypes)
                {
                    ValidatingProfiles.Add(profileType);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="validate"></param>
        public void AddProfile<TProfile>(bool validate = false)
            where TProfile : Profile, new()
        {
            Configurators.Add(context =>
            {
                context.MapperConfiguration.AddProfile<TProfile>();
            });

            if (validate)
            {
                ValidateProfile(typeof(TProfile));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProfile"></typeparam>
        /// <param name="validate"></param>
        public void ValidateProfile<TProfile>(bool validate = true)
            where TProfile : Profile
        {
            ValidateProfile(typeof(TProfile), validate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileType"></param>
        /// <param name="validate"></param>
        public void ValidateProfile(Type profileType, bool validate = true)
        {
            if (validate)
            {
                ValidatingProfiles.AddIfNotContains(profileType);
            }
            else
            {
                ValidatingProfiles.Remove(profileType);
            }
        }
    }
}