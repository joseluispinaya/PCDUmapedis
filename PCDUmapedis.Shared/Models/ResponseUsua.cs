﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCDUmapedis.Shared.Models
{
    public class ResponseUsua
    {
        public int IdUsuario { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Foto { get; set; }
        public string? Profecion { get; set; }
        public int IdRol { get; set; }
        public bool Estado { get; set; }
    }
}
