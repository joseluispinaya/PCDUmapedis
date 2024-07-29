using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCDUmapedis.Shared.Models
{
    public class UserBasicInfo
    {
        public int IdUsuaLog { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Foto { get; set; }
        public string? RolTex { get; set; }
        public int RolId { get; set; }

        public string PictureFullPath => string.IsNullOrEmpty(Foto)
            ? "https://umapedis-001-site1.ftempurl.com/Imagenes/Sinfotop.jpg"
            : $"https://umapedis-001-site1.ftempurl.com{Foto}";
    }
}
