using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class FilterItem
    {
        private string _lowQuantity;
        private string _highQuantity;

        public FilterItem()
        {

        }

        public FilterItem(string lowQuantity, string highQuantity)
        {
            _lowQuantity = lowQuantity;
            _highQuantity = highQuantity;
        }

        public string LowQuantity
        {
            get => _lowQuantity;
            set => _lowQuantity = value;
        }

        public string HighQuantity
        {
            get => _highQuantity;
            set => _highQuantity = value;
        }

        public override string ToString()
        {
            return $"{nameof(_lowQuantity)}: {_lowQuantity}, {nameof(_highQuantity)}: {_highQuantity}";
        }
    }
}
