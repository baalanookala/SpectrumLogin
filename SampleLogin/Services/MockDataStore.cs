using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleLogin.Models;

namespace SampleLogin
{
    public static class MockDataStore
    {
        static List<AccountInfo> items = new List<AccountInfo>();

        public static async Task<bool> AddItemAsync(AccountInfo item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public static async Task<bool> UpdateItemAsync(AccountInfo item)
        {
            var _item = items.Where((AccountInfo arg) => arg.UserName == item.UserName).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public static async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((AccountInfo arg) => arg.UserName == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public static async Task<AccountInfo> GetItemAsync(string id)
        {
            //return await Task.FromResult(items.FirstOrDefault(s => s.UserName == id));
            return await Task.FromResult(items.Where(s => s.UserName == id).FirstOrDefault());
        }

        public static async Task<IEnumerable<AccountInfo>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

    }
}
