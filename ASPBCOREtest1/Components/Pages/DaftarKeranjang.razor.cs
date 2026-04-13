using ASPBCOREtest1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ASPBCOREtest1.Models;

namespace ASPBCOREtest1.Components.Pages
{
    public partial class DaftarKeranjang
    {
        private List<Produk> ItemKeranjang = Produk.ListKeranjang;

        [Inject] NavigationManager Nav { get; set; }
        private void Removeproduk(Produk item)
        {
            if (item.JumlahKeranjang == 1)
            Produk.ListKeranjang.Remove(item);
            else
            {
                item.JumlahKeranjang--;
            }
        }

        private void KonfirmasiPembelian()
        {
            string name = "";

            foreach (var item in Produk.ListKeranjang)
            {
                name += $"{item.Nama}({item.JumlahKeranjang}) ";
            }

            var sejarahBaru = new Produk
            {
                Nama = name,
                Harga = Produk.TotalSemuaKeranjang,
                Date = DateOnly.FromDateTime(DateTime.Now),
                SejarahDesc = "Berhasil"
            };

            Produk.ListSejarah.Add(sejarahBaru);

            Produk.ListKeranjang.Clear();

            Nav.NavigateTo("/sejarah");
        }

    }
}
