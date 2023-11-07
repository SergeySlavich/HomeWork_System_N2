using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HomeWork_System_N2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        //Сделать оконное приложение, которое позволяет
        //запускать, останавливать, проверять статус других приложений
        //(в виде отдельных процессов).
        //Доступные к запуску приложения располагаются в выпадающем списке.
        //Добавить встроенный профилировщик, выводящий информацию о текущем процессе (максимально полную).

        //Запущенные процессы хранить в специальном списке, при остановке процесса удалять его из списка.

        //Использовать API класса Process и методы работы с ним.

        Process[] processes;

        public MainWindow()
        {
            InitializeComponent();
            LoadProcessListbox();
            LoadComboBox();
        }

        private void LoadProcessListbox()
        {
            ProcessList.Items.Clear();
            processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                ProcessList.Items.Add(process.ProcessName);
            }
        }

        private void LoadComboBox()
        {
            ComboBox.Items.Add(String.Empty);
            ComboBox.Items.Add("Notepad.exe");
            ComboBox.Items.Add("Write.exe");
        }

        private void ProcessList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Process proc = null;
            foreach (Process p in processes) { if (p.ProcessName == ProcessList.SelectedItem.ToString()) proc = p; }
            Start_stop_btn.IsEnabled = true;
            Start_stop_btn.Content = "Stop process";
            ComboBox.Items.Add(proc.ProcessName);
            ComboBox.SelectedItem = proc.ProcessName;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Process proc = null;
            foreach (Process p in processes) { if (p.ProcessName == ComboBox.SelectedItem.ToString()) proc = p; }
            Start_stop_btn.IsEnabled = true;
            Info_btn.IsEnabled = true;
            if (ComboBox.SelectedItem.ToString() != ProcessList.SelectedItem.ToString()) Start_stop_btn.Content = "Start process";
        }

        private void Start_stop_btn_Click(object sender, RoutedEventArgs e)
        {
            Process proc = null;
            if (ComboBox.SelectedItem.ToString() != ProcessList.SelectedItem.ToString())
            {
                proc = ProcessList.SelectedItem as Process;
                proc.Kill();
                proc.Close();
            }
            else
            {
                foreach (Process p in processes) { if (p.ProcessName == ComboBox.SelectedItem.ToString()) proc = p; }
                proc.Start();
            }
        }

        private void Info_btn_click(object sender, RoutedEventArgs e)
        {
            Process proc = null;
            foreach (Process p in processes) { if (p.ProcessName == ComboBox.SelectedItem.ToString()) proc = p; }
            MessageBox.Show($"{proc.ProcessName} - {proc.Id} - {proc.PriorityClass}");
        }
    }
}
