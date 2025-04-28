using System;

namespace MobileShared
{
    public class ComboBoxItem
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public ComboBoxItem()
        { }

        /// <summary>
        /// Constructor which accepts parameters.
        /// </summary>
        public ComboBoxItem(string Display, int Value)
        {
            _value = Value;
            _display = Display;
        }

        private int _value;
        private string _display;

        public string Display
        {
            get { return _display; }
            set { _display = value; }
        }

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public override string ToString()
        {
            return _display;
        }
    }
}
