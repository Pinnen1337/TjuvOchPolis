
namespace Tjuv_Polis;
public class NewsFeed
{
    public Queue<Message> NewsQueue { get; }
    private readonly int _maxCount;
    private readonly int _startDrawAtX;
    private readonly int _startDrawAtY;
    private int messageCounter = 1;

    public NewsFeed(int positionX, int positionY, int maxCount)
    {
        NewsQueue = new Queue<Message>();
        _startDrawAtX = positionX;
        _startDrawAtY = positionY;
        _maxCount = maxCount;

        Console.SetCursorPosition(_startDrawAtX, _startDrawAtY - 1);
        Console.Write($"{"NEWS FEED",10}");
    }
    public void AddMessageAndWriteQueue(string text, ConsoleColor color)
    {
        Message message = new Message(messageCounter, text, color);
        messageCounter++;
        NewsQueue.Enqueue(message);
        if (NewsQueue.Count > _maxCount)
        {
            NewsQueue.Dequeue();
        }
        Write();
    }
    public void AddMessageAndWriteQueue(string text)
    {
        Message message = new Message(messageCounter, text);
        messageCounter++;
        NewsQueue.Enqueue(message);
        if (NewsQueue.Count > _maxCount)
        {
            NewsQueue.Dequeue();
        }
        Write();
    }

    private void Write()
    {

        int rowOffset = 0;
        foreach (var news in NewsQueue.Reverse())
        {
            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset);
            Console.Write(new string(' ', 100));
            Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset);
            Console.ForegroundColor = news.Color;
            Console.Write(news);

            rowOffset++;
        }
    }
    public class Message
    {
        int Count;
        string Text;
        public ConsoleColor Color { get; set; }

        public Message(int count, string text)
        {
            Count = count;
            Text = text;
            Color = ConsoleColor.White;
        }
        public Message(int count, string text, ConsoleColor color) : this(count, text)
        {
            Color = color;
        }
        public override string ToString()
        {
            return $"{Count}. {Text}";
        }
    }
}

 