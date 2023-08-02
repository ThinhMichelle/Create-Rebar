using RvtWPFTemplate.UI;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RvtWPFTemplate
{
    /// <summary>
    /// Interaction logic for Create_Rebar.xaml
    /// </summary>
    public partial class Create_Rebar : Window
    {
        public static Create_Rebar Instance { get; set; } = null;
        public Create_Rebar(ColorViewModel vm)
        {
            #region Set ower revit window for Wpf form
            if (!ColorViewModel.IsOpen) //Enforce single window
            {
                InitializeComponent();
                Instance = this;
                var uiapp = ColorViewModel.Uiapp;
                IntPtr revitWindow;

#if REVIT2018
                revitWindow = System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle; // 2018
#else
                revitWindow = uiapp.MainWindowHandle; //Revit 2019 and above
#endif

                //Get window of Revit form Revit handle
                HwndSource hwndSource = HwndSource.FromHwnd(revitWindow);
                var windowRevitOpen = hwndSource.RootVisual as Window;
                #endregion


                this.Owner = windowRevitOpen; //Set onwer for WPF window
                this.DataContext = vm;

                if (vm.DisplayUI())
                {
                    this.Show(); //Modeless form
                }

            }
            //Check: if Wpf window is minimized, re-open it
            if (Instance?.WindowState == WindowState.Minimized)
                Instance.WindowState = WindowState.Normal;
        }
    }
    
}
