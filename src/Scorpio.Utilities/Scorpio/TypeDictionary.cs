using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public class TypeDictionary : TypeDictionary<object, object>, ITypeDictionary
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBaseKeyType"></typeparam>
    /// <typeparam name="TBaseValueType"></typeparam>
    public class TypeDictionary<TBaseKeyType, TBaseValueType> : ITypeDictionary<TBaseKeyType, TBaseValueType>
    {
        private readonly Dictionary<Type, Type> _pairs;

        /// <summary>
        /// 
        /// </summary>
        public TypeDictionary() => _pairs = new Dictionary<Type, Type>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Type this[Type key]
        {
            get => _pairs[key];
            set
            {
                CheckKeyType(key);
                CheckValueType(value);
                _pairs[key] = value;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public ICollection<Type> Keys => _pairs.Keys;

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Type> Values => _pairs.Values;

        /// <summary>
        /// 
        /// </summary>
        public int Count => _pairs.Count;

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        /// <typeparam name="TValueType"></typeparam>
        public void Add<TKeyType, TValueType>()
            where TKeyType : TBaseKeyType
            where TValueType : TBaseValueType => Add(typeof(TKeyType), typeof(TValueType));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(Type key, Type value)
        {
            CheckKeyType(key);
            CheckValueType(value);
            _pairs.Add(key, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<Type, Type> item) => Add(item.Key, item.Value);

        /// <summary>
        /// 
        /// </summary>
        public void Clear() => _pairs.Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        /// <returns></returns>
        public bool Contains<TKeyType>() where TKeyType : TBaseKeyType => ContainsKey(typeof(TKeyType));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool ICollection<KeyValuePair<Type, Type>>.Contains(KeyValuePair<Type, Type> item) => (_pairs as ICollection<KeyValuePair<Type, Type>>).Contains(item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(Type key) => _pairs.ContainsKey(key);

        void ICollection<KeyValuePair<Type, Type>>.CopyTo(KeyValuePair<Type, Type>[] array, int arrayIndex) => (_pairs as ICollection<KeyValuePair<Type, Type>>).CopyTo(array, arrayIndex);

        IEnumerator<KeyValuePair<Type, Type>> IEnumerable<KeyValuePair<Type, Type>>.GetEnumerator() => _pairs.GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        /// <returns></returns>
        public Type GetOrDefault<TKeyType>() where TKeyType : TBaseKeyType => _pairs.GetOrDefault(typeof(TKeyType));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        public bool Remove<TKeyType>() where TKeyType : TBaseKeyType => Remove(typeof(TKeyType));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(Type key)
        {
            CheckKeyType(key);
            return _pairs.Remove(key);
        }

        bool ICollection<KeyValuePair<Type, Type>>.Remove(KeyValuePair<Type, Type> item) => (_pairs as ICollection<KeyValuePair<Type, Type>>).Remove(item);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKeyType"></typeparam>
        /// <typeparam name="TValueType"></typeparam>
        public bool TryAdd<TKeyType, TValueType>()
            where TKeyType : TBaseKeyType
            where TValueType : TBaseValueType
        {
            if (_pairs.ContainsKey(typeof(TKeyType)))
            {
                return false;
            }
            Add<TKeyType, TValueType>();
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(Type key, out Type value) => _pairs.TryGetValue(key, out value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => _pairs.GetEnumerator();

        private void CheckValueType(Type value)
        {
            if (!typeof(TBaseValueType).GetTypeInfo().IsAssignableFrom(value))
            {
                throw new ArgumentException($"Given type ({value.AssemblyQualifiedName}) should be instance of {typeof(TBaseValueType).AssemblyQualifiedName} ", nameof(value));
            }
        }

        private void CheckKeyType(Type key)
        {
            Check.NotNull(key, nameof(key));
            if (!typeof(TBaseKeyType).GetTypeInfo().IsAssignableFrom(key))
            {
                throw new ArgumentException($"Given type ({key.AssemblyQualifiedName}) should be instance of {typeof(TBaseKeyType).AssemblyQualifiedName} ", nameof(key));
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBaseValueType"></typeparam>
    public class TypeDictionary< TBaseValueType> : ITypeDictionary< TBaseValueType>
    {
        private readonly Dictionary<string, Type> _pairs;

        ///<inheritdoc/>
        public TypeDictionary() => _pairs = new Dictionary<string, Type>();
        ///<inheritdoc/>
        public Type this[string key]
        {
            get => _pairs[key];
            set
            {
                CheckValueType(value);
                _pairs[key] = value;
            }
        }



        ///<inheritdoc/>
        public ICollection<string> Keys => _pairs.Keys;

        ///<inheritdoc/>
        public ICollection<Type> Values => _pairs.Values;

        ///<inheritdoc/>
        public int Count => _pairs.Count;

        ///<inheritdoc/>
        public bool IsReadOnly => false;

        ///<inheritdoc/>
        public void Add<TValueType>(string key)
            where TValueType : TBaseValueType => Add(key, typeof(TValueType));

        ///<inheritdoc/>
        public void Add(string key, Type value)
        {
            CheckValueType(value);
            _pairs.Add(key, value);
        }

        ///<inheritdoc/>
        public void Add(KeyValuePair<string, Type> item) => Add(item.Key, item.Value);

        ///<inheritdoc/>
        public void Clear() => _pairs.Clear();

        ///<inheritdoc/>
        public bool Contains(string key)  => ContainsKey(key);

        ///<inheritdoc/>
        bool ICollection<KeyValuePair<string, Type>>.Contains(KeyValuePair<string, Type> item) => (_pairs as ICollection<KeyValuePair<string, Type>>).Contains(item);

        ///<inheritdoc/>
        public bool ContainsKey(string key) => _pairs.ContainsKey(key);

        ///<inheritdoc/>
        void ICollection<KeyValuePair<string, Type>>.CopyTo(KeyValuePair<string, Type>[] array, int arrayIndex) => (_pairs as ICollection<KeyValuePair<string, Type>>).CopyTo(array, arrayIndex);

        IEnumerator<KeyValuePair<string, Type>> IEnumerable<KeyValuePair<string, Type>>.GetEnumerator() => _pairs.GetEnumerator();

        ///<inheritdoc/>
        public Type GetOrDefault(string key) => _pairs.GetOrDefault(key);

        ///<inheritdoc/>
        public bool Remove(string key)
        {
            return _pairs.Remove(key);
        }

        ///<inheritdoc/>
        bool ICollection<KeyValuePair<string, Type>>.Remove(KeyValuePair<string, Type> item) => (_pairs as ICollection<KeyValuePair<string, Type>>).Remove(item);

        ///<inheritdoc/>
        public bool TryAdd<TValueType>(string key)
            where TValueType : TBaseValueType
        {
            if (_pairs.ContainsKey(key))
            {
                return false;
            }
            Add<TValueType>(key);
            return true;
        }

        ///<inheritdoc/>
        public bool TryGetValue(string key, out Type value) => _pairs.TryGetValue(key, out value);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => _pairs.GetEnumerator();

        private void CheckValueType(Type value)
        {
            if (!typeof(TBaseValueType).GetTypeInfo().IsAssignableFrom(value))
            {
                throw new ArgumentException($"Given type ({value.AssemblyQualifiedName}) should be instance of {typeof(TBaseValueType).AssemblyQualifiedName} ", nameof(value));
            }
        }
    }

}
