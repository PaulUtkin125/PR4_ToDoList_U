using PR4_ToDoList_U.Data;
using PR4_ToDoList_U.Model;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR4_ToDoList_U
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            VuvodZadach();
        }

        private void addNewTask_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textNewTask.Text)) return;

            dbAddNewTask(textNewTask.Text);
            VuvodZadach();
        }

        private void enterDown_Click(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (string.IsNullOrEmpty(textNewTask.Text)) return;

            dbAddNewTask(textNewTask.Text);
            VuvodZadach();

        }
        public void dbAddNewTask(string text)
        {
            using (var context = new dbContact()) 
            {
                NewTask newTask = new NewTask()
                {
                    Text = text,
                    Statys = false
                };
                context.newTasks.Add(newTask);
                context.SaveChanges();
            }
        }

        public void VuvodZadach() 
        {
            TaskList.Children.Clear();
            WrapPanel panel = TaskList;

            using (var context = new dbContact())
            {
                foreach (var task in context.newTasks)
                {
                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Margin = new Thickness(0, 10, 10, 0);
                    stackPanel.Orientation = Orientation.Horizontal;

                    CheckBox checkBox = new CheckBox();
                    checkBox.VerticalAlignment = VerticalAlignment.Center;
                    checkBox.IsChecked = false;
                    checkBox.Click += CheckPointIzm_Click;
                    stackPanel.Children.Add(checkBox);

                    TextBox textBoxId = new TextBox();
                    textBoxId.Name = "id";
                    textBoxId.Text = task.Id.ToString();
                    textBoxId.IsReadOnly = true;
                    textBoxId.Visibility = Visibility.Collapsed;
                    stackPanel.Children.Add(textBoxId);

                    TextBox textBox = new TextBox();
                    textBox.Text = task.Text;
                    textBox.FontSize = 18;
                    textBox.Width = 250;
                    textBox.VerticalAlignment = VerticalAlignment.Center;
                    textBox.TextWrapping = TextWrapping.Wrap;
                    textBox.IsReadOnly = true;
                    textBox.BorderBrush = Brushes.Transparent;
                    //textBox.MouseDoubleClick += ;
                    textBox.Margin = new Thickness(20, 0, 0, 20);
                    stackPanel.Children.Add(textBox);

                    BitmapImage bitmapImage = new BitmapImage(new Uri(@"C:\Users\П\source\repos\PR4_ToDoList_U\Image\крестик.png"));
                    Image image = new Image();
                    image.Source = bitmapImage;
                    image.Width = 30;
                    image.Height = 30;

                    Button button = new Button();
                    button.VerticalAlignment = VerticalAlignment.Center;
                    button.Width = 60;
                    button.Content = image;
                    button.Click += DeleteTask;
                    button.VerticalAlignment = VerticalAlignment.Center;

                    stackPanel.Children.Add(button);
                    panel.Children.Add(stackPanel);

                }
            }

        }
        public void DeleteTask(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            StackPanel stackPanelParent = button.Parent as StackPanel;
            if (stackPanelParent is null) return;

            int idTask = 0;
            foreach (var item in stackPanelParent.Children)
            {
                if (item is TextBox textBox)
                {
                    if (textBox.Name == "id")
                    {
                        idTask = int.Parse(textBox.Text);
                        using (var context = new dbContact())
                        {
                            var task = context.newTasks.Find(idTask);
                            if (task == null) { VuvodZadach(); break; }
                            context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                            context.SaveChanges();

                            VuvodZadach();
                            return;
                        }
                    }
                }
            }
        }
        private void CheckPointIzm_Click(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox == null) return;

            StackPanel stackPanelParent = checkBox.Parent as StackPanel;
            if (stackPanelParent == null) return;

            int idTask = 0;
            foreach (var item in stackPanelParent.Children)
            {
                if (item is TextBox textBox)
                {
                    if (textBox.Name == "id")
                    { 
                        idTask = int.Parse(textBox.Text);
                        using (var context = new dbContact())
                        {
                            var task = context.newTasks.Find(idTask);
                            if (task == null) break;
                            context.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        textBox.Foreground = Brushes.Gray;
                        textBox.TextDecorations = TextDecorations.Strikethrough;
                        using (var context = new dbContact()) 
                        {
                            ReadyTask newReadyTask = new ReadyTask()
                            {
                                firstNomer = idTask,
                                Text = textBox.Text,
                                Statys = true
                            };
                            context.ReadyTasks.Add(newReadyTask);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}