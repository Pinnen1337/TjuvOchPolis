
namespace Tjuv_Polis;

public class NewsFeed
{
    public Queue<string> NewsQueue { get; }
    private readonly int _maxCount;
    private readonly int _startDrawAtX;
    private readonly int _startDrawAtY;
    public NewsFeed(int positionX, int positionY, int maxCount)
    {
        NewsQueue = new Queue<string>();
        _startDrawAtX = positionX;
        _startDrawAtY = positionY;
        _maxCount = maxCount;
    }
    public void Add(string message)
    {
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
        foreach (var message in NewsQueue)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset);
                Console.Write(new string(' ', 100));
                Console.SetCursorPosition(_startDrawAtX, _startDrawAtY + rowOffset);
                Console.Write(message);
            }
            rowOffset++;
        };
    }
}

