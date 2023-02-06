namespace PCOHRApp.Models
{
    public class HouseBillGenerateVM
    {
        public string companyName { get; set; }
        public string companyAddress { get; set; }
        public string renterAddress { get; set; }
        public int? renterHouseId { get; set; }
        public string isClosedString { get; set; }
        public int billDetailId { get; set; }
        public string renterName { get; set; }
        public string renterPhone { get; set; }
        public bool isBatch { get; set; }
        public string monthName { get; set; }
        public int month { get; set; }
        public string yearName { get; set; }
        public decimal billAmount { get; set; }
        public string houseName { get; set; }
        public string houseType { get; set; }
        public int year { get; set; }
        public string remarks { get; set; }
        public int createdBy { get; set; }


    }
}