using System;
using System.Collections.Generic;
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

namespace WordsFromWords
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string mainWord;
        public string newWord;
        public char[] parseWord;
        public Random rnd = new Random();
        public List<string> allWords = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            mainWord = "КОВЁР";
            allWords.Add("КОВЁР");
            allWords.Add("КРОВ");
            allWords.Add("ВОР");
            allWords.Add("РЁВ");
            allWords.Add("РОВ");
            allWords.Add("РОК");
            parseWord = mainWord.ToCharArray();

            foreach (int i in Enumerable.Range(0, parseWord.Length).OrderBy(x => rnd.Next()))
            {
                char ch = parseWord[i];
                Button newBtn = new Button
                {
                    Content = ch,
                    Width = 50,
                    Height = 50,
                    FontFamily = new FontFamily("Molot"),
                    FontSize = 36,
                    Foreground = new SolidColorBrush(Colors.White),
                    Background = new SolidColorBrush(Color.FromRgb(104, 104, 104))
                };
                newBtn.Click += new RoutedEventHandler(btn_click);
                uniformBoard.Children.Add(newBtn);
            }

            rightWords.Text = $"Осталось отгадать слов:\n{allWords.Count}";
        }

        private void btn_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            newWord += button.Content.ToString();

            MainTextBox.Text = newWord;
        }

        private void bckspc_btn_Click(object sender, RoutedEventArgs e)
        {
            if (newWord != null && newWord.Length - 1 >= 0)
            {
                newWord = newWord.Remove(newWord.Length - 1);

                MainTextBox.Text = newWord;
            }
        }

        private void check_btn_Click(object sender, RoutedEventArgs e)
        {
            if (allWords.Contains(newWord))
            {
                MessageBox.Show("Right word!", "Words from Words", MessageBoxButton.OK, MessageBoxImage.Information);
                allWords.Remove(newWord);

                newWord = "";

                MainTextBox.Text = newWord;

                rightWords.Text = $"Осталось отгадать слов:\n{allWords.Count}";

                if (allWords.Count <= 0)
                {
                    MessageBox.Show("You Win!", "Words from Words", MessageBoxButton.OK, MessageBoxImage.Information);
                    System.Windows.Application.Current.Shutdown();
                }
            }
            else
                MessageBox.Show("Wrong word or it was used!", "Words from Words", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void exit_btn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
