using Android.Text;
using Java.Lang;
using SampleLogin.Droid.Interfaces;

namespace SampleLogin.Droid.Helpers
{
    public class TextWatcher : Java.Lang.Object, ITextWatcher
    {
        private readonly IOnTextChanged onTextChanged;
        private readonly InputField inputField;

        public TextWatcher()
        {
        }

        public TextWatcher(IOnTextChanged onTextChanged, InputField inputField)
        {
            this.onTextChanged = onTextChanged;
            this.inputField = inputField;
        }

        public void AfterTextChanged(IEditable s)
        {
            
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
          
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            this.onTextChanged?.SetText(s.ToString(), inputField);
        }

    }

}
