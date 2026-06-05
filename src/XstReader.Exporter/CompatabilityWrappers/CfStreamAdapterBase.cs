namespace XstReader.Exporter.CompatabilityWrappers

{
    internal abstract class CfStreamAdapterBase
    {

        public abstract void SetData(byte[] data);

        public abstract byte[] GetData();
    }
}
