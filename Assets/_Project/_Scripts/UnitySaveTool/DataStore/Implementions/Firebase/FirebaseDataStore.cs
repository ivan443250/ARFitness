using Cysharp.Threading.Tasks;
using Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnitySaveTool
{
    public sealed class FirebaseDataStore : IDataStore, IAsyncDataStore
    {
        private readonly FirebaseFirestore _db;
        private readonly string _rootCollectionPrefix;
        private readonly Type[] _knownTypes;

        public FirebaseDataStore(
            FirebaseFirestore db = null,
            string rootCollectionPrefix = "appdata",
            IEnumerable<Type> knownTypes = null)
        {
            _db = db ?? FirebaseFirestore.DefaultInstance;
            _rootCollectionPrefix = string.IsNullOrWhiteSpace(rootCollectionPrefix) ? "appdata" : rootCollectionPrefix;
            _knownTypes = knownTypes != null ? knownTypes.ToArray() : Array.Empty<Type>();
        }

        public void Save(object objectToSave, params string[] folders)
        {
            SaveAsync(objectToSave, folders).AsTask().GetAwaiter().GetResult();
        }

        public void SaveAll(Dictionary<Type, object> objectsToSave, params string[] folders)
        {
            SaveAllAsync(objectsToSave, folders).AsTask().GetAwaiter().GetResult();
        }

        public object Load(Type objectType, params string[] folders)
        {
            return LoadAsync(objectType, folders).AsTask().GetAwaiter().GetResult();
        }

        public Dictionary<Type, object> LoadAll(params string[] folders)
        {
            return LoadAllAsync(folders).AsTask().GetAwaiter().GetResult();
        }

        public async UniTask SaveAsync(object objectToSave, params string[] folders)
        {
            if (objectToSave == null) throw new ArgumentNullException(nameof(objectToSave));
            Type type = objectToSave.GetType();
            DocumentReference doc = GetDocumentReference(type, folders);
            await doc.SetAsync(objectToSave, SetOptions.MergeAll);
        }

        public async UniTask SaveAllAsync(Dictionary<Type, object> objectsToSave, params string[] folders)
        {
            if (objectsToSave == null) throw new ArgumentNullException(nameof(objectsToSave));
            WriteBatch batch = _db.StartBatch();
            foreach (KeyValuePair<Type, object> kv in objectsToSave)
            {
                Type type = kv.Key;
                object value = kv.Value;
                DocumentReference doc = GetDocumentReference(type, folders);
                batch.Set(doc, value, SetOptions.MergeAll);
            }
            await batch.CommitAsync();
        }

        public async UniTask<object> LoadAsync(Type objectType, params string[] folders)
        {
            if (objectType == null) throw new ArgumentNullException(nameof(objectType));
            DocumentReference doc = GetDocumentReference(objectType, folders);
            DocumentSnapshot snap = await doc.GetSnapshotAsync();
            if (!snap.Exists) return null;
            return ConvertSnapshotToType(snap, objectType);
        }

        public async UniTask<Dictionary<Type, object>> LoadAllAsync(params string[] folders)
        {
            Dictionary<Type, object> result = new Dictionary<Type, object>();
            for (int i = 0; i < _knownTypes.Length; i++)
            {
                Type t = _knownTypes[i];
                object obj = await LoadAsync(t, folders);
                if (obj != null) result[t] = obj;
            }
            return result;
        }

        private DocumentReference GetDocumentReference(Type type, params string[] folders)
        {
            CollectionReference col = _db.Collection(BuildCollectionName(type));
            string docId = BuildDocumentId(folders);
            return col.Document(docId);
        }

        private string BuildCollectionName(Type type)
        {
            return _rootCollectionPrefix + "_" + type.Name;
        }

        private string BuildDocumentId(string[] folders)
        {
            if (folders == null || folders.Length == 0) return "default";
            List<string> pieces = new List<string>();
            for (int i = 0; i < folders.Length; i++)
            {
                string s = folders[i];
                if (!string.IsNullOrWhiteSpace(s)) pieces.Add(s);
            }
            if (pieces.Count == 0) return "default";
            return string.Join("__", pieces);
        }

        private object ConvertSnapshotToType(DocumentSnapshot snap, Type targetType)
        {
            MethodInfo method = typeof(DocumentSnapshot)
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .First(m => m.Name == "ConvertTo" && m.IsGenericMethodDefinition && m.GetParameters().Length == 0);
            MethodInfo generic = method.MakeGenericMethod(targetType);
            object result = generic.Invoke(snap, null);
            return result;
        }
    }
}
