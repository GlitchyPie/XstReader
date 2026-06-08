using OpenMcdf;

namespace XstReader.Exporter.CompatabilityWrappers

{
    internal class StorageAdapter : StorageAdapterBase
    {
        internal Storage _store;

        public StorageAdapter(Storage root)
        {
            _store = root;
        }

        public override bool TryGetStorage(string name, out StorageAdapter? storage)
        {
            bool r = _store.TryOpenStorage(name, out Storage? store);
            if (r)
            {
                storage = new StorageAdapter(store);
                return true;
            }
            else if (store != null)
            {
                storage = new StorageAdapter(store);
            }
            else
            {
                storage = null;
            }
            return false;
        }
        public override bool TryGetStream(string name, out CfbStream stream)
        {
            return _store.TryOpenStream(name, out stream);
        }

        public override StorageAdapter AddStorage(string name)
        {
            return new StorageAdapter(_store.CreateStorage(name));
        }
        public override CfbStream AddStream(string name)
        {
            return _store.CreateStream(name);
        }
        public override void AddData(string name, byte[] data)
        {
            using CfbStream stream = _store.CreateStream(name);
            stream.Write(data, 0 , data.Length);
            stream.Flush();
        }

        public override StorageAdapter GetStorage(string name)
        {
            return new StorageAdapter(_store.OpenStorage(name));
        }
        public override CfbStream GetStream(string name)
        {
            return _store.OpenStream(name);
        }

    }
}
