using Hardcodet.Wpf.TaskbarNotification;
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
using System.Drawing;
using System.Reflection;
using System.Diagnostics;

namespace WPF.Netf.PlayingWithHardcodetNotifyIcon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Taskbar Icon is Declare in xml as taskbarIcon 
            // the ToolTipText ( set in xml will show by default ). It just a text tool tip
            // in newer windows we can have shinny tool tips. 
            var shinnyToolTip = new MyTaskbarToolTip(); // Defined by TaskbarToolTip.xaml
            taskbarIcon.TrayToolTip = shinnyToolTip;
            taskbarIcon.TrayMouseDoubleClick += TaskbarIcon_TrayMouseDoubleClick;
            taskbarIcon.IconSource = LoadBitmapFromResource("face.ico");
            taskbarIcon.TrayBalloonTipClicked += TaskbarIcon_TrayBalloonTipClicked;

        }

        private void TaskbarIcon_TrayBalloonTipClicked(object sender, RoutedEventArgs e)
        {

            Debug.WriteLine("Ballon message was clicked."); 

        }

        private void TaskbarIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Tray was clicked DoubleClicked.");
            RestoreFromTray();
        }



        private void Window_Closed(object sender, EventArgs e)
        {
            // Closing App. Dispose the icon properly so it does not hang around. 
            taskbarIcon.Dispose();
        }

        private void buttonBalloonMessage_Click(object sender, RoutedEventArgs e)
        {
             taskbarIcon.ShowBalloonTip("Balloon Title", "This is a balloon tooltip added in win 7", BalloonIcon.Info);
            // it was a small while cartoon speech bubble look on win 7+8. on win 10 its a blocky notifcation.
            // Notification autohides after a short delay. 

        }

        private void MinimiseToTray()
        {
            // Hide window in Ap bar 
            mainWindow.WindowState = WindowState.Minimized;
            mainWindow.ShowInTaskbar = false;
        }

        private void RestoreFromTray()
        {
            mainWindow.WindowState = WindowState.Normal;
            mainWindow.ShowInTaskbar = true;
            mainWindow.Activate();
        }

        private void buttonMinimiseToTray_Click(object sender, RoutedEventArgs e)
        {
            MinimiseToTray();
        }

        /// <summary>
        /// Load a resource WPF-BitmapImage (png, bmp, ...) from embedded resource defined as 'Resource' not as 'Embedded resource'.
        /// </summary>
        /// <param name="pathInApplication">Path without starting slash</param>
        /// <param name="assembly">Usually 'Assembly.GetExecutingAssembly()'. If not mentionned, I will use the calling assembly</param>
        /// <returns></returns>
        public static BitmapImage LoadBitmapFromResource(string pathInApplication, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetCallingAssembly();
            }

            if (pathInApplication[0] == '/')
            {
                pathInApplication = pathInApplication.Substring(1);
            }
            return new BitmapImage(new Uri(@"pack://application:,,,/" + assembly.GetName().Name + ";component/" + pathInApplication, UriKind.Absolute));
        }
    }
}
