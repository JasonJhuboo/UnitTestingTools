using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace JhubooSoft.UnitTestingTools
{
    public class AutoFakedObjectFactory<TObjectType>
        where TObjectType : class
    {
        #region Fields

        private static MethodInfo m_FakeInstanceDelegateMethodInfo;

        #endregion

        #region Properties

        internal MethodInfo FakeInstanceDelegateMethodInfo
        {
            get
            {
                if (m_FakeInstanceDelegateMethodInfo == null)
                    m_FakeInstanceDelegateMethodInfo = this.GetType().GetMethod(nameof(FakeInstanceDelegate), BindingFlags.NonPublic | BindingFlags.Static);

                return m_FakeInstanceDelegateMethodInfo;
            }
        }
        
        #endregion

        public TObjectType Create()
        {
            ConstructorInfo constructorInfo = GetConstructorInfo();
            object[] constructorParameters = GetConstructorParameters(constructorInfo);
            object constructed = constructorInfo.Invoke(constructorParameters);

            return (TObjectType)constructed;
        }

        private object[] GetConstructorParameters(ConstructorInfo constructorInfo)
        {
            ParameterInfo[] parameterInfo = constructorInfo.GetParameters();

            if (parameterInfo?.Length == 0)
                return Array.Empty<object>();

            List<object> parameters = new List<object>();
            foreach (ParameterInfo paramaterMetadata in parameterInfo)
            {
                parameters.Add(MakeFakedInstance(paramaterMetadata));
            }

            return parameters.ToArray();
        }

        private object MakeFakedInstance(ParameterInfo paramaterMetadata)
        {
            MethodInfo genericMethod = FakeInstanceDelegateMethodInfo.MakeGenericMethod(new Type[] { paramaterMetadata.ParameterType });
            return genericMethod.Invoke(this, Array.Empty<object>());
        }

        private static object FakeInstanceDelegate<TFakeType>()
            where TFakeType : class
        {
            return A.Fake<TFakeType>();
        }

        private ConstructorInfo GetConstructorInfo()
        {
            ConstructorInfo[] constructors = typeof(TObjectType).GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (constructors.Length > 1)
                throw new InvalidOperationException("Unable to auto construct an object with multiple constructors.");

            return constructors[0];
        }
    }
}