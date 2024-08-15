namespace orm_proj.Helpers
{
    public static class TextColor
    {
        public static void WriteLine(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        } 
    }
}
