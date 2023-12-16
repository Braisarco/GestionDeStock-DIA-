using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using Avalonia.Controls;

namespace UI.views.vistaCalendario;

public partial class vistaCalendario : UserControl
{
    private List<string> cousas = new List<string>();
    public vistaCalendario()
    {
        cousas.Add("hola");
        cousas.Add("chao");
        InitializeComponent();
        
        Calendar calendario = this.FindControl<Calendar>("calendario");
        Label dia = this.FindControl<Label>("pickedDate");
        TextBlock eventos = this.FindControl<TextBlock>("eventos");
        
        calendario.SelectedDatesChanged += (_,_) =>
        {
            dia.Content = "Dia: " + calendario.SelectedDate.Value;
            
            StringBuilder contenidoDia = new StringBuilder();
            foreach (string cousa in cousas)
            {
                contenidoDia.Append($"- {cousa}\n");
                
            }

            string contenido = contenidoDia.ToString();
            eventos.Text = contenido;
        };
    }
}