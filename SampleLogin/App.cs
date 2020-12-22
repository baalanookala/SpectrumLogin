using SampleLogin.Models;

namespace SampleLogin
{
    public class App
    {
        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IDataStore<AccountInfo>, MockDataStore>();
        }
    }
}
