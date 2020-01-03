using System;
using System.IO;
using System.Linq;
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

        public MainWindow()
        {
            InitializeComponent();

            //外部プロセスから引数を受け取る
            string[] Commands = Environment.GetCommandLineArgs();

            for (int i = 1; i < Commands.Length; i++)
            {
                string msgLine = "";
                string movementLine = "";

                if (i == 1)
                {
                    //ファイル取得
                    selectedROMPath = string.Format("{1}", i, Commands[i]);
                    FilePath.Text = selectedROMPath;
                    txtDecompileOffset.IsEnabled = true;
                    Decompilebtn.IsEnabled = true;
                    BINbtn.IsEnabled = true;
                    macrobtn.IsEnabled = true;
                }
                else if (i == 2)
                {
                    txtDecompileOffset.Text = string.Format("0x{1}", i, Commands[i]);

                    string romCode = GetROMCode();

                    //逆コンパイル実行
                    List<int> list = new List<int>();
                    List<int> msg = new List<int>();
                    List<int> movement = new List<int>();

                    location = CheckOffset.Listing(location, txtDecompileOffset.Text);

                    textEditor.Text = string.Join(Environment.NewLine, DecompileScript.DecompileCommand(selectedROMPath, location, list, msg, movement));

                    msg = msg.Distinct().ToList();

                    foreach (int locationChar in msg)
                    {
                        msgLine += "\n" + string.Join(Environment.NewLine, DecompileChar.DecompileMSG(selectedROMPath, locationChar));
                    }

                    movement = movement.Distinct().ToList();

                    foreach (int locationMovement in movement)
                    {
                        movementLine += "\n" + string.Join(Environment.NewLine, DecompileMovement.DecompileCommand(selectedROMPath, locationMovement, romCode));
                    }

                    textEditor.Text += msgLine + movementLine;
                }
            }
        }

        private void Decompile(object sender, RoutedEventArgs e)
        {
            string msgLine = "";
            string movementLine = "";

            string romCode = GetROMCode();

            //逆コンパイル実行
            List<int> list = new List<int>();
            List<int> msg = new List<int>();
            List<int> movement = new List<int>();

            location = CheckOffset.Listing(location, txtDecompileOffset.Text);

            textEditor.Text = string.Join(Environment.NewLine, DecompileScript.DecompileCommand(selectedROMPath, location, list, msg, movement));

            msg = msg.Distinct().ToList();

            foreach (int locationChar in msg)
            {
                msgLine += "\n" + string.Join(Environment.NewLine, DecompileChar.DecompileMSG(selectedROMPath, locationChar));
            }

            movement = movement.Distinct().ToList();

            foreach (int locationMovement in movement)
            {
                movementLine += "\n" + string.Join(Environment.NewLine, DecompileMovement.DecompileCommand(selectedROMPath, locationMovement, romCode));
            }

            textEditor.Text += msgLine + movementLine;
        }

        private void BIN(object sender, RoutedEventArgs e)
        {
            string msgLine = "";
            string movementLine = "";

            string romCode = GetROMCode();

            //逆コンパイル実行
            List<int> list = new List<int>();
            List<int> msg = new List<int>();
            List<int> movement = new List<int>();

            location = CheckOffset.Listing(location, txtDecompileOffset.Text);

            textEditor.Text = string.Join(Environment.NewLine, DecompileBIN.DecompileCommand(selectedROMPath, location, list, msg, movement));

            msg = msg.Distinct().ToList();

            foreach (int locationChar in msg)
            {
                msgLine += "\n" + string.Join(Environment.NewLine, DecompileChar.DecompileBinary(selectedROMPath, locationChar));
            }

            movement = movement.Distinct().ToList();

            foreach (int locationMovement in movement)
            {
                movementLine += "\n" + string.Join(Environment.NewLine, DecompileMovement.DecompileBinary(selectedROMPath, locationMovement, romCode));
            }

            textEditor.Text += msgLine + movementLine;
        }

        private void macro(object sender, RoutedEventArgs e)
        {
            bool equ = false;

            string msgLine = "";
            string movementLine = "";

            string romCode = GetROMCode();

            //逆コンパイル実行
            List<int> list = new List<int>();
            List<int> msg = new List<int>();
            List<int> movement = new List<int>();

            location = CheckOffset.Listing(location, txtDecompileOffset.Text);

            textEditor.Text = string.Join(Environment.NewLine, DecompileMacro.DecompileCommand(selectedROMPath, location, list, msg, movement, equ));

            msg = msg.Distinct().ToList();

            foreach (int locationChar in msg)
            {
                msgLine += "\n" + string.Join(Environment.NewLine, DecompileChar.DecompileRaw(selectedROMPath, locationChar));
            }

            movement = movement.Distinct().ToList();

            foreach (int locationMovement in movement)
            {
                movementLine += "\n" + string.Join(Environment.NewLine, DecompileMovement.DecompileRaw(selectedROMPath, locationMovement, romCode));
            }

            textEditor.Text += msgLine + movementLine;
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

            FileStream filestream = new FileStream(selectedROMPath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            filestream.Lock(0, filestream.Length);
            filestream.Unlock(0, filestream.Length);

            //ここでファイルの書き込み処理を行う
            //書き込みにはFilestream.Write関数を使用すること

            filestream.Close();
        }

        void OpenCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\users\desktop";
            dialog.FileName = "event.asm";
            dialog.Filter = "すべてのファイル|*.*|ASM ファイル|*.asm|GBA ファイル|*.gba";
            dialog.FilterIndex = 3;
            if (dialog.ShowDialog() == true)
            {
                if (Path.GetExtension(dialog.FileName).ToUpper().Equals(".GBA"))
                {
                    selectedROMPath = dialog.FileName;
                    FilePath.Text = selectedROMPath;
                    txtDecompileOffset.IsEnabled = true;
                    Decompilebtn.IsEnabled = true;
                    BINbtn.IsEnabled = true;
                    macrobtn.IsEnabled = true;
                    string romCode = GetROMCode();
                    textEditor.Text = "ROM Infomation : " + romCode;
                }
                if (Path.GetExtension(dialog.FileName).ToUpper().Equals(".ASM"))
                {
                    FileStream fs = new FileStream(dialog.FileName, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string text = sr.ReadToEnd();
                    textEditor.Text = text;
                    sr.Close();
                    fs.Close();
                }
            }
        }

        private void menuiteSaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = @"C:\users\desktop";
            dialog.FileName = "event.asm";
            dialog.Filter = "すべてのファイル|*.*|ASM ファイル|*.asm";
            dialog.FilterIndex = 2;
            if (dialog.ShowDialog() == true)
            {
                FileStream fs = new FileStream(dialog.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(textEditor.Text);
                sw.Close();
                fs.Close();
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
                Decompilebtn.IsEnabled = true;
                BINbtn.IsEnabled = true;
                macrobtn.IsEnabled = true;
                string romCode = GetROMCode();
                textEditor.Text = "ROM Infomation : " + romCode;
            }
            if (Path.GetExtension(file[0]).ToUpper().Equals(".ASM"))
            {
                FileStream fs = new FileStream((file[0]), FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string text = sr.ReadToEnd();
                textEditor.Text = text;
                sr.Close();
                fs.Close();
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
