
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
    }
    public void Add(string text)
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
            Console.Write(news);
            rowOffset++;
        }
    }
    public class Message
    {
        int Count;
        string Text;
        public Message(int count, string text)
        {
            Count = count;
            Text = text;
        }
        public override string ToString()
        {
            return $"{Count}. {Text}";
        }
    }
}

 