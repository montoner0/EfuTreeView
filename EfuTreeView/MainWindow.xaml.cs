﻿using System.Windows;
using System.Windows.Media;
using EfuTreeView.ViewModel;
using Microsoft.Win32;

namespace EfuTreeView
{
    public partial class MainWindow : Window
    {
        private readonly string _appName = "EFU Tree Viewer";
        private FolderNodeViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            //SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            TextOptions.SetTextFormattingMode(this, TextFormattingMode.Display);
            //TextOptions.SetTextRenderingMode(this, TextRenderingMode.Aliased);
            //SetValue(TextOptions.TextRenderingModeProperty, TextRenderingMode.Aliased);
            //SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            //UseLayoutRounding = true;
            Title = _appName;
#if DEBUG
            var dummyData = new DummyEfuParser();
            _viewModel = new FolderNodeViewModel(dummyData.GetNodes());
            DataContext = _viewModel;
#endif
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void MenuItemOpen_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog() {
                CheckFileExists = true,
                DefaultExt = "efu",
                Filter = "EFU Everything File List|*.efu|All Files|*.*",
                Title = "Open EFU file",
                ValidateNames = true
            };

            if (ofd.ShowDialog(this) == true) {
                Title = $"{_appName} - {ofd.FileName}";
                _viewModel = new FolderNodeViewModel(EfuParser.BuildEfuData(ofd.FileName).GetNodes());
                DataContext = _viewModel;
            }
        }
    }
}