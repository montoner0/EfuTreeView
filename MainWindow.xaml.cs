﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using EfuTreeView.ViewModel;
using Microsoft.Win32;

namespace EfuTreeView
{
    public partial class MainWindow : Window
    {
        private readonly TreeViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            //SetValue(RenderOptions.EdgeModeProperty, EdgeMode.Aliased);
            TextOptions.SetTextFormattingMode(this, TextFormattingMode.Display);
            //TextOptions.SetTextRenderingMode(this, TextRenderingMode.Aliased);
            //SetValue(TextOptions.TextRenderingModeProperty, TextRenderingMode.Aliased);
            //SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            //UseLayoutRounding = true;
            // Добавляем обработчик для всех кнопок на гриде
            //viewModel=
            _viewModel = new TreeViewModel();
            DataContext = _viewModel;
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

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
                _viewModel.Load(ofd.FileName);
            }
        }
    }
}