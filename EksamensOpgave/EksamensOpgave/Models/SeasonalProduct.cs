using System;

namespace EksamensOpgave.Models
{
    public class SeasonalProduct : Product
    {
        public DateTime SeasonStartDate { get; }
        public DateTime SeasonEndDate { get; }

        public override bool Active { get => base.Active; set => base.Active = value; }

        public SeasonalProduct(string name, int price, bool active, DateTime seasonstartdate, DateTime seasonendDate) :base(name, price, active)
        {
            SeasonStartDate = seasonstartdate;
            SeasonEndDate = seasonendDate;

        }
    }
}
