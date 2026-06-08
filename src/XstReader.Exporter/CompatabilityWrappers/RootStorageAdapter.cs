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
        public override bool TryGetStream(string name, out CfbStream stream)
        {
            return _root.TryOpenStream(name, out stream);

        }
        public override StorageAdapter GetStorage(string name)
        {
            return new StorageAdapter(_root.OpenStorage(name));
        }

        public override StorageAdapter AddStorage(string name)
        {
            return new StorageAdapter(_root.CreateStorage(name));
        }
        public override CfbStream AddStream(string name)
        {
            return _root.CreateStream(name);
        }
        public override void AddData(string name, byte[] data)
        {
            using CfbStream stream = _root.CreateStream(name);
            stream.Write(data, 0, data.Length);

        }
        public override CfbStream GetStream(string name)
        {
            return _root.OpenStream(name);
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
