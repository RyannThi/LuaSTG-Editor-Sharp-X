using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LuaSTGEditorSharp.Windows.Input
{
    /// <summary>
    /// BulletInput.xaml 的交互逻辑
    /// </summary>
    public partial class BulletInput : InputWindow
    {
        public BulletInput(string s)
        {
            InitializeComponent();
            Result = s;

            /*
            if (TryGetOverrides("BulletInput.json", out var overrides))
            {
                BulletPanel.Children.Clear();

                foreach (var bulletDef in overrides)
                {
                    BitmapImage bmp = new(new Uri(Path.Combine(Environment.CurrentDirectory, "images_override", "bullets", bulletDef.ImagePath ?? bulletDef.Tag)));

                    Image content = new()
                    {
                        Style = (Style)FindResource("SelectionImg"),
                        Source = bmp
                    };

                    Button b = new()
                    {
                        Content = content,
                        Tag = bulletDef.Tag
                    };
                    b.Click += Style_Click;
                }
            }*/
        }

        private void Style_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button a) Result = a.Tag?.ToString();
            DialogResult = true;
            this.Close();
        }

        private void InputWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void InputWindow_Closed(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
