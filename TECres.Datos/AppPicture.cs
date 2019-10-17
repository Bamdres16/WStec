using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECres.Datos
{
    public class AppPicture
    {
        private string path = @"D:\Documentos\Api\TECres\Propiedades\";
        public string guardar(Picture picture, int count)
        {
            var bytes = Convert.FromBase64String(picture.Base64);
            string rute = path + count.ToString() + "_" + picture.Id_propiedad.ToString() + ".jpg";
            using (var imageFile = new FileStream(rute, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }
            return count.ToString() + "_" + picture.Id_propiedad.ToString() + ".jpg";
        }
        public string guardar(Picture picture)
        {
            var bytes = Convert.FromBase64String(picture.Base64);
            string rute = path + picture.Id_propiedad.ToString() + ".jpg";
            using (var imageFile = new FileStream(rute, FileMode.Create))
            {
                imageFile.Write(bytes, 0, bytes.Length);
                imageFile.Flush();
            }
            return picture.Id_propiedad.ToString() + ".jpg";
        }
        public string convertir(string rute)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(@rute);
            string base64String = "data:image/jpeg;base64,"+Convert.ToBase64String(imageBytes);

            return base64String;
        }



    }
}
