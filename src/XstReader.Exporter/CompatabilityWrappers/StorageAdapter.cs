using OpenMcdf;

namespace XstReader.Exporter.CompatabilityWrappers

{
    internal class StorageAdapter : StorageAdapterBase
    {
        public Storage Store { get; }
        public StorageAdapter(Storage root)
        {
            Store = root;
        }

        public override bool TryGetStorage(string name, out StorageAdapter? storage)
        {
            bool r = Store.TryOpenStorage(name, out Storage? store);
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
            bool r = Store.TryOpenStream(name, out CfbStream? str);
            if (r)
            {
                stream = new CfbStreamAdapter(str,name, Store);
                return true;
            }
            else if (str != null)
            {
                stream = new CfbStreamAdapter(str,name, Store);
            }
            else
            {
                stream = null;
            }
            return false;

        }

        public override StorageAdapter AddStorage(string name)
        {
            return new StorageAdapter(Store.CreateStorage(name));
        }
        public override CfbStreamAdapter AddStream(string name)
        {
            return new CfbStreamAdapter(Store.CreateStream(name), name, Store);
        }
        public override StorageAdapter GetStorage(string name)
        {
            return new StorageAdapter(Store.OpenStorage(name));
        }
        public override CfStreamAdapterBase GetStream(string name)
        {
            return new CfbStreamAdapter(Store.OpenStream(name), name, Store);
        }

    }
}
