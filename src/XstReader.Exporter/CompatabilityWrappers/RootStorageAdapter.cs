using OpenMcdf;

namespace XstReader.Exporter.CompatabilityWrappers

{
    internal class RootStorageAdapter : StorageAdapterBase
    {
        public RootStorage Root { get; }
        public RootStorageAdapter(RootStorage root)
        {
            Root = root;
        }

        public override bool TryGetStorage(string name, out StorageAdapter? storage)
        {
            bool r = Root.TryOpenStorage(name, out Storage? store);
            if (r)
            {
                storage = new StorageAdapter(store);
                return true;
            }else if(store != null)
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
            bool r = Root.TryOpenStream(name, out CfbStream? str);
            if (r)
            {
                stream = new CfbStreamAdapter(str, name, Root);
                return true;
            }
            else if (str != null)
            {
                stream = new CfbStreamAdapter(str, name, Root);
            }
            else
            {
                stream = null;
            }
            return false;

        }
        public override StorageAdapter GetStorage(string name)
        {
            return new StorageAdapter(Root.OpenStorage(name));
        }

        public override StorageAdapter AddStorage(string name)
        {
            return new StorageAdapter(Root.CreateStorage(name));
        }
        public override CfbStreamAdapter AddStream(string name)
        {
            return new CfbStreamAdapter(Root.CreateStream(name), name, Root);
        }
        public override CfStreamAdapterBase GetStream(string name)
        {
            return new CfbStreamAdapter(Root.OpenStream(name), name, Root);
        }
    }
}
