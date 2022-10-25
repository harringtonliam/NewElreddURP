namespace RPG.Control
{
    public interface IRaycastable
    {
        public CursorType GetCursorType();
        public RaycastableReturnValue HandleRaycast(PlayerController playerController);
        public void HandleActivation(PlayerController playerController);
    }

}