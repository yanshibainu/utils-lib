namespace utils_lib.MqUtils
{
    public interface ISender<in T>
    {
        void Send(T t);
    }
}