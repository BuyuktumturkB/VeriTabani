namespace CahitBuyuktumturk_VizeAcentesi_Mobil;

public partial class PersonelKayit : ContentPage
{
	public PersonelKayit()
	{
		InitializeComponent();
	}
	public Calisanlar Kay�tPersonel;
	public Action<Calisanlar> EkleGorevli { get;internal set; }
	private void Kaydet(object sender , EventArgs eventArgs ) 
	{
		if (Kay�tPersonel == null) 
		{
			Kay�tPersonel = new Calisanlar()
			{
				Personelid = PersonelNumara.Text,
				Personelkimlik = PTCKN.Text,
				Personelisim = PersonelAD.Text,
				PersonelSoyisim = PersonelSoyad.Text,
				PersonelTelefon = Personeltel.Text

			};
		}
		else 
		{
			Kay�tPersonel.personelid = PersonelNumara.Text;
			Kay�tPersonel.Personelkimlik = PTCKN.Text;
			Kay�tPersonel.Personelisim = PersonelAD.Text;
			Kay�tPersonel.PersonelSoyisim= PersonelSoyad.Text;
			Kay�tPersonel.PersonelTelefon = Personeltel.Text;

		}
		if (EkleGorevli != null)
		{
			EkleGorevli(Kay�tPersonel);
		}
		Navigation.PopModalAsync();
		
	}
	public async void Iptal(object sender,EventArgs e) 
	{
		await Navigation.PopModalAsync();
	}
}