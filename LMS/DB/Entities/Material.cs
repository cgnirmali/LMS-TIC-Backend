namespace LMS.DB.Entities
{
    public class Material
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Week {  get; set; }
        public string Description { get; set; }
        public string UploadedBy { get; set; }
        public string URL { get; set; }

        public Guid SubjectId { get; set; }

        //navigation property
        public Subject Subject { get; set; }
    }
}
