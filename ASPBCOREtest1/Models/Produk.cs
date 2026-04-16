namespace ASPBCOREtest1.Models
{
    public class Produk
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string DeskripsiSingkat { get; set; }
        public decimal Harga { get; set; }
        public string GambarUrl { get; set; }
        public int JumlahKeranjang { get; set; } = 1;
        public string HargaDisplay => string.Format("Rp {0:N0}", Harga);


        public decimal SubTotal => Harga * JumlahKeranjang;

        public string SubTotalDisplay => string.Format("Rp {0:N0}", SubTotal);


        public decimal TotalSemuaKeranjang => ListKeranjang.Sum(p => p.SubTotal);
        public string TotalSemuaDisplay => string.Format("Rp {0:N0}", TotalSemuaKeranjang);
  
        public DateOnly Date { get; set; }

        public string SejarahDesc { get; set; }

        public static List<Produk> ListProduk = new List<Produk>
        {
            new Produk { Id = 1, Nama = "Martabak Manis", DeskripsiSingkat = "Martabak adalah kudapan populer di Indonesia, berasal dari bahasa Arab muttabaq yang berarti 'terlipat'.", Harga = 27000, GambarUrl="https://www.chocolatesandchai.com/wp-content/uploads/2024/10/Martabak-Manis-Featured.jpg" },
            new Produk { Id = 2, Nama = "Sate Padang", DeskripsiSingkat = "Sate Padang adalah kuliner khas Sumatera Barat berbahan dasar daging sapi, lidah, atau jeroan (jantung, usus, tetelan) yang ditusuk dan dibakar.", Harga = 16000,GambarUrl="https://upload.wikimedia.org/wikipedia/commons/4/41/Sate_Padang2.JPG" },
            new Produk { Id = 3, Nama = "Nasi Padang", DeskripsiSingkat = "Nasi Padang adalah hidangan khas Minangkabau, Sumatera Barat, yang terdiri dari nasi putih disajikan dengan berbagai lauk pauk kaya rempah dan bersantan.", Harga = 12000,GambarUrl="https://thumbs.dreamstime.com/b/nasi-padang-indonesian-food-west-sumatera-nasi-padang-typical-minangkabau-dish-form-steamed-rice-417610125.jpg" },
            new Produk { Id = 4, Nama = "Es Teh", DeskripsiSingkat = "Es teh adalah minuman hasil seduhan teh (hitam, hijau, atau oolong) yang didinginkan dan disajikan dengan es batu. ", Harga = 5000, GambarUrl="https://asset.kompas.com/crops/-EW4dZIFD3U055K4qtHqSgUg_hM=/92x67:892x600/1200x800/data/photo/2023/08/23/64e59deb79bfb.jpg" },
            new Produk { Id = 5, Nama = "Bakso", DeskripsiSingkat = "Bakso adalah makanan olahan berbentuk bulat yang terbuat dari campuran daging giling (sapi, ayam, ikan, atau udang), tepung tapioka/kanji, dan bumbu-bumbu.", Harga = 15000, GambarUrl="https://asset.tribunnews.com/PdNdZ8WFnhSh8g0l5t0cPLGF478=/1200x675/filters:upscale():quality(30):format(webp):focal(0.5x0.5:0.5x0.5)/tribunnews/foto/bank/originals/bakso-sapi-kuah-1.jpg" },
            new Produk { Id = 6, Nama = "Mie Ayam", DeskripsiSingkat = "Mie ayam adalah hidangan populer Indonesia berupa mi kuning rebus berbumbu yang disajikan dengan potongan ayam semur, sayuran (sawi), dan kuah kaldu.", Harga = 10000, GambarUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTl3R-Af5enTQszlFp3R_nEHTm-S4Onhk3HpQ&s"  },
        };

        public static List<Produk> ListKeranjang = new List<Produk>
        {

        };

        public static List<Produk> ListSejarah = new List<Produk>
        {

        };

    }
}
