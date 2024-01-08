namespace CahitBuyuktumturk_VizeAcentesi_Mobil;

public partial class MusteriKayit : ContentPage
{
	public MusteriKayit()
	{
		InitializeComponent();
	}
	public Customer Customers;
	public Action<Customer> CustomersAction { get; internal set; }
	public DateTime passportcontroll=new DateTime(2024,04,09);
	
	private async void Kaydet(object sender,EventArgs e) 
	{
		if (Customers == null) 
		{
			Customers = new Customer() 
			{ Customerid = Musteriid.Text, CustomerTCKN=MusteriTCKN.Text,CustomerPassportNumber=MPN.Text,
				CustomerName=Musterisim.Text, CustomerSurname=MusteriSoyisim.Text
			, CustomerTelephone=Telefon.Text, PassportValidate=SonTarih.Date.ToShortDateString()};
		}
		else 
		{
			Customers.Customerid = Musteriid.Text;
			Customers.CustomerTCKN = MusteriTCKN.Text;
			Customers.CustomerPassportNumber = MPN.Text;
			Customers.CustomerName = Musterisim.Text;
			Customers.CustomerSurname = MusteriSoyisim.Text;
			Customers.CustomerTelephone = Telefon.Text;	
			Customers.PassportValidate=SonTarih.Date.ToShortDateString();
		}
		if (CustomersAction != null) 
		{
			CustomersAction(Customers);
		}
		/* if (SonTarih.Date <= passportcontroll) 
		{
			await DisplayAlert("Pasaport Son Tarihi 3 ay olamaz","Tamam","");
		}
		*/
		await Navigation.PopAsync();
	}
	public  void Cancel(object sender, EventArgs e) 
	{
		Navigation.PopAsync();
	}
}