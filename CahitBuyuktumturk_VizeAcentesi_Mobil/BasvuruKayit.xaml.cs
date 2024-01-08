using System.Collections.ObjectModel;

namespace CahitBuyuktumturk_VizeAcentesi_Mobil;

public partial class BasvuruKayit : ContentPage
{
	public BasvuruKayit()
	{
		InitializeComponent();
		Musteriid.ItemsSource = customersave;
		Musteriid.ItemDisplayBinding = new Binding("CustomerNumber");

		Ulkeid.ItemsSource = app;
		Ulkeid.ItemDisplayBinding = new Binding("CountryID");
		BindingContext = new Musteri();

		Personelnum.ItemsSource = personels;
		Personelnum.ItemDisplayBinding = new Binding("PersonelNumver");
		
		




	}
	public Applications application;
	public Action<Applications> basvuruislemi { get; internal set; }
	public Customer customer;
	public Calisanlar personel;
	public Countries country;
	public List<Applications> app = new List<Applications>{ new Applications(){CountryID="EU" },new Applications(){CountryID="UK" },new Applications(){CountryID="USA" },
		new Applications(){CountryID="CND" },new Applications(){CountryID="CHN" } };
	public Action<Calisanlar> personela { get; internal set; }
	public List<Applications> personels = new List<Applications>() { new Applications { PersonalNumver = "P0" }, new Applications() { PersonalNumver = "P1" }, new Applications(){ PersonalNumver = "P2" } };
	public List<Applications> customersave = new List<Applications> { new Applications { CustomerNumber = "M0" }, new Applications() { CustomerNumber = "M1" }, new Applications() { CustomerNumber = "M2" } }; 
	public List<Applications> countries = new List<Applications>() { new Applications() { CountryID = "EU" }, new Applications() { CountryID = "UK" }, new Applications() { CountryID = "USA" },
		new Applications() { CountryID = "CHN" }, new Applications() { CountryID = "CND" } };
	public void UpdatePickerDataSource()
	{

		Musteriid.ItemsSource = null;
		Musteriid.ItemsSource = customersave;
	}





	private async void Kaydet(object sender, EventArgs e)
	{
		

		if (application == null  )
		{
			application = new Applications()
			{
				applicationnumber = Basvuruid.Text,
				CustomerNumber=Musteriid.ToString(),
				PersonalNumver=Personelnum.ToString(),
				CountryID=Ulkeid.ToString(),
				TotalPrice = ucret.Text,
				RejectionRate = GecmisRet.Text


			};
			



		}
		else
		{
			application.applicationnumber = Basvuruid.Text;
			application.customernumber= Musteriid.ToString();
			application.personalnumber=Personelnum.ToString();
			application.countryid = Ulkeid.ToString();
			application.totalprice = ucret.Text;
			application.rejectionrate = GecmisRet.Text;

		}
		if (basvuruislemi != null) 
		{
			basvuruislemi(application);
		}
		
		switch (application.CountryID) 
		{
			case ("UK"):
				application.TotalPrice = "150";
				ucret.Text = "150";
				break;
			case ("USA"):
				application.TotalPrice = "200";
				ucret.Text = "200";
				break;
			case ("EU"):
				application.TotalPrice = "80";
				ucret.Text = "80";
				break;
			case ("CHN"):
				application.TotalPrice = "50";
				ucret.Text = "60";
				break;
			case ("CND"):
				application.TotalPrice = "80";
				ucret.Text = "80";
				break;
			 default:
				application.TotalPrice =" 0";
				ucret.Text = "0";
				break;



		}
		
		await Navigation.PopAsync();

	}
	private void Iptal(object sender, EventArgs e)
	{
		 Navigation.PopAsync();
	}
}
	