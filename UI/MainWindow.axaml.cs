using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.views.vistaCalendario;

namespace UI;

public partial class MainWindow : Window
{
    private vistaCalendario _calendario;
    
    public MainWindow()
    {
        _calendario = new vistaCalendario();
        
        InitializeComponent();
    }

    private void CambiarVistaCalendario(object? sender, RoutedEventArgs routedEventArgs)
    {
        MainContentControl.Content = _calendario;
    }
}