namespace ClinicApp.Models
{
    public interface ICopyable<in T>
    {
        void CopyTo(T obj);
    }
}
