namespace SampleLogin.Helpers
{
    public interface IDataBaseHelper
    {
        object FetchRecord(string key);
        void InsertRecord<Key, Value>(ref Key key, ref Value value);
    }

}

