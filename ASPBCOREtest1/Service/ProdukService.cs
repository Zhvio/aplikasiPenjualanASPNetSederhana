using ASPBCOREtest1.Models;
using Microsoft.AspNetCore.Http.Features;

namespace ASPBCOREtest1.Service
{
    public class ProdukService
    {

        public static void TambahKeranjang(Produk item)
        {
            

            foreach (var itemKeranjang in Produk.ListKeranjang)
            {
                if (itemKeranjang.Id == item.Id)
                {
                    itemKeranjang.JumlahKeranjang++;
                    return;
                }
            }


            foreach (var Items in Produk.ListProduk)
            {
                if (item.Id == Items.Id)
                {
                    item.JumlahKeranjang = 1;
                    Produk.ListKeranjang.Add(Items);
                    return;
                }
                
            }
            
        }

    }
}
