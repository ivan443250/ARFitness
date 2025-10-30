using System;

namespace UnityFunctools
{
    public struct DisposableObject : IDisposable
    {
        private Action _disposeCallback;

        public DisposableObject(Action disposeCallback)
        {
            _disposeCallback = disposeCallback;
        }

        public void Dispose()
        {
            _disposeCallback?.Invoke();
        }

        public static DisposableObject operator +(DisposableObject obj1, DisposableObject obj2)
        {
            return obj1 + (IDisposable)obj2;
        }

        public static DisposableObject operator +(DisposableObject disposableObj, IDisposable disposable)
        {
            Action newDisposeCallback = disposableObj._disposeCallback;
            newDisposeCallback += disposable.Dispose;

            return new DisposableObject(newDisposeCallback);
        }
    }
}