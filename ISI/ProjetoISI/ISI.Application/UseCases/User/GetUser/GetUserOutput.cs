namespace ISI.Application.UseCases.User.GetUser;

public class GetUserOutput
{
   public string Email { get; }
   public string Name { get; }
   
   
   private GetUserOutput(string email, string name)
   {
       Email = email;
       Name = name;
   }
   
   public static GetUserOutput FromUser(Domain.Entity.User user) => new GetUserOutput(user.Email.Value, user.GetFullName());
   
}   