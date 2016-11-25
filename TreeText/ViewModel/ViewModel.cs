using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Xml;
using TreeText.Model;
using TreeText.Util;

namespace TreeText.ViewModel
{
    public class MainViewModel : NotificationObject
    {
        public ObservableCollection<Template> Templates { get; set; }

        private Template _selectedTemplate;
        public Template SelectedTemplate
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
                            Debug.WriteLine("コマンドが実行されました。");
                            Template selectedItem = parameter as Template;
                            this.SelectedTemplate = selectedItem;
                        },
                        CanExecuteHandler = (parameter) => {
                            return true;    // 必ず実行許可
                        },
                    };
                return _selectionChangeCommand;
            }
        }

        public MainViewModel()
        {
            this.Templates = new ObservableCollection<Template>();
            
            #region loading data
            XmlDocument doc = new XmlDocument();
            doc.Load("Data/Templates.xml");
            XmlNodeList templates = doc.SelectNodes("Templates/Template");

            foreach (XmlNode _template in templates)
            {
                Template template = new Template();

                template.ID = int.Parse(_template.SelectNodes("ID").Item(0).InnerText);
                template.Title = _template.SelectNodes("Title").Item(0).InnerText;
                template.Content = _template.SelectNodes("Content").Item(0).InnerText;

                this.Templates.Add(template);
            }
            #endregion
        }
    }
}