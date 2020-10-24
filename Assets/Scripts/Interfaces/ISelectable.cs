using UnityEngine;

public interface ISelectable
{
    void Hovered();
    void Unhovered();

    void Selected();
    void Unselected();

    void OnContextMenu(ContextMenu menu);
}
