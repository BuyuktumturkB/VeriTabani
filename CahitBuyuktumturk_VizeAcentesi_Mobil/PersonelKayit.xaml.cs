namespace CahitBuyuktumturk_VizeAcentesi_Mobil;

public partial class PersonelKayit : ContentPage
{
	public PersonelKayit()
	{
		InitializeComponent();
	}
	public Calisanlar KayýtPersonel;
	public Action<Calisanlar> EkleGorevli { get;internal set; }
	private void Kaydet(object sender , EventArgs eventArgs ) 
	{
		if (KayýtPersonel == null) 
		{
			KayýtPersonel = new Calisanlar()
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
			KayýtPersonel.personelid = PersonelNumara.Text;
			KayýtPersonel.Personelkimlik = PTCKN.Text;
			KayýtPersonel.Personelisim = PersonelAD.Text;
			KayýtPersonel.PersonelSoyisim= PersonelSoyad.Text;
			KayýtPersonel.PersonelTelefon = Personeltel.Text;

		}
		if (EkleGorevli != null)
		{
			EkleGorevli(KayýtPersonel);
		}
		Navigation.PopModalAsync();
		
	}
	public async void Iptal(object sender,EventArgs e) 
	{
		await Navigation.PopModalAsync();
	}
}