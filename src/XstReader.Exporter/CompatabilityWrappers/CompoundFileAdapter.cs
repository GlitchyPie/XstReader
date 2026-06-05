namespace XstReader.Exporter.CompatabilityWrappers

{
    internal class CompoundFileAdapter : IDisposable
    {
        public RootStorageAdapter RootStorage { get; }

        internal Stream _baseStream;

        public CompoundFileAdapter(string fileName)
        {
            _baseStream = new FileStream(fileName,FileMode.Open,FileAccess.Read,FileShare.Read);
            RootStorage = new RootStorageAdapter(OpenMcdf.RootStorage.Open(_baseStream));
        }
        public CompoundFileAdapter()
        {
            _baseStream = new MemoryStream();
            RootStorage = new RootStorageAdapter(OpenMcdf.RootStorage.Create(_baseStream));
        }

        public void Close()
        {
            RootStorage.Dispose();
            _baseStream.Close();
            _baseStream.Dispose();
        }

        public void Save(string filename)
        {
            using FileStream fs = new(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            Save(fs);
            fs.Flush();
        }
        public void Save(Stream stream)
        {
            RootStorage.Flush(true);
            _baseStream.Seek(0, SeekOrigin.Begin);
            _baseStream.CopyTo(stream);
        }

        public void Dispose()
        {
            Close();
        }
    }
}
