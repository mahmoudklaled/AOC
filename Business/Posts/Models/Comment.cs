using Business.Accounts.Models;

namespace Business.Posts.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string comment { get; set; }
        public Photo? Photo { get; set; }
        public Vedio? Vedio { get; set; }
        //Forign-key
        public ProfileAccounts ProfileAccount { get; set; }
        public string ProfileAccountId { get; set; }

        //Forign-key
        public Post? Post { get; set; }
        public Guid? PostId { get; set; }
        public QuestionPost? QuestionPost { get; set; }
        public Guid? QuestionPostId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public ICollection<Reacts> Reacts { get; set; }

    }
}
