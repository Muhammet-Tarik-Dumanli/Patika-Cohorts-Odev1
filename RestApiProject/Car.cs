using System.ComponentModel.DataAnnotations.Schema;

namespace RestApiProject
{
    public class Car
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public bool IsAutomatic { get; set; }
    }

}