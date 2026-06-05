using OpenMcdf;

namespace XstReader.Exporter.CompatabilityWrappers

{
    internal class CfbStreamAdapter : CfStreamAdapterBase
    {
        internal CfbStream _Stream;
        internal Storage _owner;
        internal string _name;

        public CfbStreamAdapter(CfbStream stream, string name, Storage owner)
        {
            _Stream = stream;
            _owner = owner;
            _name = name;
        }

        public override void SetData(byte[] data)
        {
            _Stream.Dispose();

            _owner.Delete(_name);

            _Stream = _owner.CreateStream(_name);
            _Stream.Write(data, 0, data.Length);
            _Stream.Flush();
        }
        public override byte[] GetData()
        {
            byte[] data = new byte[_Stream.Length];
            _Stream.Position = 0;
            int offset = 0;
            while(offset < data.Length)
            {
                int read = _Stream.Read(data,offset, data.Length - offset);
                if (read < 0)
                    throw new IOException("Unexpected end of stream");

                offset += read;
            }
            return data;
        }
    }
}
