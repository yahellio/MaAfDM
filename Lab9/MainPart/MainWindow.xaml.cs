using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Forms;
using ImageLoader;
using NeuralNetwork;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace MainPart
{
    public partial class MainWindow : Window
    {
        private const int _imageSize = 30;
        private Bitmap _bitmap;
        private NamedNeuralNetwork _network;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClassificationButton_Click(object sender, RoutedEventArgs e)
        {
            var vector = BitmapConverter.ToInt32List(_bitmap);
            var predictedClass = _network.GetAnswer(vector);

            ClassificationResultLabel.Text = predictedClass;
        }

        private void LoadImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter ="Images (*.bmp, *.png, *.jpg, *.jpeg)|*.bmp;*.png;*.jpg;*.jpeg|Все файлы|*.*"
            };

            if (!(openFileDialog.ShowDialog() ?? false))
            {
                return;
            }
            
            _bitmap = BitmapConverter.Load(openFileDialog.FileName, _imageSize);
            CurrentImage.Source = BitmapConverter.ToBitmapImage(_bitmap);

            ClassificationButton.IsEnabled = true;
        }

        private void OpenNetworkButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Load network (*.snn)|*.snn"
            };

            if (openFileDialog.ShowDialog() ?? false)
            {
                using (var fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    var serializer = new BinaryFormatter();
                    _network = (NamedNeuralNetwork)serializer.Deserialize(fileStream);
                }

                LoadImageButton.IsEnabled = true;

                //TrainingProgressBar.Value = 100;
                TrainingStatusLabel.Text = "Network successfully loaded.";
            }
        }

        private void TeachingButton_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            var directory = new DirectoryInfo(folderBrowserDialog.SelectedPath);
            var neuronsNames = GetNeuronsNames(directory);
            _network = new NamedNeuralNetwork(_imageSize * _imageSize, neuronsNames);

            int iteration = 0;
            while (!IsTeachingEnd(directory))
            {
                iteration++;
                TeachFromDirectory(directory);

                int correctCount = directory.GetFiles("*.png").Count(file =>
                {
                    var expected = file.Name.Split('-')[0];
                    var predicted = _network.GetAnswer(GetElementFromPath(directory, file));
                    return expected == predicted;
                });

                double accuracy = 100.0 * correctCount / directory.GetFiles("*.png").Length;
                //TrainingProgressBar.Value = accuracy;
                TrainingStatusLabel.Text = $"Iteration: {iteration}, accuracy: {accuracy:0.00}%";

                DoEvents();
            }

            LoadImageButton.IsEnabled = true;
            //SaveNetworkButton.IsEnabled = true;
        }

        private void DoEvents()
        {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background,
                new Action(delegate { }));
        }

        private void SaveNetworkButton_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Save network (*.snn)|*.snn"
            };

            if (! (saveFileDialog.ShowDialog() ?? false))
            {
                return;
            } 

            using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
            {
                var serializer = new BinaryFormatter();
                serializer.Serialize(fileStream, _network);
            }
        }

        private static List<string> GetNeuronsNames(DirectoryInfo directory)
        {
            var neuronsNames = new List<string>();
            foreach (var file in directory.GetFiles())
            {
                var neuronNames = file.Name.Split('-')[0];
                if (! neuronsNames.Contains(neuronNames))
                {
                    neuronsNames.Add(neuronNames);
                }
            }

            return neuronsNames;
        }

        private bool IsTeachingEnd(DirectoryInfo directory)
        {
            return ! (from file in directory.GetFiles("*.png")
                     let neuronName = file.Name.Split('-')[0]
                     where _network.GetAnswer(GetElementFromPath(directory, file)) != neuronName
                     select file).Any();
        }

        private void TeachFromDirectory(DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles("*.png"))
            {
                var neuronName = file.Name.Split('-')[0];

                _network.Teaching(GetElementFromPath(directory, file), neuronName);
            }
        }

        private static List<int> GetElementFromPath(DirectoryInfo directory, FileInfo file)
        {
            return BitmapConverter.ToInt32List(BitmapConverter.Load($"{directory}\\{file.Name}", _imageSize));
        }
    }
}