using System.Collections;

public interface IGrab
{
    public IGrabable grabObject { get; set; }
    public bool isGrab {  get; set; }
    public IEnumerator OnGrabAction();
    public void OnGrabExit();
}
