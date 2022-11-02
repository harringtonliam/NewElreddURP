namespace RPG.Control
{
    public interface IRaycastable
    {
        public CursorType GetCursorType();
        public RaycastableReturnValue HandleRaycast(PlayerSelector playerSelector);
        public void HandleActivation(PlayerSelector playerSelector);
    }

}