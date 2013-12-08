using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoSearch
{
    class ComboBoxItem
    {
        private string _text = null;
        private string _value = null;
        public string Text { get { return this._text; } set { this._text = value; } }
        public string Value { get { return this._value; } set { this._value = value; } }
        public ComboBoxItem(string t, string v)
        {
            this._text = t;
            this._value = v;
        }
        public override string ToString()
        {
            return this._text;
        }
    }
}
