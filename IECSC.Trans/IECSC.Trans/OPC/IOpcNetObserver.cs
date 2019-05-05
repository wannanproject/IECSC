namespace IECSC.Trans.OPC
{
    public interface IOpcNetObserver
    {
        bool UpdateEntity(string TagLongName,object TagValue,bool Quality);
    }
}
