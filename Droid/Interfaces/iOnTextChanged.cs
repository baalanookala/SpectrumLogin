using SampleLogin.Droid.Helpers;

namespace SampleLogin.Droid.Interfaces
{
    public interface IOnTextChanged
    {
        void SetText(string inputValue, InputField inputFieldType);
    }
}
