using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace UnitySaveTool
{
    public enum StorageBackend
    {
        Local,
        Firebase
    }

    public sealed class DataExplorer : IDataExplorer
    {
        private readonly IDataStore _localStore;
        private readonly IAsyncDataStore _localAsyncStore;
        private readonly IDataStore _firebaseStore;
        private readonly IAsyncDataStore _firebaseAsyncStore;

        private StorageBackend _backend;
        private string _userId;
        private string _sceneFolder;

        public DataExplorer(IDataStore localStore, IAsyncDataStore localAsyncStore,
                            IDataStore firebaseStore, IAsyncDataStore firebaseAsyncStore)
        {
            if (localStore == null) throw new ArgumentNullException(nameof(localStore));
            if (localAsyncStore == null) throw new ArgumentNullException(nameof(localAsyncStore));
            if (firebaseStore == null) throw new ArgumentNullException(nameof(firebaseStore));
            if (firebaseAsyncStore == null) throw new ArgumentNullException(nameof(firebaseAsyncStore));

            _localStore = localStore;
            _localAsyncStore = localAsyncStore;
            _firebaseStore = firebaseStore;
            _firebaseAsyncStore = firebaseAsyncStore;

            _backend = StorageBackend.Local;
            _userId = string.Empty;
            _sceneFolder = ResolveActiveSceneName();
        }

        public void UseLocal()
        {
            _backend = StorageBackend.Local;
        }

        public void UseFirebase()
        {
            _backend = StorageBackend.Firebase;
        }

        public void SetUser(string userId)
        {
            _userId = string.IsNullOrWhiteSpace(userId) ? string.Empty : userId;
        }

        public void NotifySceneChanged(string sceneName)
        {
            _sceneFolder = string.IsNullOrWhiteSpace(sceneName) ? "UnknownScene" : sceneName;
        }

        public StorageBackend GetBackend()
        {
            return _backend;
        }

        public string GetUser()
        {
            return _userId;
        }

        public string GetSceneFolder()
        {
            return _sceneFolder;
        }

        public void Save(object objectToSave)
        {
            IDataStore store = SelectSyncStore();
            string[] chain = BuildFolderChain();
            store.Save(objectToSave, chain);
        }

        public void SaveAll(Dictionary<Type, object> objectsToSave)
        {
            IDataStore store = SelectSyncStore();
            string[] chain = BuildFolderChain();
            store.SaveAll(objectsToSave, chain);
        }

        public object Load(Type objectType)
        {
            IDataStore store = SelectSyncStore();
            string[] chain = BuildFolderChain();
            object result = store.Load(objectType, chain);
            return result;
        }

        public Dictionary<Type, object> LoadAll()
        {
            IDataStore store = SelectSyncStore();
            string[] chain = BuildFolderChain();
            Dictionary<Type, object> result = store.LoadAll(chain);
            return result;
        }

        public async UniTask SaveAsync(object objectToSave)
        {
            IAsyncDataStore store = SelectAsyncStore();
            string[] chain = BuildFolderChain();
            await store.SaveAsync(objectToSave, chain);
        }

        public async UniTask SaveAllAsync(Dictionary<Type, object> objectsToSave)
        {
            IAsyncDataStore store = SelectAsyncStore();
            string[] chain = BuildFolderChain();
            await store.SaveAllAsync(objectsToSave, chain);
        }

        public async UniTask<object> LoadAsync(Type objectType)
        {
            IAsyncDataStore store = SelectAsyncStore();
            string[] chain = BuildFolderChain();
            object result = await store.LoadAsync(objectType, chain);
            return result;
        }

        public async UniTask<Dictionary<Type, object>> LoadAllAsync()
        {
            IAsyncDataStore store = SelectAsyncStore();
            string[] chain = BuildFolderChain();
            Dictionary<Type, object> result = await store.LoadAllAsync(chain);
            return result;
        }

        private IDataStore SelectSyncStore()
        {
            return _backend == StorageBackend.Firebase ? _firebaseStore : _localStore;
        }

        private IAsyncDataStore SelectAsyncStore()
        {
            return _backend == StorageBackend.Firebase ? _firebaseAsyncStore : _localAsyncStore;
        }

        private string[] BuildFolderChain()
        {
            List<string> chain = new List<string>();
            AppendIfNotEmpty(chain, _userId);
            AppendIfNotEmpty(chain, _sceneFolder);
            return chain.ToArray();
        }

        private void AppendIfNotEmpty(List<string> list, string value)
        {
            if (!string.IsNullOrWhiteSpace(value)) list.Add(value);
        }

        private string ResolveActiveSceneName()
        {
            string name = SceneManager.GetActiveScene().name;
            return string.IsNullOrWhiteSpace(name) ? "UnknownScene" : name;
        }
    }
}
