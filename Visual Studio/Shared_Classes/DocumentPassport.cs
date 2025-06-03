namespace Shared_Classes
{
    public class DocumentPassport : Document
    {
        public int Id { get; private set; }
		public int UserId { get; private set; }
        public string? PassportNumber { get; private set; }
        public DateOnly? DateOfIssue { get; private set; }
        public DateOnly? DateOfExpire { get; private set; }


        // create
        public DocumentPassport(int userId, string? passportNumber, DateOnly? issueDate, DateOnly? expiryDate)
        {
            if (userId <= 0)
                throw new ArgumentException("User ID must be a positive integer.", nameof(userId));

            this.UserId = userId;
            this.PassportNumber = passportNumber;
            this.DateOfIssue = issueDate;
            this.DateOfExpire = expiryDate;
        }


        // get db
        public DocumentPassport(int id, int userId, string passportNumber, DateOnly issueDate, DateOnly expiryDate)
        {
            if (id < 0)
                throw new ArgumentException("ID must be a non-negative integer.", nameof(id));

            if (userId <= 0)
                throw new ArgumentException("User ID must be a positive integer.", nameof(userId));

            if (string.IsNullOrWhiteSpace(passportNumber))
                throw new ArgumentException("Passport Number must not be null or empty.", nameof(passportNumber));

            this.Id = id;
            this.UserId = userId;
            this.PassportNumber = passportNumber;
            this.DateOfIssue = issueDate;
            this.DateOfExpire = expiryDate;
        }


        public override string GetDetails()
        {
            return $"Passport Number: {PassportNumber}, " +
                   $"Date of issue: {DateOfIssue?.ToString("yyyy-MM-dd")}, " +
                   $"Date of expire: {DateOfExpire?.ToString("yyyy-MM-dd")}";
        }

    }
}
