namespace Veldrid.SceneGraph.InputAdapter
{
    public interface IUiActionAdapter
    {
        void RequestRedraw();

        void RequestContinuousUpdate(bool flag);
    }
}