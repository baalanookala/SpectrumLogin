using System;
using Android.Text;
using Java.Lang;

namespace SampleLogin.Droid.Helpers
{
    public class TextWatcher : Java.Lang.Object, ITextWatcher
    {
        private string vmStringValue;
        private Action onTextValueChanged;

        public TextWatcher(string vmStringValue, Action onTextValueChanged)
        {
            this.vmStringValue = vmStringValue;
            this.onTextValueChanged = onTextValueChanged;
        }

        public void AfterTextChanged(IEditable s)
        {
            //
        }

        public void BeforeTextChanged(ICharSequence s, int start, int count, int after)
        {
            //
        }

        public void OnTextChanged(ICharSequence s, int start, int before, int count)
        {
            this.vmStringValue = s.ToString();
            this.onTextValueChanged?.Invoke();
        }
      
    }
}
