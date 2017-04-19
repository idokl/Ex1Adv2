using Ex1.Controller;

namespace Ex1.View
{
    interface IView
    {
        void Start(IController controller);
        void Stop();
    }
}
