
using MySql.Data.MySqlClient;
using System.Net.WebSockets;

Console.WriteLine("****************************");
Console.WriteLine("Sistem Simpan Kontak");
Console.WriteLine("****************************");

string connectionString = "Server=127.0.0.1;Port=3306;Database=tiktokcsharp;Uid=root;Pwd=;";
var mysql = new MySqlConnection(connectionString);

List<ContactModel> contacts = new List<ContactModel>();
//  0     1     2
// data1 data2 data3

while (true)
{
    Console.WriteLine();
    Console.Write("Aksi (tambah / padam / ubah / senarai / keluar): ");
    string aksi = Console.ReadLine();

    switch (aksi)
    {
        case "tambah":
            Console.WriteLine("+++ Tambah Kontak Baharu+++");

            Console.Write("Nama: ");
            string nama = Console.ReadLine();

            Console.Write("No Telefon: ");
            string fon = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            try
            {
                mysql.Open();
                string sql = "INSERT INTO contacts (nama, fon, email) VALUES(@name, @fon, @email)";

                var cmd = new MySqlCommand(sql, mysql);
                cmd.Parameters.AddWithValue("@name", nama);
                cmd.Parameters.AddWithValue("@fon", fon);
                cmd.Parameters.AddWithValue("@email", email);

                cmd.ExecuteNonQuery();
                mysql.Close();

                Console.WriteLine("Maklumat kontak telah disimpan!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Data cannot saved.");
            }
            
            Console.WriteLine();
            break;

        case "padam":
            Console.WriteLine();
            Console.WriteLine("===== Sila pilih ID kontak untuk dipadam ========");

            try
            {
                mysql.Open();
                string sql_senarai = "SELECT * FROM contacts";
                var cmd_senarai = new MySqlCommand(sql_senarai, mysql);
                var read_senarai = cmd_senarai.ExecuteReader();

                while (read_senarai.Read())
                {
                    var read_id = read_senarai["id"].ToString();
                    var read_nama = read_senarai["nama"].ToString();
                    var read_fon = read_senarai["fon"].ToString();
                    var read_email = read_senarai["email"].ToString();

                    Console.WriteLine(
                        read_id + ". " +
                        read_nama + " " +
                        read_fon + " " +
                        read_email
                    );
                }
                mysql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Ada error masa ambil data.");
            }

            Console.Write("ID Kontak untuk dipadam: ");
            string idx = Console.ReadLine();

            mysql.Open();
            string sql_padam = "DELETE FROM contacts WHERE id=@id";
            var cmd_padam = new MySqlCommand(sql_padam, mysql);
            cmd_padam.Parameters.AddWithValue("@id", idx);
            cmd_padam.ExecuteNonQuery();

            mysql.Close();

            Console.WriteLine("Maklumat telah dipadam.");

            break;

        case "ubah":
            Console.WriteLine();
            Console.WriteLine("===== Sila pilih ID kontak diubah ========");

            try
            {
                mysql.Open();
                string sql_senarai = "SELECT * FROM contacts";
                var cmd_senarai = new MySqlCommand(sql_senarai, mysql);
                var read_senarai = cmd_senarai.ExecuteReader();

                while (read_senarai.Read())
                {
                    var read_id = read_senarai["id"].ToString();
                    var read_nama = read_senarai["nama"].ToString();
                    var read_fon = read_senarai["fon"].ToString();
                    var read_email = read_senarai["email"].ToString();

                    Console.WriteLine(
                        read_id + ". " +
                        read_nama + " " +
                        read_fon + " " +
                        read_email
                    );
                }
                mysql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Ada error masa ambil data.");
            }

            Console.Write("ID Kontak: ");
            string id = Console.ReadLine();

            Console.WriteLine("+++++ Sila masukkan maklumat terbaharu +++++");
            Console.Write("Nama (baru): ");
            string nama_ubah = Console.ReadLine();

            Console.Write("Fon (baru): ");
            string fon_ubah = Console.ReadLine();

            Console.Write("Email (baru): ");
            string email_ubah = Console.ReadLine();

            mysql.Open();
            string sql_ubah = "UPDATE contacts SET nama=@name, fon=@fon, email=@email WHERE id=@id";
            var cmd_ubah = new MySqlCommand(sql_ubah, mysql);
            cmd_ubah.Parameters.AddWithValue("@name", nama_ubah);
            cmd_ubah.Parameters.AddWithValue("@fon", fon_ubah);
            cmd_ubah.Parameters.AddWithValue("@email", email_ubah);
            cmd_ubah.Parameters.AddWithValue("@id", id);

            cmd_ubah.ExecuteNonQuery();
            mysql.Close();


            Console.WriteLine("Maklumat kontak telah berjaya diubah!");
            Console.WriteLine();
            break;

        case "senarai":
            Console.WriteLine();
            Console.WriteLine("===== Senarai Terbaru Kontak ========");

            try
            {
                mysql.Open();
                string sql_senarai = "SELECT * FROM contacts";
                var cmd_senarai = new MySqlCommand(sql_senarai, mysql);
                var read_senarai = cmd_senarai.ExecuteReader();

                while (read_senarai.Read())
                {
                    var read_id = read_senarai["id"].ToString();
                    var read_nama = read_senarai["nama"].ToString();
                    var read_fon = read_senarai["fon"].ToString();
                    var read_email = read_senarai["email"].ToString();

                    Console.WriteLine(
                        read_id + ". " +
                        read_nama + " " +
                        read_fon + " " +
                        read_email
                    );
                }
                mysql.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! Ada error masa ambil data.");
            }
            break;

        case "keluar":
            return;
            break;

        default:
            Console.WriteLine("Aksi yang dipilih tidak dikenalpasti!");
            Console.WriteLine();
            break;
    }
}

class ContactModel
{
    public string nama { get; set; }
    public string fon { get; set; }
    public string email { get; set; }
}