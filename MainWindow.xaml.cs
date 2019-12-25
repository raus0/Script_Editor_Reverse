using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using MessageBox = System.Windows.MessageBox;
using System.Collections.Generic;


namespace Script_Editor_Reverse
{
    public partial class MainWindow : Window
    {
        string selectedROMPath;
        int location;
        List<int> list = new List<int>();

        public MainWindow()
        {
            InitializeComponent();

            //外部プロセスから引数を受け取る
            string[] Commands = Environment.GetCommandLineArgs();

            for (int i = 1; i < Commands.Length; i++)
            {
                if (i == 1)
                {
                    //ファイル取得
                    selectedROMPath = string.Format("{1}", i, Commands[i]);
                    FilePath.Text = selectedROMPath;
                    txtDecompileOffset.IsEnabled = true;
                }
                else if (i == 2)
                {
                    txtDecompileOffset.Text = string.Format("0x{1}", i, Commands[i]);
                    //逆コンパイル実行
                    location = CheckOffset.Listing(location, txtDecompileOffset.Text, list);
                    textEditor.Text = DecompileScript.DecompileCommand(selectedROMPath, location, list);
                }
            }
        }

        private void Decompile(object sender, RoutedEventArgs e)
        {
            //逆コンパイル実行
            location = CheckOffset.Listing(location, txtDecompileOffset.Text, list);
            textEditor.Text = DecompileScript.DecompileCommand(selectedROMPath, location, list);
        }

        private string GetROMCode()
        {
            using (FileStream fs = new FileStream(selectedROMPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] file = new BinaryReader(fs).ReadBytes((int)fs.Length);
                char c;
                string toReturn = "";
                for (int i = 0; i < 4; i++)
                {
                    c = Convert.ToChar(file[0xAC + i]);
                    toReturn += c;
                }
                return toReturn;
            }
        }

        private void Compile(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Compile！");
        }

        void OpenCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                if (Path.GetExtension(dialog.FileName).ToUpper().Equals(".GBA"))
                {
                    selectedROMPath = dialog.FileName;
                    FilePath.Text = selectedROMPath;
                    txtDecompileOffset.IsEnabled = true;
                    string romCode = GetROMCode();
                    textEditor.Text = "ROM Infomation : " + romCode;
                }
            }
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string[] file = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (file == null)
            {
                return;
            }
            if (Path.GetExtension(file[0]).ToUpper().Equals(".GBA"))
            {
                selectedROMPath = file[0];
                FilePath.Text = selectedROMPath;
                txtDecompileOffset.IsEnabled = true;
                string romCode = GetROMCode();
                textEditor.Text = "ROM Infomation : " + romCode;
            }
        }

        private void Window_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        void OpenCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void menuitemExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
