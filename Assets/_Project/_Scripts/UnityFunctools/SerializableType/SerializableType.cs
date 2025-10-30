using System;
using UnityEditor;
using UnityEngine;

namespace UnityFunctools
{
    [Serializable]
    public class SerializableType
    {
#if UNITY_EDITOR
        public const string ScriptField = nameof(_script);
        public const string ClassInfoField = nameof(_classInfo);

        [SerializeField] private MonoScript _script;
#endif
        [HideInInspector]
        [SerializeField]
        private string _classInfo;

        public SerializableType() { }

        public SerializableType(Type type)
        {
            SetValue(type);
        }

        public Type GetValue()
        {
            if (_classInfo == null)
                throw new NullReferenceException();

            return Type.GetType(_classInfo);
        }

        public void SetValue(Type type)
        {
            _classInfo = $"{type.FullName}, {type.Assembly.FullName}";
        }

        public override int GetHashCode()
        {
            return GetValue()?.GetHashCode() ?? 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is not SerializableType otherType)
                return false;

            return GetValue() == otherType.GetValue();
        }

        public static bool operator ==(SerializableType type1, SerializableType type2)
        {
            return type1.Equals(type2);
        }

        public static bool operator !=(SerializableType type1, SerializableType type2)
        {
            return !type1.Equals(type2);
        }
    }
}