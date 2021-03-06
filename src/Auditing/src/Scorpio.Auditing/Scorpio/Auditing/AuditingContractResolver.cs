﻿using System;
using System.Collections.Generic;
using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    internal class AuditingContractResolver : CamelCasePropertyNamesContractResolver
    {
        private readonly IList<Type> _ignoredTypes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ignoredTypes"></param>
        public AuditingContractResolver(IList<Type> ignoredTypes) => _ignoredTypes = ignoredTypes;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="member"></param>
        /// <param name="memberSerialization"></param>
        /// <returns></returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (member.IsDefined(typeof(DisableAuditingAttribute)) || member.IsDefined(typeof(JsonIgnoreAttribute)))
            {
                property.ShouldSerialize = instance => false;
            }

            foreach (var ignoredType in _ignoredTypes)
            {
                if (ignoredType.GetTypeInfo().IsAssignableFrom(property.PropertyType))
                {
                    property.ShouldSerialize = instance => false;
                    break;
                }
            }

            return property;
        }
    }
}
