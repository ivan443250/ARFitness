using System;
using System.Collections.Generic;

namespace UnitySaveTool.Test
{
    public class GlobalData
    {
        public static Context Global => _context;
        private static readonly Context _context;

        static GlobalData()
        {
            _context = new();
            _context.RegisterInstance<ISaveToolBindInstaller>(new SaveToolBindInstaller(_context, _context.Resolve<IDataExplorer>()));
        }
    }

    public class Context : IDIContainer
    {
        private readonly Dictionary<Type, object> _objectContainer;

        private IProvider _provider;

        public Context() 
        {
            _objectContainer = new();
        }

        public void RegisterInstance<TInterface>(TInterface instance)
        {
            Type type = typeof(TInterface);

            if (_objectContainer.ContainsKey(type))
                throw new Exception();

            _objectContainer.Add(type, instance);
        }

        public bool HasBinding(Type type)
        {
            return _objectContainer.ContainsKey(type);
        }

        public T Resolve<T>() where T : class
        {
            Type type = typeof(T);

            if (_provider != null && _provider.HasInstance(type))
                return _provider.GetInstance(type) as T;

            return _objectContainer[typeof(T)] as T;
        }

        public void RegisterProvdier(IProvider provider)
        {
            _provider = provider;
        }
    }
}
