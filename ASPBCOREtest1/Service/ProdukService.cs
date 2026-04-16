using ASPBCOREtest1.Components.Pages;
using ASPBCOREtest1.Models;
using Microsoft.AspNetCore.Http.Features;
using MySqlConnector;
using System.Data;
using System.Security.Cryptography;


namespace ASPBCOREtest1.Service
{
    public class ProdukService
    {
        string connString = "server=localhost;database=aspnettes;user=root;password=;";

        public async Task Checkout(Produk item)
        {
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                string sqlSejarah = "INSERT INTO sejarah (nama, harga) VALUES (@nama, @harga)";
                using var cmd1 = new MySqlCommand(sqlSejarah, connection, transaction);
                cmd1.Parameters.AddWithValue("@nama", item.Nama);
                cmd1.Parameters.AddWithValue("@harga", item.Harga);

                await cmd1.ExecuteNonQueryAsync();

                string sqlHapus = "DELETE FROM keranjang";
                using var cmd2 = new MySqlCommand(sqlHapus, connection, transaction);
                await cmd2.ExecuteNonQueryAsync();

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
         }

        public async Task<Produk> CekProdukDiKeranjang(string namaProduk)
        {
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();


            string query = "SELECT id, nama, harga, jumlah, (harga * jumlah) AS total_harga FROM keranjang WHERE nama = @nama";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nama", namaProduk);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Produk
                {
                    Id = reader.GetInt32("id"),
                    Nama = reader.GetString("nama"),
                    Harga = reader.GetDecimal("harga"),
                    JumlahKeranjang = reader.GetInt32("jumlah"),
                    GambarUrl = reader.GetString("img_url"),

                };
            }
            return null;
        }

        public async Task<List<Produk>> GetKeranjang()
        {

            var tempListKeranjang = new List<Produk>();

            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();

            string query = "SELECT id, nama, harga, jumlah, img_url FROM keranjang";

            using var cmd = new MySqlCommand(query,connection);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tempListKeranjang.Add(new Produk
                {
                    Id = reader.GetInt32("id"),
                    Nama = reader.GetString("nama"),
                    Harga = reader.GetDecimal("harga"),
                    JumlahKeranjang = reader.GetInt32("jumlah"),
                    GambarUrl = reader.GetString("img_url"),
                });
            }

            return tempListKeranjang;

        }

        public async Task<List<Produk>> GetProduk()
        {

            var tempListProduk = new List<Produk>();

            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();

            string query = "SELECT id, nama, deskripsi, harga, img_url FROM produk";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tempListProduk.Add(new Produk
                {
                    Id = reader.GetInt32("id"),
                    Nama = reader.GetString("nama"),
                    Harga = reader.GetDecimal("harga"),
                    DeskripsiSingkat = reader.GetString("deskripsi"),
                    GambarUrl = reader.GetString("img_url"),
                });
            }

            return tempListProduk;

        }

        public async Task<List<Produk>> GetSejarah()
        {

            var tempListSejarah = new List<Produk>();

            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();

            string query = "SELECT id, nama, harga, tanggal, status FROM sejarah";

            using var cmd = new MySqlCommand(query, connection);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                tempListSejarah.Add(new Produk
                {
                    Id = reader.GetInt32("id"),
                    Nama = reader.GetString("nama"),
                    Harga = reader.GetDecimal("harga"),
                    Date = reader.GetDateOnly("tanggal"),
                    SejarahDesc = reader.GetString("status")
                });
            }

            return tempListSejarah;

        }
        public async Task SimpanKeKeranjang(Produk item)
        {
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();

            string cekQuery = "SELECT jumlah FROM keranjang WHERE nama = @nama";
            using var cekCmd = new MySqlCommand(cekQuery, connection);
            cekCmd.Parameters.AddWithValue("@nama", item.Nama);

            var hasil = await cekCmd.ExecuteScalarAsync();

            if (hasil != null)
            {
                string updateQuery = "UPDATE keranjang SET jumlah = jumlah + @tambah WHERE nama = @nama";
                using var upCmd = new MySqlCommand(updateQuery, connection);
                upCmd.Parameters.AddWithValue("@tambah", item.JumlahKeranjang);
                upCmd.Parameters.AddWithValue("@nama", item.Nama);
                await upCmd.ExecuteNonQueryAsync();
            }
            else
            {
                string insertQuery = "INSERT INTO keranjang (nama, harga, jumlah, img_url) VALUES (@nama, @harga, @jumlah, @img_url)";
                using var inCmd = new MySqlCommand(insertQuery, connection);
                inCmd.Parameters.AddWithValue("@nama", item.Nama);
                inCmd.Parameters.AddWithValue("@harga", item.Harga);
                inCmd.Parameters.AddWithValue("@jumlah", item.JumlahKeranjang);
                inCmd.Parameters.AddWithValue("@img_url", item.GambarUrl);
                await inCmd.ExecuteNonQueryAsync();
            }

        }

        public async Task TambahKeranjang(Produk item)
        {
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();

            string query = "INSERT INTO keranjang (nama, harga, jumlah, img_url) VALUES (@nama, @harga, @jumlah, @img_url)";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nama", item.Nama);
            cmd.Parameters.AddWithValue("@harga", item.Harga);
            cmd.Parameters.AddWithValue("@jumlah", item.JumlahKeranjang);
            cmd.Parameters.AddWithValue("@img_url", item.GambarUrl);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task TambahJumlahKeranjang(string nama, int jumlah)
        {
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();

            string query = "UPDATE keranjang SET jumlah = jumlah + @tambah WHERE nama = @nama";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@tambah", jumlah);
            cmd.Parameters.AddWithValue("@nama", nama);


            await cmd.ExecuteNonQueryAsync();

        }

        public async Task RemoveItemKeranjang(Produk item)
        {
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();

            if (item.JumlahKeranjang <= 1)
            {
                string query = "DELETE FROM keranjang WHERE id = @id";
                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);

                await cmd.ExecuteNonQueryAsync();
            }
            else
            {
                string query = "UPDATE keranjang SET jumlah = jumlah - 1 WHERE id = @id";
                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", item.Id);

                await cmd.ExecuteNonQueryAsync();
            }

        }


        public async Task AddProduk(Produk item)
        {
            using var connection = new MySqlConnection(connString);
            await connection.OpenAsync();

            string query = "INSERT INTO produk (nama, deskripsi, harga, img_url) VALUES (@nama, @deskripsi, @harga, @img_url)";

            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nama", item.Nama);
            cmd.Parameters.AddWithValue("@deskripsi", item.DeskripsiSingkat);
            cmd.Parameters.AddWithValue("@harga", item.Harga);
            cmd.Parameters.AddWithValue("@img_url", item.GambarUrl);

            await cmd.ExecuteNonQueryAsync();
        }







    }
}
