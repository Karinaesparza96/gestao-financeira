using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }

        [Key]
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
