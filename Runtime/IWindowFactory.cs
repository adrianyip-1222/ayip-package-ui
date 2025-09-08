using UnityEngine;

namespace AYip.UI
{
    /// <summary>
    /// Responsible for creating windows.
    /// </summary>
    public interface IWindowFactory
    {
        IWindow Create<TModal>(TModal modal, RectTransform canvasRoot) 
            where TModal : IWindowModal;
    }
}