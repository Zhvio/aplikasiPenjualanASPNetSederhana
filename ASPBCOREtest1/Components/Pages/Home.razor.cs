using ASPBCOREtest1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics;
using ASPBCOREtest1.Service;
using Microsoft.AspNetCore.Http.Features;


namespace ASPBCOREtest1.Components.Pages
{
    public partial class Home
    {

        [Inject] private ProdukService service { get; set; }

        private List<Produk> ListProduk = new List<Produk>();
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            ListProduk = await service.GetProduk();

            if (ListProduk.Count ==  0)
            {
                foreach (var item in Produk.ListProduk)
                {

                    var itemsz = new Produk
                    {
                        Nama = item.Nama,
                        Harga = item.Harga,
                        DeskripsiSingkat = item.DeskripsiSingkat,
                        GambarUrl = item.GambarUrl
                    };


                    await service.AddProduk(item);
                }
            }

        }
        private async Task BeliProduk(Produk produk)
        {

            await service.SimpanKeKeranjang(produk);
            await JSRuntime.InvokeVoidAsync("alert", $"Produk '{produk.Nama}' ditambahkan ke keranjang.");
        }

    }
}
