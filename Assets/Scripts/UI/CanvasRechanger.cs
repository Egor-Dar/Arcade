namespace UI
{
    public class CanvasRechanger
    {
        private IHidenable _currentPanel;

        public void SetNewPanel(IHidenable newPanel)
        {
            if (_currentPanel != null)
            {
                _currentPanel.Hide();
            }

            _currentPanel = newPanel;
            _currentPanel.Show();
        }
    }
}