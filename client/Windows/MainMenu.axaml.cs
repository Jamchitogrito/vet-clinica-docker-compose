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
            var listZapisi = await http.GetFromJsonAsync<List<DiagnozDto>>("http://localhost:3000/api/diagnozs");
            displayZapisi.ItemsSource = listZapisi;
            var listOwners = await http.GetFromJsonAsync<List<OwnerDto>>("http://localhost:3000/api/owners");
            displayOwners.ItemsSource = listOwners;
            var listPets = await http.GetFromJsonAsync<List<PetDto>>("http://localhost:3000/api/pets");
            displayPets.ItemsSource = listPets;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[MainMenu] не удалось загрузить диагнозы: {ex.Message}");
        }
    }
}