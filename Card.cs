public class Card
{
    public int Id { get; set; }
    public string Color { get; set; }
    public bool IsFlipped { get; set; }
    public bool IsMatched { get; set; }

    private static readonly string[] availableColors = { "Red", "Orange", "Blue", "Green", "Yellow", "Purple", "White", "Black" };

    public Card(int id, int maxColors)
    {
        int colorChoiceSize = maxColors < availableColors.Length ? maxColors : availableColors.Length;

        Id = id;
        Color = availableColors[id % colorChoiceSize];
        IsFlipped = false;
        IsMatched = false;
    }
}
