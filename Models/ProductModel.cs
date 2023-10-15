namespace MepasTask.Models
{
    public class ProductModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string category_id { get; set; }
        public string price { get; set; }
        public string unit { get; set; }
        public string stock { get; set; }
        public string color { get; set; }
        public string weight { get; set; }
        public string width { get; set; }
        public string height { get; set; } 
        public string added_user_id { get; set; }
        public string updated_user_id { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
    }
}
