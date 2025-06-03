namespace Shared_Classes
{
    public class DocumentID : Document
    {
		public int Id { get; private set; }
        public int UserId { get; private set; }
		public string? IDNumber { get; private set; }
        public DateOnly? DateOfIssue { get; private set; }
        public DateOnly? DateOfExpire { get; private set; }

        // create
        public DocumentID(int userId, string? idNumber, DateOnly? issueDate, DateOnly? expiryDate)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be a positive integer.", nameof(userId));

            this.UserId = userId;
            this.IDNumber = idNumber;
            this.DateOfIssue = issueDate;
            this.DateOfExpire = expiryDate;
        }



        // get db
        public DocumentID(int id, int userId, string idNumber, DateOnly issueDate, DateOnly expiryDate)
        {
            if (id < 0)
                throw new ArgumentException("ID must be a non-negative integer.", nameof(id));

            if (userId <= 0)
                throw new ArgumentException("User ID must be a positive integer.", nameof(userId));

            if (string.IsNullOrWhiteSpace(idNumber))
                throw new ArgumentException("ID Number must not be null or empty.", nameof(idNumber));

            this.Id = id;
            this.UserId = userId;
            this.IDNumber = idNumber;
            this.DateOfIssue = issueDate;
            this.DateOfExpire = expiryDate;
        }


        public override string GetDetails()
        {
            return $"ID Number:{IDNumber}, " +
                   $"Date of issue: {DateOfIssue?.ToString("yyyy-MM-dd")}, " +
                   $"Date of expire: {DateOfExpire?.ToString("yyyy-MM-dd")}";

        }
    }
}
