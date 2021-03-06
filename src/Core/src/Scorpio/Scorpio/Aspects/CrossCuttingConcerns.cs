﻿using System;
using System.Collections.Generic;

using Scorpio.DynamicProxy;

namespace Scorpio.Aspects
{
    /// <summary>
    /// 
    /// </summary>
    public static class CrossCuttingConcerns
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="concerns"></param>
        public static void AddApplied(object obj, params string[] concerns)
        {
            Check.NotNull(obj, nameof(obj));
            if (concerns.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(concerns), $"{nameof(concerns)} should be provided!");
            }

            (obj.UnProxy() as IAvoidDuplicateCrossCuttingConcerns)?.AppliedCrossCuttingConcerns.AddRange(concerns);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="concerns"></param>
        public static void RemoveApplied(object obj, params string[] concerns)
        {
            Check.NotNull(obj, nameof(obj));

            if (concerns.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(concerns), $"{nameof(concerns)} should be provided!");
            }


            if (!(obj.UnProxy() is IAvoidDuplicateCrossCuttingConcerns crossCuttingEnabledObj))
            {
                return;
            }

            foreach (var concern in concerns)
            {
                crossCuttingEnabledObj.AppliedCrossCuttingConcerns.RemoveAll(c => c == concern);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="concern"></param>
        /// <returns></returns>
        public static bool IsApplied(object obj, string concern)
        {
            Check.NotNull(obj, nameof(obj));
            if (concern == null)
            {
                throw new ArgumentNullException(nameof(concern));
            }

            return (obj.UnProxy() as IAvoidDuplicateCrossCuttingConcerns)?.AppliedCrossCuttingConcerns.Contains(concern) ?? false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="concerns"></param>
        /// <returns></returns>
        public static IDisposable Applying(object obj, params string[] concerns)
        {
            AddApplied(obj, concerns);
            return new DisposeAction(() => RemoveApplied(obj, concerns));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string[] GetApplieds(object obj)
        {
            Check.NotNull(obj, nameof(obj));
            if (!(obj.UnProxy() is IAvoidDuplicateCrossCuttingConcerns crossCuttingEnabledObj))
            {
                return new string[0];
            }

            return crossCuttingEnabledObj.AppliedCrossCuttingConcerns.ToArray();
        }
    }
}
