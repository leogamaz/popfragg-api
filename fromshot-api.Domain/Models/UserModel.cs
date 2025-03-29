using Supabase.Postgrest.Models;
//using System.ComponentModel.DataAnnotations.Schema;
using Supabase;
//using System.ComponentModel.DataAnnotations;
using System;
using Supabase.Postgrest.Attributes;

namespace fromshot_api.Domain.Models
{
    [Table("users")] 
    public class UserModel : BaseModel
    {
        
        [PrimaryKey("id")]
        public Guid Id { get; set; } // Correspondente ao tipo UUID no banco de dados

        [Column("username")]
        public string Username { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("steamid")]
        public string SteamId { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("criado_em")]
        public DateTime? CriadoEm { get; set; } // Nullable, pois o valor padrão é 'now()' no banco de dados

        [Column("atualizado_em")]
        public DateTime? AtualizadoEm { get; set; }

        // Definindo a chave estrangeira
        //ForeignKey("Id")]
        //public virtual AuthUser AuthUser { get; set; } // A relação com a tabela auth.users (caso seja relevante para a sua aplicação)
    }
}