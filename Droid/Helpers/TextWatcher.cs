using System;
using Android.Text;
using Java.Lang;
using SampleLogin.Droid.Interfaces;

namespace SampleLogin.Droid.Helpers
{
    public class TextWatcher : Java.Lang.Object, ITextWatcher
    {
        private iOnTextChanged onTextChanged1;
        private InputField inputX;
        private string phnNumber;


        public TextWatcher()
        {
        }

        public TextWatcher(iOnTextChanged onTextChanged, InputField inputX)
        {
            this.onTextChanged1 = onTextChanged;
            this.inputX = inputX;
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
           
                this.onTextChanged1?.setText(s.ToString(), inputX);
            
        }

    }

}
