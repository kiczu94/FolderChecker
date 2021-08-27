using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderChecker.ViewModel
{
    public class EditSimpleTextViewModel
    {
        private string _propertyName;

        public string MyPropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }
        private string _newText;

        public string MyNewText
        {
            get { return _newText; }
            set { _newText = value; }
        }
        private string _oldText;

        public string MyOldText
        {
            get { return _oldText; }
            set { _oldText = value; }
        }

        public string MyTextToShow
        {
            get { return _oldText; }
            set { _newText = value; }
        }


        public EditSimpleTextViewModel()
        {

        }
        public void editText()
        {
            MyNewText = MyOldText;
        }

    }
}
