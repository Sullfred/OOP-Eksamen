using System;

namespace EksamensOpgave.Models
{
    //Inherts from class Product
    public class SeasonalProduct : Product
    {
        public DateTime SeasonStartDate { get; }
        public DateTime SeasonEndDate { get; }

        //Checks if Dates matches. If not the product is inactive
        public override bool Active
        {
            get => (DateTime.Now.Date >= SeasonStartDate.Date &&
                    DateTime.Now.Date <= SeasonEndDate.Date);
        }

        // **constructor**
        public SeasonalProduct(string name, int price, bool active, DateTime seasonstartdate, DateTime seasonendDate) :base(name, price, active)
        {
            SeasonStartDate = seasonstartdate;
            SeasonEndDate = seasonendDate;

        }
    }
}
