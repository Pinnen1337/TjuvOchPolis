
namespace Tjuv_Polis;

public class NewsFeed
{
    public Queue<string> NewsQueue { get; }
    private const int _maxCount = 5;
    public NewsFeed()
    {
        NewsQueue = new Queue<string>();
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
        foreach (var message in NewsQueue)
        {
            Console.WriteLine(message);
        };
    }
}

