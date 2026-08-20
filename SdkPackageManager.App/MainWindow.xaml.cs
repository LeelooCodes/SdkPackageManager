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

using SdkPackageManager.App.Interop;

namespace SdkPackageManager.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TestNative_Click(object sender, RoutedEventArgs e)
        {
            int result = NativeMethods.CompareVersions(
                1, 2, 0,
                1, 4, 0);

            NativeResultText.Text = $"Native result: {result}";
        }
    }
}