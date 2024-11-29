using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Sub.Data.Models
{
    [Collection("Tarefas")]
    public class Tarefa
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        [BsonRepresentation(BsonType.String)]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [BsonElement("nome")]
        [Required(ErrorMessage = "O nome da tarefa é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [BsonElement("categoria")]
        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [StringLength(50, ErrorMessage = "A categoria deve ter no máximo 50 caracteres.")]
        public string Categoria { get; set; }

        [BsonElement("dificuldade")]
        [Required(ErrorMessage = "A dificuldade é obrigatória.")]
        [StringLength(20, ErrorMessage = "A dificuldade deve ter no máximo 20 caracteres.")]
        public string Dificuldade { get; set; }

        [BsonElement("dia")]
        [Required(ErrorMessage = "A data é obrigatória.")]
        public DateTime Dia { get; set; }
    }
}
