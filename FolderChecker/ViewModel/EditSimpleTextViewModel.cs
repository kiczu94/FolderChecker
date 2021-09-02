namespace FolderChecker.ViewModel
{
    public class EditSimpleTextViewModel
    {
        private string _propertyName;
        private string _newText;
        private string _oldText;
        public string MyPropertyName
        {
            get { return _propertyName; }
            set { _propertyName = value; }
        }
        public string MyNewText
        {
            get { return _newText; }
            set { _newText = value; }
        }
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
        { }
        public void editText()
        {
            MyNewText = MyOldText;
        }
    }
}
