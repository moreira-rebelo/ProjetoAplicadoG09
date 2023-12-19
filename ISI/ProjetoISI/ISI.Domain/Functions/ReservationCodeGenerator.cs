namespace ISI.Domain.Functions;


public static class ReservationCodeGenerator
{
    private static Random random = new Random();

    public static string GenerateReservationCode()
    {
        string vowels = "AEIOU";
        string numbers = "0123456789";

        return new string(Enumerable.Repeat(vowels, 4)
                   .Select(s => s[random.Next(s.Length)]).ToArray()) +
               new string(Enumerable.Repeat(numbers, 6)
                   .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string GenerateReservationPassword()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, 6)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}