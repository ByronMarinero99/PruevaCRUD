using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.EntidadesDeNegocio
{
    public class Rol
    {
        // Asignacion de las entidades con sus restricciones
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(75, ErrorMessage = "El largo máximo son 75 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripcion es requerida")]
        [MaxLength(75, ErrorMessage = "El largo máximo son 75 caracteres")]
        public string Descripcion { get; set; }

        [NotMapped]
        public int top_aux { get; set; } //Propiedad auxiliar

        public List<Usuario> Usuarios { get; set; } //Propiedad de navegción

    }
}
