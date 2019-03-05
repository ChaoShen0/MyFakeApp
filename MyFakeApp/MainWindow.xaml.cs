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
using System.IO;
using System.Reflection;

namespace MyFakeApp
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      private bool _firstTime = true;
      private IEnumerable<int> _values = new List<int>() {
         2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 4096
      };
      public MainWindow()
      {
         InitializeComponent();
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         var dlg = new Microsoft.Win32.OpenFileDialog();
         var result = dlg.ShowDialog();
         if (result == true)
         {
            textbox1.Text = dlg.FileName;
            var outputPath = System.IO.Path.Combine(
               System.IO.Path.GetDirectoryName(dlg.FileName),
               System.IO.Path.GetFileNameWithoutExtension(dlg.FileName));
            textbox2.Text = outputPath;
            textbox4.Text = outputPath;
            textbox5.Text = System.IO.Path.Combine(outputPath, "faces");
            textbox12.Text = dlg.FileName;
            textbox13.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName),
               System.IO.Path.GetFileNameWithoutExtension(dlg.FileName)
               + "-myfakeapp" + System.IO.Path.GetExtension(dlg.FileName));
         }
      }

      private void Button_Click_1(object sender, RoutedEventArgs e)
      {
         var dlg = new System.Windows.Forms.FolderBrowserDialog();
         var result = dlg.ShowDialog();
         if (result == System.Windows.Forms.DialogResult.OK)
         {
            textbox2.Text = dlg.SelectedPath;
            textbox4.Text = dlg.SelectedPath;
            textbox5.Text = System.IO.Path.Combine(dlg.SelectedPath, "faces");
         }
      }

      private void Button_Click_2(object sender, RoutedEventArgs e)
      {
         if (!Directory.Exists(textbox2.Text))
            Directory.CreateDirectory(textbox2.Text);
         var startInfo = new ProcessStartInfo();
         startInfo.FileName = GetAbsolutePath(@"lib\ffmpeg.exe");
         startInfo.UseShellExecute = false;
         startInfo.Arguments = string.Format("-y -i \"{0}\" -vf fps={1}/{2} \"{3}\"",
            textbox1.Text, textbox3.Text,
            ((ComboBoxItem)combobox1.SelectedValue).Tag, System.IO.Path.Combine(textbox2.Text, "img%07d.png"));
         using (var proc = new Process())
         {
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
            if (proc.ExitCode != 0)
            {
               MessageBox.Show("Could not create frames!", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
         }
      }

      private static string GetAbsolutePath(string v)
      {
         string codeBase = Assembly.GetExecutingAssembly().CodeBase;
         UriBuilder uri = new UriBuilder(codeBase);
         string path = Uri.UnescapeDataString(uri.Path);
         return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(path), v);
      }

      private void Button_Click_3(object sender, RoutedEventArgs e)
      {
         var dlg = new System.Windows.Forms.FolderBrowserDialog();
         var result = dlg.ShowDialog();
         if (result == System.Windows.Forms.DialogResult.OK)
         {
            textbox4.Text = dlg.SelectedPath;
            textbox5.Text = System.IO.Path.Combine(dlg.SelectedPath, "faces");
         }
      }

      private void Button_Click_4(object sender, RoutedEventArgs e)
      {
         var dlg = new System.Windows.Forms.FolderBrowserDialog();
         var result = dlg.ShowDialog();
         if (result == System.Windows.Forms.DialogResult.OK)
         {
            textbox5.Text = dlg.SelectedPath;
         }
      }

      private void Button_Click_5(object sender, RoutedEventArgs e)
      {
         var startInfo = new ProcessStartInfo();
         startInfo.FileName = GetAbsolutePath("lib\\python-CPU\\python.exe");
         startInfo.UseShellExecute = false;
         startInfo.Arguments = BuildExtractParameters();
         using (var proc = new Process())
         {
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
            if (proc.ExitCode != 0)
            {
               MessageBox.Show("Could not create faces!", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            if (_firstTime)
            {
               textbox8.Text = textbox5.Text;
               textbox10.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(textbox5.Text), "model");
               textbox14.Text = textbox10.Text;
               _firstTime = false;
            }
            else
            {
               textbox9.Text = textbox5.Text;
               textbox10.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(textbox5.Text), "model");
               textbox14.Text = textbox10.Text;
            }
         }
      }

      private string BuildExtractParameters()
      {
         var result = string.Format("lib\\faceswap.py extract -i \"{0}\" -o \"{1}\" -v -D {2}",
            textbox4.Text, textbox5.Text, ((ComboBoxItem)combobox2.SelectedValue).Tag);
         if (!string.IsNullOrEmpty(textbox6.Text))
            result += string.Format(" -f \"{0}\"", textbox6.Text);
         if (!string.IsNullOrEmpty(textbox7.Text))
            result += string.Format(" -j {0}", textbox7.Text);
         return result;
      }

      private void Button_Click_6(object sender, RoutedEventArgs e)
      {
         var dlg = new Microsoft.Win32.OpenFileDialog();
         var result = dlg.ShowDialog();
         if (result == true)
         {
            textbox6.Text = dlg.FileName;
            textbox15.Text = dlg.FileName;
         }
      }

      private void Button_Click_7(object sender, RoutedEventArgs e)
      {
         var dlg = new System.Windows.Forms.FolderBrowserDialog();
         var result = dlg.ShowDialog();
         if (result == System.Windows.Forms.DialogResult.OK)
         {
            textbox8.Text = dlg.SelectedPath;
            textbox10.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.SelectedPath), "model");
            textbox14.Text = textbox10.Text;
         }
      }

      private void Button_Click_8(object sender, RoutedEventArgs e)
      {
         var dlg = new System.Windows.Forms.FolderBrowserDialog();
         var result = dlg.ShowDialog();
         if (result == System.Windows.Forms.DialogResult.OK)
         {
            textbox9.Text = dlg.SelectedPath;
            textbox10.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.SelectedPath), "model");
            textbox14.Text = textbox10.Text;
         }
      }

      private void Button_Click_9(object sender, RoutedEventArgs e)
      {
         var dlg = new System.Windows.Forms.FolderBrowserDialog();
         var result = dlg.ShowDialog();
         if (result == System.Windows.Forms.DialogResult.OK)
         {
            textbox10.Text = dlg.SelectedPath;
            textbox14.Text = textbox10.Text;
         }
      }

      private void Button_Click_10(object sender, RoutedEventArgs e)
      {
         var batchSize = int.Parse((string)((ComboBoxItem)combobox4.SelectedValue).Tag);
         var minimunFiles = Math.Min(
            Directory.GetFiles(textbox8.Text, "*", SearchOption.TopDirectoryOnly).Length,
            Directory.GetFiles(textbox9.Text, "*", SearchOption.TopDirectoryOnly).Length);
         if (minimunFiles < batchSize)
         {
            var newBatchSize = _values.Last(v => v <= minimunFiles);
            MessageBox.Show("Not enough files in data directory. Setting the batch size from " + batchSize + " to " + newBatchSize + " .",
               "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            batchSize = newBatchSize;

         }
         var processor = (string)((ComboBoxItem)combobox5.SelectedValue).Tag;
         var startInfo = new ProcessStartInfo();
         startInfo.FileName = GetAbsolutePath("lib\\python-" + processor + "\\python.exe");
         startInfo.UseShellExecute = false;
         startInfo.Arguments = BuildTrainParameters(batchSize);
         using (var proc = new Process())
         {
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
            if (proc.ExitCode != 0)
            {
               MessageBox.Show("Could not train your model!", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
         }
      }

      private string BuildTrainParameters(int batchSize)
      {
         return string.Format("lib\\faceswap.py train -A \"{0}\" -B \"{1}\" -m \"{2}\" -p -v -s {3} -w -t {4} -bs {5}",
            textbox8.Text, textbox9.Text, textbox10.Text, textbox11.Text,
            ((ComboBoxItem)combobox3.SelectedValue).Tag, batchSize);
      }

      private void combobox7_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         try
         {
            var val = (string)((ComboBoxItem)combobox7.SelectedValue).Tag;
            switch (val)
            {
               case "Masked":
                  textbox16.IsEnabled = true;
                  checkbox2.IsEnabled = true;
                  combobox9.IsEnabled = true;
                  textbox17.IsEnabled = true;
                  checkbox3.IsEnabled = false;
                  checkbox4.IsEnabled = false;
                  break;
               case "Adjust":
                  textbox16.IsEnabled = false;
                  checkbox2.IsEnabled = false;
                  combobox9.IsEnabled = false;
                  textbox17.IsEnabled = false;
                  checkbox3.IsEnabled = true;
                  checkbox4.IsEnabled = true;
                  break;
               case "GAN":
                  textbox16.IsEnabled = false;
                  checkbox2.IsEnabled = false;
                  combobox9.IsEnabled = false;
                  textbox17.IsEnabled = false;
                  checkbox3.IsEnabled = false;
                  checkbox4.IsEnabled = false;
                  break;
            }
         }
         catch { } // ignore error
      }

      private void Button_Click_11(object sender, RoutedEventArgs e)
      {
         var dlg = new Microsoft.Win32.OpenFileDialog();
         var result = dlg.ShowDialog();
         if (result == true)
         {
            textbox12.Text = dlg.FileName;
            textbox13.Text = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(dlg.FileName),
               System.IO.Path.GetFileNameWithoutExtension(dlg.FileName)
               + "-myfakeapp" + System.IO.Path.GetExtension(dlg.FileName));
         }
      }

      private void Button_Click_13(object sender, RoutedEventArgs e)
      {
         var dlg = new Microsoft.Win32.OpenFileDialog();
         var result = dlg.ShowDialog();
         if (result == true)
         {
            textbox13.Text = dlg.FileName;
         }
      }

      private void Button_Click_12(object sender, RoutedEventArgs e)
      {
         var dlg = new System.Windows.Forms.FolderBrowserDialog();
         var result = dlg.ShowDialog();
         if (result == System.Windows.Forms.DialogResult.OK)
         {
            textbox14.Text = dlg.SelectedPath;
         }
      }

      private void Button_Click_14(object sender, RoutedEventArgs e)
      {
         var dlg = new Microsoft.Win32.OpenFileDialog();
         var result = dlg.ShowDialog();
         if (result == true)
         {
            textbox15.Text = dlg.FileName;
         }
      }

      private void Button_Click_15(object sender, RoutedEventArgs e)
      {
         var tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), Guid.NewGuid().ToString());
         Directory.CreateDirectory(tempPath);
         try
         {
            var framesPath = CreateFrames(tempPath, out var audioPath);
            var swapedFrames = SwapFrames(tempPath, framesPath);
            CreateVideo(swapedFrames, audioPath);
         }
         finally
         {
            Directory.Delete(tempPath, true);
         }
      }

      private void CreateVideo(string swapedFrames, string audioPath)
      {
         var startInfo = new ProcessStartInfo();
         startInfo.FileName = GetAbsolutePath(@"lib\ffmpeg.exe");
         startInfo.UseShellExecute = false;
         startInfo.Arguments = string.Format("-y -i \"{0}\" -i \"{1}\" -acodec copy -c:v libx264 -vf fps={2} \"{3}\"",
            System.IO.Path.Combine(swapedFrames, "img%08d.png"), audioPath, textbox18.Text, textbox13.Text);
         using (var proc = new Process())
         {
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
            if (proc.ExitCode != 0)
            {
               MessageBox.Show("Could not create frames!", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
         }
      }

      private string SwapFrames(string tempPath, string framesPath)
      {
         var outputPath = System.IO.Path.Combine(tempPath, "swapedFrames");
         var processor = (string)((ComboBoxItem)combobox10.SelectedValue).Tag;
         var startInfo = new ProcessStartInfo();
         startInfo.FileName = GetAbsolutePath("lib\\python-" + processor + "\\python.exe");
         startInfo.UseShellExecute = false;
         startInfo.Arguments = BuildSwapFramesParameters(processor, framesPath, outputPath);
         using (var proc = new Process())
         {
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
            if (proc.ExitCode != 0)
            {
               MessageBox.Show("Could not swap faces!", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
         }
         return outputPath;
      }

      private string BuildSwapFramesParameters(string processor, string framesPath, string outputPath)
      {
         var converter = (string)((ComboBoxItem)combobox7.SelectedValue).Tag;
         var result = string.Format("lib\\faceswap.py convert -i \"{0}\" -o \"{1}\" -v -m \"{2}\" -t {3} -c {4} -D {5}",
            framesPath, outputPath, textbox14.Text, ((ComboBoxItem)combobox6.SelectedValue).Tag,
            converter, ((ComboBoxItem)combobox8.SelectedValue).Tag);

         if (checkbox1.IsChecked.HasValue && checkbox1.IsChecked.Value)
            result += " -s";
         if (!string.IsNullOrEmpty(textbox15.Text))
            result += string.Format(" -f \"{0}\"", textbox15.Text);
         switch (converter)
         {
            case "Masked":
               if (!string.IsNullOrEmpty(textbox16.Text))
                  result += string.Format(" -b {0}", textbox16.Text);
               if (checkbox2.IsChecked.HasValue && checkbox2.IsChecked.Value)
                  result += " -S";
               result += string.Format(" -M {0}", ((ComboBoxItem)combobox9.SelectedValue).Tag);
               if (!string.IsNullOrEmpty(textbox17.Text))
                  result += string.Format(" -e {0}", textbox17.Text);
               break;
            case "Adjust":
               if (checkbox3.IsChecked.HasValue && checkbox3.IsChecked.Value)
                  result += " -sm";
               if (checkbox4.IsChecked.HasValue && checkbox4.IsChecked.Value)
                  result += " -aca";
               break;
            default:
               break;
         }
         return result;
      }

      private string CreateFrames(string tempPath, out string audioPath)
      {
         var outputPath = System.IO.Path.Combine(tempPath, "frames");
         Directory.CreateDirectory(outputPath);
         audioPath = System.IO.Path.Combine(tempPath, "audio.aac");
         var startInfo = new ProcessStartInfo();
         startInfo.FileName = GetAbsolutePath(@"lib\ffmpeg.exe");
         startInfo.UseShellExecute = false;
         startInfo.Arguments = string.Format("-y -i \"{0}\" -an -vf fps={1}/1 \"{2}\" -vn -acodec copy {3}",
            textbox12.Text, textbox18.Text, System.IO.Path.Combine(outputPath, "img%08d.png"), audioPath);
         using (var proc = new Process())
         {
            proc.StartInfo = startInfo;
            proc.Start();
            proc.WaitForExit();
            if (proc.ExitCode != 0)
            {
               MessageBox.Show("Could not create frames!", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
         }
         return outputPath;
      }
   }
}
