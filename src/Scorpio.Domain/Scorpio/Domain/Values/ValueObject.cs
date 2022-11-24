using System.Linq;
using System.Reflection;

namespace Scorpio.Data.Values
{
    /// <summary>
    /// Base class for value objects.
    /// </summary>
    /// <typeparam name="TValueObject">The type of the value object.</typeparam>
    public abstract class ValueObject<TValueObject>
        where TValueObject : ValueObject<TValueObject>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            var publicProperties = GetType().GetTypeInfo().GetProperties();
            if (!publicProperties.Any())
            {
                return true;
            }
            return publicProperties.All(property => Equals(property.GetValue(this, null), property.GetValue(obj, null)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {

            var index = 1;
            const int initialHasCode = 31;

            var publicProperties = GetType().GetTypeInfo().GetProperties();

            if (!publicProperties.Any())
            {
                return initialHasCode;
            }

            var hashCode = initialHasCode;
            var changeMultiplier = false;

            foreach (var property in publicProperties)
            {
                var value = property.GetValue(this, null);

                if (value == null)
                {
                    hashCode ^= (index++ * 13);
                    continue;
                }

                hashCode = (hashCode * (changeMultiplier ? 59 : 114)) + value.GetHashCode();
                changeMultiplier = !changeMultiplier;
            }

            return hashCode;
        }


    }
}
