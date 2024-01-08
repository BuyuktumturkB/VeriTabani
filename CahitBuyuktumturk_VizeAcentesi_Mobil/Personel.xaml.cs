
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CahitBuyuktumturk_VizeAcentesi_Mobil;

public partial class Personel : ContentPage
{

	public Personel()
	{
		InitializeComponent();

		Calisan.ItemsSource = Calisanlar;
		if (File.Exists(dosyaadý))
		{
			string data = File.ReadAllText(dosyaadý);
			calisanlar = JsonSerializer.Deserialize<ObservableCollection<Calisanlar>>(data);

		}
	}
	string dosyaadý = Path.Combine(FileSystem.AppDataDirectory, "data.json");
	public ObservableCollection<Calisanlar> Calisanlar => calisanlar;
	ObservableCollection<Calisanlar> calisanlar = new() {
		new Calisanlar(){Personelid = "P01",Personelkimlik="1211241121",Personelisim="Ali",PersonelSoyisim="Bilir",PersonelTelefon="02127214166" },};

	static FirebaseClient client = new FirebaseClient("https://veritabaniyonetimvizeacenta-default-rtdb.firebaseio.com/");
	public async void PersonelKayit(Calisanlar calisanlar)
	{
		PersonelKayit personelKayit = new PersonelKayit() { };
		await Navigation.PushModalAsync(personelKayit);
	}
	private async void YeniPersonel(object sender, EventArgs e)
	{
		PersonelKayit personelKayit = new PersonelKayit() { Title = "Title", EkleGorevli = KaydetPersonel };
		await Navigation.PushModalAsync(personelKayit);
	}
	public async void KaydetPersonel(Calisanlar calisan)
	{
		await client.Child("Personel").PostAsync(calisan);
		calisanlar.Add(calisan);

	}

	private async void PersonelSil(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		var sil = await DisplayAlert("Sil", "Emin Misiniz?", "Evet", "Hayýr");
		if (sil)
		{
			var temizle = button.BindingContext as Calisanlar;
			if (temizle != null)
			{
				calisanlar.Remove(temizle);
				await client.Child("Personel").DeleteAsync();

			}
		}
	}
	private async void PersonelDuzenle(object sender, EventArgs e)
	{
		var duzenle = (Button)sender;
		if (duzenle != null)
		{
			await client.Child("Personel").PutAsync(calisanlar);
			var id = duzenle.CommandParameter.ToString();
			var sondurum = Calisanlar.Single(o => o.ID == id);

			PersonelKayit personelKayit = new PersonelKayit() { Title = "Title",KayýtPersonel=sondurum };
			await Navigation.PushModalAsync(personelKayit);

		}
	}
}
public class Calisanlar : INotifyPropertyChanged
{
	public string personelid, personelisim, personelsoyisim, personeltckn, personeltelefon;

	public string ID
	{
		get
		{
			if (Personelid == null)
			{
				Personelid=Guid.NewGuid().ToString();
				
			}
			return Personelid;
		}
		set { personelid = value; }
	}

	public string Personelid { get => personelid; set { personelid = value; NotifyPropertyChanged(); } }
	public string Personelkimlik { get => personeltckn; set { personeltckn = value; NotifyPropertyChanged(); } }
	public string Personelisim { get => personelisim; set { personelisim = value; NotifyPropertyChanged(); } }
	public string PersonelSoyisim
	{
		get => personelsoyisim; set
		{
			personelsoyisim = value;
			NotifyPropertyChanged();
		}
	}
	public string PersonelTelefon
	{
		get => personeltelefon; set
		{
			personeltelefon = value;
			NotifyPropertyChanged();
		}
	}
	public event PropertyChangedEventHandler PropertyChanged;
	public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	}
}