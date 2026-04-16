using ASPBCOREtest1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ASPBCOREtest1.Models;
using ASPBCOREtest1.Service;

namespace ASPBCOREtest1.Components.Pages
{
    public partial class DaftarKeranjang
    {

        [Inject] private ProdukService service { get; set; }

        private List<Produk> ItemKeranjang = new();
        private decimal totalBayar = 0;
        [Inject] NavigationManager Nav { get; set; }
        private async Task Removeproduk(Produk item)
        {
            await service.RemoveItemKeranjang(item);
            
            await MuatKeranjang();
        }

        private async Task KonfirmasiPembelian()
        {

            string name = string.Join(", ", ItemKeranjang.Select(item => $"{item.Nama} ({item.JumlahKeranjang})"));
            totalBayar = ItemKeranjang.Sum(p => p.SubTotal);
            var sejarahBaru = new Produk
            {
                Nama = name,
                Harga = totalBayar,
                Date = DateOnly.FromDateTime(DateTime.Now),
                SejarahDesc = "Berhasil"
            };

            await service.Checkout(sejarahBaru);
            await MuatKeranjang();
        }

        

        protected override async Task OnInitializedAsync()
        {
            await MuatKeranjang();
        }

        private async Task MuatKeranjang()
        {
            ItemKeranjang = await service.GetKeranjang();
            totalBayar = ItemKeranjang.Sum(p => p.SubTotal);
        }


    }
}
