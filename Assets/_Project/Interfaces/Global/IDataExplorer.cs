using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace UnitySaveTool
{
    public interface IDataExplorer
    {
        void UseLocal();
        void UseFirebase();

        void SetUser(string userId);
        void NotifySceneChanged(string sceneName);

        StorageBackend GetBackend();
        string GetUser();
        string GetSceneFolder();

        void Save(object objectToSave);
        void SaveAll(Dictionary<Type, object> objectsToSave);
        object Load(Type objectType);
        Dictionary<Type, object> LoadAll();

        UniTask SaveAsync(object objectToSave);
        UniTask SaveAllAsync(Dictionary<Type, object> objectsToSave);
        UniTask<object> LoadAsync(Type objectType);
        UniTask<Dictionary<Type, object>> LoadAllAsync();
    }
}
