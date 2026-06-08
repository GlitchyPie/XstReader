namespace XstReader.Exporter.CompatabilityWrappers

{
    internal abstract class StorageAdapterBase
    {


        public abstract bool TryGetStorage(string name, out StorageAdapter? storage);
        public abstract bool TryGetStream(string name, out CfbStreamAdapter? stream);


        public abstract StorageAdapterBase AddStorage(string name);

        public abstract CfbStreamAdapter AddStream(string name);
        public abstract void AddData(string name, byte[] data);


        public abstract StorageAdapterBase GetStorage(string name);

        public abstract CfStreamAdapterBase GetStream(string name);

    }
}
