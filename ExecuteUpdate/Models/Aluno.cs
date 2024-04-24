using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExecuteUpdate.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int Idade { get; set; } = 0;
        public bool Ativo { get; set; }
        public DateTime DataMatricula { get; set; }
    }
}