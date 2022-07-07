using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Sistema_de_planos.Dominio.Entidades
{
    public partial class User
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public string Password { get; set; }
    }

}
