using NPoco;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Perplex._2FA.Models.Database
{
    [TableName("TwoFactor")]
    [PrimaryKey("Id", AutoIncrement = true)]
    public class TwoFactorEntity
    {
        [Column("Id")]
        [PrimaryKeyColumn(AutoIncrement = true)]
        public int Id { get; set; }

        [Column("UserId")]
        public int UserId { get; set; }

        [Column("Provider")]
        public string Provider { get; set; }

        [Column("Value")]
        public string Value { get; set; }
    }
}
