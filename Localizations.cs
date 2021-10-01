using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Localizatio.InMemory
{
    [Table("Localizations")]
    public class Localizations
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [StringLength(10)]
        [Column("Localization")]
        public string Language { get; set; }

        [StringLength(500)]
        [Column("Label")]
        public string Key { get; set; }

        [StringLength(500)]
        [Column("LabelValue")]
        public string Value { get; set; }
    }
}
