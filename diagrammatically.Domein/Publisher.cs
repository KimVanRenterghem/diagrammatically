namespace diagrammatically.Domein
{
    public interface Publisher<out TMessage>
    {
        void Subscribe(Subscriber<TMessage> subscriber);
    }
}