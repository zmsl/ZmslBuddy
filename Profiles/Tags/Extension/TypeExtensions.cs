using System;
using System.ComponentModel;
using System.Reflection;

namespace ZmslBuddy.Profiles.Tags.Extension
{
    public static class TypeExtensions
    {
        private const BindingFlags Flags = BindingFlags.Instance
                                       | BindingFlags.Public
                                       | BindingFlags.NonPublic;

        /// <summary>
        /// Gets a private field value out of the specified instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static T GetPrivateField<T>(this object instance, string fieldName)
        {
            PropertyInfo propertyInfo = null;
            var type = instance.GetType();
            do
            {
                propertyInfo = type.GetProperty(fieldName, Flags);
                type = type.BaseType;
            }
            while (propertyInfo == null && type != null);

            // Throw if field does not exist
            if (propertyInfo == null)
            {
                throw new MissingMemberException(instance.GetType().FullName, fieldName);
            }

            // Throw if the field type is not equal to the specified type
            var propertyType = propertyInfo.PropertyType;
            if (!(propertyType == typeof(T)))
            {
                throw new InvalidCastException(string.Format("Field of type {0} cannot be cast to {1}", propertyType.FullName, typeof(T).FullName));
            }

            return (T) propertyInfo.GetValue(instance);
        }
    }
}
