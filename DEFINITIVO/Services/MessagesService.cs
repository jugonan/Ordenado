namespace DEFINITIVO.Services
{
    public class MessagesService
    {
        private bool ShowMessage;
        private string Message;
        public MessagesService()
        {
            ShowMessage = false;
            Message = null;
        }
        public void SetShowMessage(bool value)
        {
            ShowMessage = value;
        }

        public void SetMessage(string value)
        {
            Message = value;
        }
        public void ClearMessage()
        {
            Message = null;
        }

        public bool GetShowMessage()
        {
            return ShowMessage;
        }

        public string GetMessage()
        {
            return Message;
        }
    }
}
