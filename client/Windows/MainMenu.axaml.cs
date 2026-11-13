using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using client.dto;

namespace client.Windows;

public partial class MainMenu : Window
{
    private static readonly HttpClient http = new HttpClient();

    public MainMenu()
    {
        InitializeComponent();
        Opened += OnOpened;
    }
    private async void OnOpened(object? sender, EventArgs e)
    {
        try
        {
            var list = await http.GetFromJsonAsync<List<DiagnozDto>>("http://localhost:3000/api/diagnozs");

            displayZapisi.ItemsSource = list;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[MainMenu] не удалось загрузить диагнозы: {ex.Message}");
        }
    }
}