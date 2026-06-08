namespace XstReader.Exporter.CompatabilityWrappers

{
    internal abstract class CfStreamAdapterBase: Stream
    {

        public abstract void SetData(byte[] data);

        public abstract byte[] GetData();
    }
}
