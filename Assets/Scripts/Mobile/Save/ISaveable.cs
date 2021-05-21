
namespace Est.Mobile.Save
{
    public interface ISaveable 
    {
        object CaptureState();
        void RestoreState(object state);
    }
}
