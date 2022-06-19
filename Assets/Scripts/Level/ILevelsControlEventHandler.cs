using UnityEngine.Events;

public interface ILevelsControlEventHandler 
{
    public event UnityAction<int> LevelChanges;
    public event UnityAction<int> LevelRestarted;
    public event UnityAction<int> LevelStart;
}
