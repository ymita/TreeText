using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TreeText.Model;
using TreeText.Commands;
using TreeText.Util;
using TreeText.Infrastructure;

namespace TreeText.ViewModel
{
    public class MainViewModel : NotificationObject
    {
        public ObservableCollection<Node> Nodes { get; set; }

        private Node _selectedTemplate;
        public Node SelectedTemplate
        {
            get
            {
                return _selectedTemplate;
            }
            set
            {
                _selectedTemplate = value;
                OnPropertyChanged();
                ((DelegateCommand)SelectionChangeCommand).RaiseCanExecuteChanged();
            }
        }

        private ICommand _selectionChangeCommand;
        public ICommand SelectionChangeCommand
        {
            get
            {
                if (_selectionChangeCommand == null)
                    _selectionChangeCommand = new DelegateCommand
                    {
                        ExecuteHandler = (parameter) => {
                            Debug.WriteLine("SelectionChangeCommand executed.");
                            Node selectedItem = parameter as Node;

                            if(selectedItem != null)
                            {
                                this.SelectedTemplate = selectedItem;
                            }
                            else
                            {
                                Debug.WriteLine("Category is selected so that RichTextEditor is blank.");
                                this.SelectedTemplate = null;
                            }
                        },
                        CanExecuteHandler = (parameter) => {
                            // When template node is selected, execute SelectionChangeCommand
                            Node node = parameter as Node;

                            // Regard children count != null as leaf
                            if(node != null && node.Children == null)
                            {
                                return true;
                            }
                            else
                            {
                                // Clear RichTextEditor content when template is not selected.
                                this.SelectedTemplate = null;
                                return false;
                            }
                        },
                    };
                return _selectionChangeCommand;
            }
        }

        public MainViewModel()
        {
            #region loading data
            this.Nodes = DataLoadHelper.getNodes("Data/Templates.xml");
            #endregion
        }
    }
}