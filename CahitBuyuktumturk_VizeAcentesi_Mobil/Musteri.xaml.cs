using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CahitBuyuktumturk_VizeAcentesi_Mobil;

public partial class Musteri : ContentPage
{
	public Musteri()
	{
		InitializeComponent();
		Musteriler.ItemsSource = Customer;
		if (File.Exists(dosyaadý))
		{
			string data = File.ReadAllText(dosyaadý);
			customer = JsonSerializer.Deserialize<ObservableCollection<Customer>>(data);

		}
	}
	string dosyaadý = Path.Combine(FileSystem.AppDataDirectory, "data.json");
	static FirebaseClient client = new FirebaseClient("https://veritabaniyonetimvizeacenta-default-rtdb.firebaseio.com/");
	public ObservableCollection<Customer> Customer => customer;
	ObservableCollection<Customer> customer = new()
	{
		new Customer()
		{
			Customerid = "M001",
			CustomerName = "Tahir",
			CustomerSurname = "Gider",
			CustomerTCKN = "1111111111",
			CustomerPassportNumber = "S000000000",
			CustomerTelephone = "111111111111",
			PassportValidate = "01,01,2025"
		}
	};
	public async void YeniMusteri(object sender,EventArgs e) 
	{
		MusteriKayit musterikaydet = new MusteriKayit() {Title="Title",CustomersAction=KaydetMusteri };
		await Navigation.PushAsync(musterikaydet);
	}
	public async void KaydetMusteri(Customer newcustomers) 
	{
		await client.Child("Musteri").PostAsync(newcustomers);
		customer.Add(newcustomers);

		BasvuruKayit basvuruKayitPage = GetBasvuruKayitPage();
		/*if (basvuruKayitPage != null)
		{
			
			basvuruKayitPage.customersave.Add(newcustomers);

			
			basvuruKayitPage.UpdatePickerDataSource();
		} */

	}

	private BasvuruKayit GetBasvuruKayitPage()
	{
		if (Application.Current.MainPage is NavigationPage navigationPage)
		{
			return navigationPage.RootPage.Navigation.NavigationStack
				.FirstOrDefault(page => page is BasvuruKayit) as BasvuruKayit;
		}

		return null;
	}

	public async void MusteriSil(object sender, EventArgs e) 
	{
		Button button = (Button)sender;
		var sil = await DisplayAlert("Müþteri Sil ", "Emin Misiniz?", "Evet", "Hayýr");
		if (sil) 
		{
			var temizle = button.BindingContext as Customer;
			if (temizle != null) 
			{
				customer.Remove(temizle);
				await client.Child("Musteri").DeleteAsync();
			}
		}

	}
	public async void MusteriDuzenle(object sender,EventArgs e) 
	{
		var duzenle = (Button)sender;
		if (duzenle != null) 
		{
			await client.Child("Musteri").PatchAsync(customer);
			var id = duzenle.CommandParameter.ToString();
			var sondurum=Customer.Single(o => o.ID == id);

			MusteriKayit musteriKayit= new MusteriKayit() { Title="Title",Customers=sondurum};
			await Navigation.PushAsync(musteriKayit);
		}
	}
	
	
}
public class Customer : INotifyPropertyChanged
{
	private string customerid,customername,customertckn,customersurname,customerpassportnumber,customertel,passportvalidateuntill;
	public string ID
	{
		get
		{
			if (Customerid == null)
			{
				Customerid = Guid.NewGuid().ToString();

			}
			return Customerid;
		}
		set { customerid = value; }
	}
	public string Customerid { get => customerid; set { customerid = value; NotifyPropertyChanged(); } }
	public string CustomerName { get => customername; set {  customername = value; NotifyPropertyChanged(); } }
	public string CustomerSurname { get => customersurname; set { customersurname=value; NotifyPropertyChanged(); } }
	public string CustomerTCKN { get => customertckn; set { customertckn = value; NotifyPropertyChanged(); } }
	public string CustomerPassportNumber { get => customerpassportnumber; set
		{
			customerpassportnumber = value;
			NotifyPropertyChanged();
		} }
	public string CustomerTelephone { get => customertel; set
		{
			customertel = value;
			NotifyPropertyChanged();
		} }
	public string PassportValidate { get => passportvalidateuntill; set
		{
			passportvalidateuntill = value;
			NotifyPropertyChanged();
		} }

	public event PropertyChangedEventHandler PropertyChanged;
	public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

	}
}
