using LMS.Assets.Enums;

namespace LMS.DB.Entities.Email
{
    public class EmailTemplate
    {
        public Guid Id { get; set; }
        public EmailType EmailType { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
