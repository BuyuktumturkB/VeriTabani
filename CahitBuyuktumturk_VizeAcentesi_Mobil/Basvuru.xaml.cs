using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CahitBuyuktumturk_VizeAcentesi_Mobil;

public partial class Basvuru : ContentPage
{
	public Basvuru()
	{
		InitializeComponent();
		Basvurular.ItemsSource = basvurular;
	}
	public ObservableCollection<Applications> basvuru => basvurular;
	ObservableCollection<Applications> basvurular = new() { new Applications() { ApplicationNumber = "B0", CustomerNumber = "M0", PersonalNumver = "P0", CountryID = "UK",
		TotalPrice = "150", RejectionRate = "0" } , new Applications(){ApplicationNumber="B1", CustomerNumber="M0",PersonalNumver="P1",CountryID="EU",TotalPrice="50",RejectionRate="0,25" } };
	static FirebaseClient client = new FirebaseClient("https://veritabaniyonetimvizeacenta-default-rtdb.firebaseio.com/");
	string dosyaadý = Path.Combine(FileSystem.AppDataDirectory, "data.json");
	public void BasvuruEkle(object sender, EventArgs e)
	{
		BasvuruKayit basvuruKayit = new BasvuruKayit() { Title = "Title", basvuruislemi=BasvuruKaydet };
		Navigation.PushAsync(basvuruKayit);
	}

	public async void BasvuruKaydet(Applications applications)
	{
		await client.Child("Basvurular").PostAsync(applications);
		basvurular.Add(applications);
	}
	public async void BasvuruSil(object sender, EventArgs e)
	{
		Button button = (Button)sender;
		var sil = await DisplayAlert("Baþvuru Silinecek", "Emin Misiniz?", "Evet", "Hayýr");
		if (sil)
		{
			var temizle = button.BindingContext as Applications;
			if (temizle != null)
			{
				basvurular.Remove(temizle);
				await client.Child("Basvurular").DeleteAsync();

			}
		}
	}
	public async void BasvuruDuzenle(object sender,EventArgs e) 
	{
		var duzenle = (Button)sender;
		if (duzenle != null)
		{
			await client.Child("Basvurular").PutAsync(basvurular);
			var id = duzenle.CommandParameter.ToString();
			var sondurum = basvurular.Single(o => o.ID == id);

			//BasvuruKayit basvuruduzenle = new BasvuruKayit() { Title = "Title", basvuruislemi =sondurum };
			//await Navigation.PushModalAsync(basvuruduzenle);

		}
	}
}
public class Applications : INotifyPropertyChanged
{
	public string applicationnumber, customernumber, personalnumber, apllicationdate, totalprice, countryid,rejectionrate;

	public string ApplicationNumber { get => applicationnumber; set { applicationnumber = value; NotifyPropertyChanged(); } }
	public string CustomerNumber { get => customernumber; set { customernumber = value; NotifyPropertyChanged(); } }
	public string PersonalNumver { get => personalnumber; set { personalnumber = value; NotifyPropertyChanged(); } }
	public string CountryID { get => countryid; set {  countryid = value; NotifyPropertyChanged(); } }
	public string TotalPrice { get => totalprice; set { totalprice = value; NotifyPropertyChanged(); }
		
	}
	public string ID
	{
		get
		{
			if (ApplicationNumber == null)
			{
				ApplicationNumber = Guid.NewGuid().ToString();

			}
			return ApplicationNumber;
		}
		set { applicationnumber = value; }
	}

	public string RejectionRate { get =>rejectionrate; set { rejectionrate = value; NotifyPropertyChanged(); } }


	private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public event PropertyChangedEventHandler PropertyChanged;
}