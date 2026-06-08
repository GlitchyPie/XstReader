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
        public override bool TryGetStream(string name, out CfbStreamAdapter? stream)
        {
            bool r = _store.TryOpenStream(name, out CfbStream? str);
            if (r)
            {
                stream = new CfbStreamAdapter(str,name, _store);
                return true;
            }
            else if (str != null)
            {
                stream = new CfbStreamAdapter(str,name, _store);
            }
            else
            {
                stream = null;
            }
            return false;

        }

        public override StorageAdapter AddStorage(string name)
        {
            return new StorageAdapter(_store.CreateStorage(name));
        }
        public override CfbStreamAdapter AddStream(string name)
        {
            return new CfbStreamAdapter(_store.CreateStream(name), name, _store);
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
        public override CfStreamAdapterBase GetStream(string name)
        {
            return new CfbStreamAdapter(_store.OpenStream(name), name, _store);
        }

    }
}
