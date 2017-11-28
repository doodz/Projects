namespace IvidatalinkShortcut
{
    public class CheckedListItem<T> : NotifyPropertyChangedBase
    {
        private bool _isChecked;

        public bool isChecked
        {
            get => _isChecked;
            set => SetProperty<bool>(ref _isChecked, "IsChecked", value);
        }

        public T Item { get; set; }
    }
}