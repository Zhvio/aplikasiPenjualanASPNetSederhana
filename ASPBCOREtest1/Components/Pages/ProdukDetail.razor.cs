using ASPBCOREtest1.Models;
using ASPBCOREtest1.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ASPBCOREtest1.Components.Pages
{
    public partial class ProdukDetail
    {
        [Parameter] public int Id { get; set; }

        private Produk produk;

        protected override void OnInitialized()
        {
            produk = GetProductFromDatabase(Id);
        }

        private void GoBack()
        {
            Console.WriteLine("Tombol GoBack diklik!");
            if (Nav != null)
            {
                Nav.NavigateTo("/");
            }
            else
            {
                Console.WriteLine("Nav Manager masih NULL!");
            }
        }

        [Inject] protected NavigationManager Nav { get; set; }

        private Produk GetProductFromDatabase(int id)
        {
            foreach (var item in Produk.ListProduk)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        private async void TambahKeranjang(Produk item)
        {
            await JSRuntime.InvokeVoidAsync("alert", $"Selamat! {item.Nama} Berhasil Masuk Keranjang.");
            ProdukService.TambahKeranjang(item);

        }
    }
}
