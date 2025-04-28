namespace FreeSweet.Models
{
    public class UserOrder 
    {
        public User user { get; set; }
        public List<Order> Order { get; set; }

        //public SecialOrder SecialOrder { get; set; }
       
    }
}
