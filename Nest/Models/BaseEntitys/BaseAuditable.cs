namespace Nest.Models.BaseEntitys
{
    public class BaseAuditable:BaseEntity
    {
        public int CreateBy { get; set; }
        public DateTime Created { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
        public string IPAddress { get; set; }
    }
}
