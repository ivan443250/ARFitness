using Firebase.Firestore;
using Zenject;

namespace UnitySaveTool.Advanced
{
    public class SaveToolProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SetupSaveSystem();
        }

        private void SetupSaveSystem()
        {
            JsonUtilityDataConverter jsonUtilityDataConverter = new JsonUtilityDataConverter();

            FileSystem localStore = new FileSystem(jsonUtilityDataConverter);
            FirebaseDataStore firebaseDataStore = new FirebaseDataStore(FirebaseFirestore.DefaultInstance); //todo

            Container
                .Bind<IDataExplorer>()
                .To<DataExplorer>()
                .AsSingle()
                .WithArguments(localStore, localStore, firebaseDataStore, firebaseDataStore);
        }
    }
}