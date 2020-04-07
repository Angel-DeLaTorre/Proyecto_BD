﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_BD.Models
{
    public class Alumno
    {
        public int idAlumno { get; set; }
        public long matricula { get; set; }
        public int idCarrera { get; set; }
        public int idGrupo { get; set; }
        public int idPersona { get; set; }
        public int idUsuario { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}