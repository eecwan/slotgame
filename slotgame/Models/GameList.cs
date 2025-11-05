using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // 👈 加這行！

namespace slotgame.Models
{
    [Table("GameList")]
    public class GameList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("GameID")] // 這裡也要配合資料庫大小寫
        public int GameID { get; set; }

        [Column("GameNameTW")]
        public string GameNameTW { get; set; } = string.Empty;

        [Column("GameNameEN")]
        public string GameNameEN { get; set; } = string.Empty;

        [Column("GameImageUrl")]
        public string? GameImageUrl { get; set; }

        [Column("GameCode")]
        public string GameCode { get; set; } = string.Empty;

        [Column("ReleaseDate")]
        public DateOnly? ReleaseDate { get; set; }

        [Column("GameCategory")]
        public string? GameCategory { get; set; }

        [Column("IsPromoted")]
        public bool IsPromoted { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("GameLink")]
        public string? GameLink { get; set; }
    }
}