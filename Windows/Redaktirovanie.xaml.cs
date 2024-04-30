using PR4_ToDoList_U.Data;
using PR4_ToDoList_U.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PR4_ToDoList_U.Windows
{
    /// <summary>
    /// Логика взаимодействия для Redaktirovanie.xaml
    /// </summary>
    public partial class Redaktirovanie : Window
    {
        public NewTask? newTask = null;
        public Redaktirovanie(NewTask task)
        {
            InitializeComponent();
            newTask = task;
            TaskBox.Text = newTask.Text;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindow mainWindow = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.VuvodZadach();
        }
        private void save_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new dbContact())
            {
                newTask.Text = TaskBox.Text;
                context.Entry(newTask).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
            this.Close();
        }
    }
}
