using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
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
        await ReloadAllAsync();
    }

    private async Task ReloadAllAsync()
    {
        try
        {
            var owners = await http.GetFromJsonAsync<List<OwnerDto>>("http://localhost:3000/api/owners");
            displayOwners.ItemsSource = owners;

            var pets = await http.GetFromJsonAsync<List<PetDto>>("http://localhost:3000/api/pets");
            displayPets.ItemsSource = pets;

            var diagnozs = await http.GetFromJsonAsync<List<DiagnozDto>>("http://localhost:3000/api/diagnozs");
            displayZapisi.ItemsSource = diagnozs;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[MainMenu] load error: {ex.Message}");
        }
    }


    private async void OwnerAdd_Click(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ownerFio.Text)) return;
        if (string.IsNullOrWhiteSpace(ownerTelefon.Text)) return;

        var body = new
        {
            fio = ownerFio.Text,
            telefon = ownerTelefon.Text,
            adres = ownerAdres.Text
        };

        var r = await http.PostAsJsonAsync("http://localhost:3000/api/owners", body);
        Console.WriteLine($"[Owner add] {(int)r.StatusCode}");
        if (r.IsSuccessStatusCode)
        {
            ownerId.Text = ownerFio.Text = ownerTelefon.Text = ownerAdres.Text = "";
            await ReloadAllAsync();
        }
    }

    private async void OwnerUpdate_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(ownerId.Text, out int id)) return;
        if (string.IsNullOrWhiteSpace(ownerFio.Text)) return;
        if (string.IsNullOrWhiteSpace(ownerTelefon.Text)) return;

        var body = new
        {
            fio = ownerFio.Text,
            telefon = ownerTelefon.Text,
            adres = ownerAdres.Text
        };

        var r = await http.PutAsJsonAsync($"http://localhost:3000/api/owners/{id}", body);
        Console.WriteLine($"[Owner update] {(int)r.StatusCode}");
        if (r.IsSuccessStatusCode) await ReloadAllAsync();
    }

    private async void OwnerDelete_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(ownerId.Text, out int id)) return;

        var r = await http.DeleteAsync($"http://localhost:3000/api/owners/{id}");
        Console.WriteLine($"[Owner delete] {(int)r.StatusCode}");
        if (r.IsSuccessStatusCode)
        {
            ownerId.Text = "";
            await ReloadAllAsync();
        }
    }


    private async void PetAdd_Click(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(petKlichka.Text)) return;
        if (!int.TryParse(petOwnerId.Text, out int ownerId)) return;
        int? vozrast = int.TryParse(petVozrast.Text, out var v) ? v : null;

        var body = new
        {
            klichka = petKlichka.Text,
            vid = petVid.Text,
            poroda = petPoroda.Text,
            vozrast = vozrast,
            ownerId = ownerId
        };

        var r = await http.PostAsJsonAsync("http://localhost:3000/api/pets", body);
        Console.WriteLine($"[Pet add] {(int)r.StatusCode}");
        if (r.IsSuccessStatusCode)
        {
            petId.Text = petKlichka.Text = petVid.Text = petPoroda.Text = petVozrast.Text = petOwnerId.Text = "";
            await ReloadAllAsync();
        }
    }

    private async void PetUpdate_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(petId.Text, out int id)) return;
        if (string.IsNullOrWhiteSpace(petKlichka.Text)) return;
        if (!int.TryParse(petOwnerId.Text, out int ownerId)) return;
        int? vozrast = int.TryParse(petVozrast.Text, out var v) ? v : null;

        var body = new
        {
            klichka = petKlichka.Text,
            vid = petVid.Text,
            poroda = petPoroda.Text,
            vozrast = vozrast,
            ownerId = ownerId
        };

        var r = await http.PutAsJsonAsync($"http://localhost:3000/api/pets/{id}", body);
        Console.WriteLine($"[Pet update] {(int)r.StatusCode}");
        if (r.IsSuccessStatusCode) await ReloadAllAsync();
    }

    private async void PetDelete_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(petId.Text, out int id)) return;

        var r = await http.DeleteAsync($"http://localhost:3000/api/pets/{id}");
        Console.WriteLine($"[Pet delete] {(int)r.StatusCode}");
        if (r.IsSuccessStatusCode)
        {
            petId.Text = "";
            await ReloadAllAsync();
        }
    }


    private async void DiagnozAdd_Click(object? sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(diagnozName.Text)) return;
        int? vrachId  = int.TryParse(diagnozVrachId.Text, out var v) ? v : null;
        int? uslugaId = int.TryParse(diagnozUslugaId.Text, out var u) ? u : null;
        int? petId    = int.TryParse(diagnozPetId.Text, out var p) ? p : null;

        var body = new
        {
            name = diagnozName.Text,
            stadia = diagnozStadia.Text,
            vrachId = vrachId,
            uslugaId = uslugaId,
            petId = petId
        };

        var r = await http.PostAsJsonAsync("http://localhost:3000/api/diagnozs", body);
        Console.WriteLine($"[Diagnoz add] {(int)r.StatusCode}");
        if (r.IsSuccessStatusCode)
        {
            diagnozId.Text = diagnozName.Text = diagnozStadia.Text =
                diagnozVrachId.Text = diagnozUslugaId.Text = diagnozPetId.Text = "";
            await ReloadAllAsync();
        }
    }

    private async void DiagnozUpdate_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(diagnozId.Text, out int id)) return;
        if (string.IsNullOrWhiteSpace(diagnozName.Text)) return;
        int? vrachId  = int.TryParse(diagnozVrachId.Text, out var v) ? v : null;
        int? uslugaId = int.TryParse(diagnozUslugaId.Text, out var u) ? u : null;
        int? petId    = int.TryParse(diagnozPetId.Text, out var p) ? p : null;

        var body = new
        {
            name = diagnozName.Text,
            stadia = diagnozStadia.Text,
            vrachId = vrachId,
            uslugaId = uslugaId,
            petId = petId
        };

        var r = await http.PutAsJsonAsync($"http://localhost:3000/api/diagnozs/{id}", body);
        Console.WriteLine($"[Diagnoz update] {(int)r.StatusCode}");
        if (r.IsSuccessStatusCode) await ReloadAllAsync();
    }

    private async void DiagnozDelete_Click(object? sender, RoutedEventArgs e)
    {
        if (!int.TryParse(diagnozId.Text, out int id)) return;

        var r = await http.DeleteAsync($"http://localhost:3000/api/diagnozs/{id}");
        Console.WriteLine($"[Diagnoz delete] {(int)r.StatusCode}");
        if (r.IsSuccessStatusCode)
        {
            diagnozId.Text = "";
            await ReloadAllAsync();
        }
    }
}