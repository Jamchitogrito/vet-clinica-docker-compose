using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using client.dto;
using client.Windows;
using System.Text.Json;

namespace client;

public partial class MainWindow : Window
{
    private static readonly HttpClient http = new HttpClient();
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        
    }

    private async void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
{
    try
    {
        string? login = inputLogin.Text;
        if (string.IsNullOrEmpty(login))
        {
            errorDisplay.Text = "Введите логин";
            return;
        }

        var url = $"http://localhost:3000/api/vrachs/login?login={Uri.EscapeDataString(login)}";
        var response = await http.PostAsync(url, null);

        if (!response.IsSuccessStatusCode)
        {
            errorDisplay.Text = $"Неверный логин ({(int)response.StatusCode})";
            return;
        }

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var vrachDto = await response.Content.ReadFromJsonAsync<VrachDto>(options);
        if (vrachDto == null)
        {
            errorDisplay.Text = "Пустой ответ от сервера";
            return;
        }

        Console.WriteLine($"DTO: id={vrachDto.Id}, fio={vrachDto.Fio}, login={vrachDto.Login}");

        var mainMenu = new MainMenu();
        await mainMenu.ShowDialog(this);
    }
    catch (Exception ex)
    {
        errorDisplay.Text = $"Ошибка: {ex.Message}";
        Console.WriteLine(ex);
    }
}
}