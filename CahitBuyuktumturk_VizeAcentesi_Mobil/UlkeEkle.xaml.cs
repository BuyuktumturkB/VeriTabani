

namespace CahitBuyuktumturk_VizeAcentesi_Mobil;

public partial class UlkeEkle : ContentPage
{
	public UlkeEkle()
	{
		InitializeComponent();
	}
	public Countries country;
	public Action<Countries> CountryAction { get; internal set; }
	private async void Kaydet(object sender, EventArgs e)
	{
		if (country == null)
		{
			country = new Countries()
			{
				Countrycode = Ulkekodu.Text,
				Countryname = UlkeAdi.Text,
				VisaPrice = visa.Text,
				EmbassyPhone=KonsolosTel.Text,
				Embassyadress=KonsoloslukAdres.Text
			};
		}
		else 
		{
			country.countrcode = Ulkekodu.Text;
			country.countryname = UlkeAdi.Text;
			country.visaprice = visa.Text;
			country.embassyphone = KonsolosTel.Text;
			country.embassyadress= KonsoloslukAdres.Text;
		}
		if (CountryAction != null) 
		{
			CountryAction(country);
		}
		await Navigation.PopAsync();
	}
	private async void Iptal(object sender, EventArgs e) 
	{
		await Navigation.PopAsync();
	}
}
