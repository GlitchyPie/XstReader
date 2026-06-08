using OpenMcdf;
using System.ComponentModel.DataAnnotations;

namespace XstReader.Exporter.CompatabilityWrappers

{
    internal class CfbStreamAdapter : CfStreamAdapterBase
    {
        internal CfbStream _stream;
        internal Storage _owner;
        internal string _name;

        public override bool CanRead => _stream.CanRead;

        public override bool CanSeek => _stream.CanSeek;

        public override bool CanWrite => _stream.CanWrite;

        public override long Length => _stream.Length;

        public override long Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

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

        public override void Flush()
        {
            _stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _stream.Write(buffer, offset, count);
        }
    }
}
