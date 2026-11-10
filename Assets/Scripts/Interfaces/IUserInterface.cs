using UnityEngine;

public interface IUserInterface
{
    //only place this on the parent UI, e.g. HUD. HUD holds HP,COins, etc. 
    public void ShowUI();
    public void HideUI();
}
