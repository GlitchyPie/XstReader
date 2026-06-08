using OpenMcdf;

namespace XstReader.Exporter.CompatabilityWrappers

{
    internal class RootStorageAdapter : StorageAdapterBase,IDisposable
    {
        internal RootStorage _root;
        public RootStorageAdapter(RootStorage root)
        {
            _root = root;
        }

        public override bool TryGetStorage(string name, out StorageAdapter? storage)
        {
            bool r = _root.TryOpenStorage(name, out Storage? store);
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
            bool r = _root.TryOpenStream(name, out CfbStream? str);
            if (r)
            {
                stream = new CfbStreamAdapter(str, name, _root);
                return true;
            }
            else if (str != null)
            {
                stream = new CfbStreamAdapter(str, name, _root);
            }
            else
            {
                stream = null;
            }
            return false;

        }
        public override StorageAdapter GetStorage(string name)
        {
            return new StorageAdapter(_root.OpenStorage(name));
        }

        public override StorageAdapter AddStorage(string name)
        {
            return new StorageAdapter(_root.CreateStorage(name));
        }
        public override CfbStreamAdapter AddStream(string name)
        {
            return new CfbStreamAdapter(_root.CreateStream(name), name, _root);
        }
        public override void AddData(string name, byte[] data)
        {
            using CfbStream stream = _root.CreateStream(name);
            stream.Write(data, 0, data.Length);

        }
        public override CfStreamAdapterBase GetStream(string name)
        {
            return new CfbStreamAdapter(_root.OpenStream(name), name, _root);
        }

        public void Flush(bool consolidate = false)
        {
            _root.Flush(consolidate);
        }

        public void Dispose()
        {
            _root.Dispose();
        }
    }
}
