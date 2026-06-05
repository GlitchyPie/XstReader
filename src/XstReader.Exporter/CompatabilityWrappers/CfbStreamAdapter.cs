using OpenMcdf;

namespace XstReader.Exporter.CompatabilityWrappers

{
    internal class CfbStreamAdapter : CfStreamAdapterBase
    {
        internal CfbStream _stream;
        internal Storage _owner;
        internal string _name;

        public CfbStreamAdapter(CfbStream stream, string name, Storage owner)
        {
            _stream = stream;
            _owner = owner;
            _name = name;
        }

        public override void SetData(byte[] data)
        {
            _stream.Dispose();

            _owner.Delete(_name);

            _stream = _owner.CreateStream(_name);
            _stream.Write(data, 0, data.Length);
            _stream.Flush();
        }
        public override byte[] GetData()
        {
            byte[] data = new byte[_stream.Length];
            _stream.Position = 0;
            int offset = 0;
            while(offset < data.Length)
            {
                int read = _stream.Read(data,offset, data.Length - offset);
                if (read < 0)
                    throw new IOException("Unexpected end of stream");

                offset += read;
            }
            return data;
        }
    }
}
