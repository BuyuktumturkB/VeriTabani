using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CahitBuyuktumturk_VizeAcentesi_Mobil;

public partial class Ulkeler : ContentPage
{
	public Ulkeler()
	{
		InitializeComponent();
		Ulke.ItemsSource = Countries;
		if (File.Exists(dosyaadý))
		{
			string data = File.ReadAllText(dosyaadý);
			countries = JsonSerializer.Deserialize<ObservableCollection<Countries>>(data);

		}
	}
	string dosyaadý = Path.Combine(FileSystem.AppDataDirectory, "data.json");

	public async void Ulkeekle(object sender, EventArgs e) 
	{
		UlkeEkle ulkeEkle = new UlkeEkle() {  Title = "Title", CountryAction = UlkeKaydet  };
		await Navigation.PushAsync(ulkeEkle);
	}
	static FirebaseClient client = new FirebaseClient("https://veritabaniyonetimvizeacenta-default-rtdb.firebaseio.com/");
	public ObservableCollection<Countries> Countries => countries;
	ObservableCollection<Countries> countries=new () {
		new Countries() 
		{
			countrcode="EU",
			countryname="Avrupa Birliði",
			visaprice="80.00",
			embassyphone="----",
			embassyadress="------"
		}
	};
	
	public async void UlkeKaydet(Countries country) 
	{
		await client.Child("Ulkeler").PostAsync(country);
		countries.Add(country);
		
	}
	public async void UlkeKaldir(object sender, EventArgs e) 
	{
		Button button = (Button)sender;
		var sil = await DisplayAlert("Ülke Bilgisi Silinecektir ", "Emin Misiniz?", "Evet", "Hayýr");
		if (sil)
		{
			var temizle = button.BindingContext as Countries;
			if (temizle != null)
			{
				countries.Remove(temizle);
				await client.Child("Ulkeler").DeleteAsync();
			}
		}
	}
	public async void UlkeDuzenle(object sender, EventArgs e)
	{
		var duzenle = (Button)sender;
		if (duzenle != null)
		{
			var id = duzenle.CommandParameter.ToString();
			var sondurum = Countries.Single(o => o.ID == id);
			await client.Child("Ulkeler").PutAsync(sondurum);

			UlkeEkle ulkeduzenle = new UlkeEkle() { Title = "Title", country = sondurum };
			await Navigation.PushModalAsync(ulkeduzenle);
		}
	}

}


public class Countries:INotifyPropertyChanged
{
	public string ID
	{
		get
		{
			if (Countrycode == null)
			{
				Countrycode = Guid.NewGuid().ToString();

			}
			return Countrycode;
		}
		set { countrcode = value; }
	}
	public string countrcode, countryname, visaprice, embassyphone,embassyadress;

	public string Countrycode { get => countrcode; set { countrcode = value;  NotifyPropertyChanged(); } }
	public string Countryname { get => countryname; set {  countryname = value; NotifyPropertyChanged(); } }
	public string VisaPrice { get => visaprice; set { visaprice = value;NotifyPropertyChanged(); } }

	public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	}
	public string Embassyadress { get => embassyadress; set { embassyadress = value; NotifyPropertyChanged(); } }
	public string EmbassyPhone { get => embassyphone; set { embassyphone = value; NotifyPropertyChanged(); } }

	public event PropertyChangedEventHandler PropertyChanged;
}