using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CRUD.EntidadesDeNegocio
{
    public class Usuario
    {

        // Declaracion de entidades y sus restricciones

            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "El nombre del producto es requerido")]
            [MaxLength(75, ErrorMessage = "El largo máximo son 75 caracteres")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El correo electronico es requerido")]
            [MaxLength(75, ErrorMessage = "El largo máximo son 75 caracteres")]
            public string Correo { get; set; }

            [Required(ErrorMessage = "la contrasena es requerida")]
            [MaxLength(75, ErrorMessage = "El largo máximo son 75 caracteres")]
            public string Password { get; set; }

            [Required(ErrorMessage = "El ID del rol es requerido")]
            [ForeignKey("Rol")]
            [Display(Name = "Roles")]
            public int IdRoles { get; set; }
            public Rol Roles { get; set; }

            [NotMapped]
            public int top_aux { get; set; } //propiedad auxiliar que sirve para especificar
                                             //el numero de registros que queremos consultar.

    }
}
