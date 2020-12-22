namespace SampleLogin.Helpers
{
    public interface IDataBaseHelper
    {
        object fetchRecord(string key);
        void insertRecord<Key, Value>(ref Key key, ref Value value);
    }

}

