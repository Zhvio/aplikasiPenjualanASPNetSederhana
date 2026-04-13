using ASPBCOREtest1.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics;
using ASPBCOREtest1.Service;


namespace ASPBCOREtest1.Components.Pages
{
    public partial class Home
    {

        private List<Produk> ListProduk = Produk.ListProduk;
        [Inject] protected IJSRuntime JSRuntime { get; set; }

        private async Task BeliProduk(Produk produk)
        {
            ProdukService.TambahKeranjang(produk);
            await JSRuntime.InvokeVoidAsync("alert", $"Produk '{produk.Nama}' ditambahkan ke keranjang.");
        }

    }
}
