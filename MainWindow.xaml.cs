using PR4_ToDoList_U.Data;
using PR4_ToDoList_U.Model;
using System.Text;
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
                    //checkBox.Click +=;
                    stackPanel.Children.Add(checkBox);

                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = task.Text;
                    textBlock.FontSize = 18;
                    textBlock.Width = 250;
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.TextWrapping = TextWrapping.Wrap;
                    textBlock.Margin = new Thickness(20, 0, 0, 20);
                    stackPanel.Children.Add(textBlock);

                    BitmapImage bitmapImage = new BitmapImage(new Uri(@"C:\Users\П\source\repos\PR4_ToDoList_U\Image\крестик.png"));
                    Image image = new Image();
                    image.Source = bitmapImage;
                    image.Width = 30;
                    image.Height = 30;

                    Button button = new Button();
                    button.VerticalAlignment = VerticalAlignment.Center;
                    button.Width = 60;
                    button.Content = image;
                    button.VerticalAlignment = VerticalAlignment.Center;

                    stackPanel.Children.Add(button);
                    panel.Children.Add(stackPanel);
                }
            }

        }
    }
}