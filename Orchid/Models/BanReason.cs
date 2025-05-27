using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.Models
{
    public class BanReason
    {
        public int Id { get; set; }

        public int? UserId { get; set; }
        public string Reason { get; set; } = null!;

        //public BanReason(int id, int userId, string reason)
        //{
        //    Id = id;
        //    UserId = userId;
        //    Reason = reason;
        //}
    }
}
